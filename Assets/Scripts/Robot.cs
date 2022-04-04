using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public Vector3 position = Vector3.zero;
    public Vector3 desiredPosition;
    public Vector3 velocity = Vector3.zero;

    // VARIABLE
    public float speed;
    public float desiredSpeed;
    public float maxSpeed = 5;
    public float minSpeed;
    public float acceleration = 3;
    //public float cruisingSpeed;
    //public float maneuverSpeed;
    //public float turningSpeed;
    public float heading;
    public float desiredHeading;
    //public float currentStoppingDistance;
    // CONST
    public float turnRate = 45;


    // Start is called before the first frame update
    void Start() {
        //cruisingSpeed = (maxSpeed + minSpeed) * .75f;
        //maneuverSpeed = 0;
        //turningSpeed = maxSpeed * 0.1f;
        desiredPosition = position;
    }

    // Update is called once per frame
    void Update() {
        //currentStoppingDistance = 1.5f * Mathf.Pow(speed, 2) / acceleration;
    }
}
