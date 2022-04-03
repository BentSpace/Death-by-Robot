using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientedPhysics : MonoBehaviour
{
    float robotLevitation = 10;
    public Robot entity;
    public Vector3 eulerRotation = Vector3.zero;
    public PotentialField player;

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

        /*
        // Potential Field Physics
        Vector3 finalVector = GetBoulderVector();
        finalVector += GetFriendVector();
        finalVector += GetPlayerVector();
        float finalAngle = Vector3.Angle(finalVector, transform.forward);
        */

        Vector3 playerPos = player.gameObject.transform.position;
        entity.desiredPosition = playerPos;
        
        entity.desiredHeading = GetHeading(entity.position, entity.desiredPosition);

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

    }

    float GetHeading(Vector3 start, Vector3 end) {
        Vector3 diff = end - start;
        diff.y = 0;
        return Mathf.Atan2(diff.y, diff.x);
    }

    public Vector3 GetBoulderVector() {
        Vector3 result = Vector3.zero;
        foreach (GameObject b in EntityMgr.inst.boulders) {
            result += b.GetComponent<PotentialField>().GetForceVector(entity.position, b.transform.position);
        }
        return result;
    }

    public Vector3 GetFriendVector() {
        Vector3 result = Vector3.zero;
        foreach (GameObject ent in EntityMgr.inst.robots) {
            if (ent != entity) {
                result += ent.gameObject.GetComponent<PotentialField>().GetForceVector(entity.position, ent.transform.position);
            }
        }
        return result;
    }

    public Vector3 GetPlayerVector() {
        return player.GetComponent<PotentialField>().GetForceVector(entity.position, player.transform.position);
    }
}
