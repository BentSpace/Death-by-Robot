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

        // Potential Field Physics
        Vector3 finalVector = GetBoulderVector();
        finalVector += GetFriendVector();
        finalVector += GetPlayerVector();

        // Accelerate/decelerate to desired speed
        if (Utils.ApproximatelyEqual(entity.speed, entity.desiredSpeed)) {
            entity.speed = entity.desiredSpeed;
        } else if (entity.speed < entity.desiredSpeed) {
            entity.speed = entity.speed + entity.acceleration * Time.deltaTime;
        } else if (entity.speed > entity.desiredSpeed) {
            entity.speed = entity.speed - entity.acceleration * Time.deltaTime;
        }

        // Normalize/recalculate heading
        entity.heading = Utils.NormalizeAngle(entity.heading);
        entity.desiredHeading = Utils.NormalizeAngle(Mathf.Atan2(entity.desiredPosition.x - entity.position.x, entity.desiredPosition.z - entity.position.z) * Mathf.Rad2Deg);

        // Adjust heading (turning)
        if (Utils.ApproximatelyEqual(entity.heading, entity.desiredHeading)) {
            entity.heading = entity.desiredHeading;
        } else if (Utils.AngleDiffPosNeg(entity.desiredHeading, entity.heading) > 0) {
            entity.heading += entity.turnRate * Time.deltaTime;
        } else if (Utils.AngleDiffPosNeg(entity.desiredHeading, entity.heading) < 0) {
            entity.heading -= entity.turnRate * Time.deltaTime;
        }

        // Recalculate velocity
        entity.velocity.x = entity.speed * Mathf.Sin(entity.heading * Mathf.Deg2Rad);
        entity.velocity.y = 0;
        entity.velocity.z = entity.speed * Mathf.Cos(entity.heading * Mathf.Deg2Rad);

        // Step position movement
        entity.position = entity.position + entity.velocity * Time.deltaTime;
        transform.localPosition = entity.position;

        // Adjust position for terrain height
        entity.position = Utils.GetTerrainPos(entity.position.x, entity.position.z) + new Vector3(0.0f, robotLevitation, 0.0f);

        eulerRotation.z = -entity.heading;
        transform.localEulerAngles = eulerRotation;
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
