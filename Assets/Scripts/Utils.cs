using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    // Start is called before the first frame update

    public static float EPSILON = 0.01f;
    public static float VECTOR_EPSILON = 1f;
    public static Utils inst;

    void Start()
    {
        inst = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Vector3 GetTerrainPos(float x, float y) {
        //Create object to store raycast data
        RaycastHit hit;

        //Create origin for raycast that is above the terrain. I chose 100.
        Vector3 origin = new Vector3(x, 500, y);

        //Send the raycast.
        Physics.Raycast(origin, Vector3.down, out hit, Mathf.Infinity);

        //Debug.Log("Terrain location found at " + hit.point);
        return hit.point;
    }

    public static bool ApproximatelyEqual(float a, float b) {
        return (Mathf.Abs(a - b) <= EPSILON);
    }

    public static bool ApproximatelyEqual(Vector3 a, Vector3 b) {
        return (a - b).magnitude <= VECTOR_EPSILON;
    }

    public static bool ApproximatelyEqual(float a, float b, float EPS) {
        return (Mathf.Abs(a - b) <= EPS);
    }

    public static bool ApproximatelyEqual(Vector3 a, Vector3 b, float EPS) {
        return (a - b).magnitude <= EPS;
    }

    public static float Clamp(float value, float min, float max) {
        return Mathf.Max(Mathf.Min(max, value), min);
    }

    public static bool InRange(float value, float min, float max) {
        return min <= value && value <= max;
    }

    public static bool InRange(Vector2 pos, float minX, float maxX, float minY, float maxY) {
        return InRange(pos.x, minX, maxX) && InRange(pos.y, minY, maxY);
    }

    public static float AngleDiffPosNeg(float a, float b) {
        float diff = a - b;
        if (diff > 180)
            return diff - 360;
        if (diff < -180)
            return diff + 360;
        return diff;
    }

    public static float NormalizeAngle(float a) {
        while (a < 0) {
            a += 360;
        }
        a %= 360;
        return a;
    }
}
