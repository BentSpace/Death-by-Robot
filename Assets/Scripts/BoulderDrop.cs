using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderDrop : MonoBehaviour
{
    // Reference to the Prefab.
    public GameObject boulderPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        Instantiate(boulderPrefab, new Vector3(445, 400, 700), Quaternion.identity);
    }
    void OnCollisionEnter(Collision collision)
    {
        //ContactPoint contact = collision.contacts[0];
        //Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        //Vector3 pos = contact.point;
        //Instantiate(boulderPrefab, pos, rot);
    }

}
