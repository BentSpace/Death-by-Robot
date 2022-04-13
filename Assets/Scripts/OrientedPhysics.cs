using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientedPhysics : MonoBehaviour
{
    public float robotLevitation = 0;
    public Robot entity;
    public Vector3 eulerRotation = Vector3.zero;
    public PotentialField player;
    public Vector3 normalToTerrain;
    public TerrainData terrain;
    public float robotPersonalSpace = 10f;

    // Start is called before the first frame update
    void Start()
    {
        entity.position = transform.localPosition;
        eulerRotation.z = entity.heading;
        entity.desiredHeading = entity.heading;
        player = GameManager.inst.player;
    }

    // Update is called once per frame
    void Update()
    {
        DoPhysics();
    }

    void DoPhysics() {

        Vector3 finalVector = new Vector3();
        finalVector += GetBoulderVector();
        //finalVector += GetFriendVector();
        finalVector += GetPlayerVector();
        finalVector.y = 0;
        finalVector = Vector3.Normalize(finalVector);
        //Debug.Log(finalVector);


        //Vector3 playerPos = player.gameObject.transform.position;
        //entity.desiredPosition = playerPos;
        entity.desiredPosition = entity.position + finalVector;


        entity.desiredHeading = GetHeading(entity.position, entity.desiredPosition); ;

        entity.heading = Utils.NormalizeAngle(entity.heading);
        entity.desiredHeading = Utils.NormalizeAngle(entity.desiredHeading);

        if (Utils.ApproximatelyEqual(entity.heading, entity.desiredHeading)) {
            entity.heading = entity.desiredHeading;
        } else if (Utils.AngleDiffPosNeg(entity.desiredHeading, entity.heading) > 0) {
            entity.heading += entity.turnRate * Time.deltaTime;
        } else if (Utils.AngleDiffPosNeg(entity.desiredHeading, entity.heading) < 0) {
            entity.heading -= entity.turnRate * Time.deltaTime;
        }

        // Accelerate/decelerate to desired speed

        entity.desiredSpeed = Mathf.Cos((entity.desiredHeading - entity.heading) * Mathf.Deg2Rad) / 2.0f * entity.maxSpeed;
        if (Utils.ApproximatelyEqual(entity.speed, entity.desiredSpeed)) {
            entity.speed = entity.desiredSpeed;
        } else if (entity.speed < entity.desiredSpeed) {
            entity.speed = entity.speed + entity.acceleration * Time.deltaTime;
        } else if (entity.speed > entity.desiredSpeed) {
            entity.speed = entity.speed - entity.acceleration * Time.deltaTime;
        }

        entity.velocity.x = entity.speed * Mathf.Sin(entity.heading * Mathf.Deg2Rad);
        entity.velocity.y = 0;
        entity.velocity.z = entity.speed * Mathf.Cos(entity.heading * Mathf.Deg2Rad);

        eulerRotation.y = entity.heading;
        transform.localEulerAngles = eulerRotation;

        entity.position = entity.position + entity.velocity * Time.deltaTime;
        entity.position = Utils.GetTerrainPos(entity.position.x, entity.position.z) + new Vector3(0.0f, robotLevitation, 0.0f);
        transform.localPosition = entity.position;

        // Adjust orientation to be perpendicular to ground
        AlignTransform(entity.transform);

    }

    float GetHeading(Vector3 start, Vector3 end) {
        Vector3 diff = -(end - start);
        diff.y = 0;
        return Mathf.Rad2Deg * Mathf.Atan2(diff.x, diff.z);
    }

    public Vector3 GetBoulderVector() {
        Vector3 result = Vector3.zero;
        foreach (GameObject b in EntityMgr.inst.boulders) {
            if (Vector3.Distance(entity.position, b.transform.position) <= b.GetComponent<Rigidbody>().velocity.magnitude) {
                result -= b.GetComponent<PotentialField>().GetForceVector(entity.position, b.transform.position);
            }
        }
        return result;
    }

    public Vector3 GetFriendVector() {
        Vector3 result = Vector3.zero;
        foreach (GameObject ent in EntityMgr.inst.robots) {
            if (ent != entity && (ent.transform.position - entity.transform.position).magnitude <= robotPersonalSpace) {
                result += ent.gameObject.GetComponent<PotentialField>().GetForceVector(entity.position, ent.transform.position);
            }
        }
        return result;
    }

    public Vector3 GetPlayerVector() {
        return player.GetComponent<PotentialField>().GetForceVector(entity.position, player.transform.position);
    }

    // Borrowed from https://answers.unity.com/questions/868898/make-gameobject-always-vertical-to-terrain.html
    public static void AlignTransform(Transform transform)
    {
        Vector3 sample = SampleNormal(transform.position);

        Vector3 proj = transform.forward - (Vector3.Dot(transform.forward, sample)) * sample;
        transform.rotation = Quaternion.LookRotation(proj, sample);
    }

    // Borrowed from https://answers.unity.com/questions/868898/make-gameobject-always-vertical-to-terrain.html
    public static Vector3 SampleNormal(Vector3 position)
    {
        Terrain terrain = Terrain.activeTerrain;
        var terrainLocalPos = position - terrain.transform.position;
        var normalizedPos = new Vector2(
            Mathf.InverseLerp(0f, terrain.terrainData.size.x, terrainLocalPos.x),
            Mathf.InverseLerp(0f, terrain.terrainData.size.z, terrainLocalPos.z)
        );
        var terrainNormal = terrain.terrainData.GetInterpolatedNormal(normalizedPos.x, normalizedPos.y);
        return terrainNormal;
    }

}
