using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotentialField : MonoBehaviour
{
    public float c;
    public float e;
    public string obstacleType;

    public float boulderC =  50000f;       // TODO Tweak 'C' values!
    public float boulderE = -2f;   // DO NOT CHANGE (repulsive to robots)
    public float robotC   =  50000f;
    public float robotE   = -2f;   // DO NOT CHANGE (repulsive to robots)
    public float playerC  =  5000f;
    public float playerE  = -1f;   // DO NOT CHANGE (attractive to robots)

    // Start is called before the first frame update
    void Start()
    {
        switch (obstacleType) {
            case "robot":
                c = robotC;
                e = robotE;
                break;
            case "boulder":
                c = boulderC;
                e = boulderE;
                break;
            case "player":
                c = playerC;
                e = playerE;
                break;
            default:
                c = 1.5f;
                e = -2f;        // Repulsive by default
                break;
        }

        // Optional code, scales attractive/repulsive force by entity scale. Will enable if needed
        // c *= Mathf.Max(this.gameObject.transform.localScale.x, this.gameObject.transform.localScale.y);
    }

    public float GetForceScalar(Vector3 b, Vector3 a) {
        if (a == b) {
            Debug.Log("got 0 scalar");
        }
        return Mathf.Pow((a - b).magnitude, e) * c;
    }

    public Vector3 GetForceVector(Vector3 a, Vector3 b) {
        Vector3 uni = (a - b);
        uni.Normalize();
        return GetForceScalar(a, b) * uni;
    }
}
