using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMgr : MonoBehaviour
{
    // Reference to the Prefab.
    public GameObject boulderPrefab;
    public List<GameObject> boulders = new List<GameObject>();
    public List<GameObject> robots   = new List<GameObject>();
    //public float slowSpeed = 2.5f;
    public int boulderLimit = 25;
    public Vector3 worldPosition;
    public Vector3 playerPosition;

    public static EntityMgr inst;

    // Start is called before the first frame update
    void Start()
    {
        inst = this;
        playerPosition = GameObject.Find("Player").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        /* Time-based culling
        foreach(Boulder b in boulders) {
            // If the boulder is going too slowly, start the process of decay
            float speedTest = b.GetComponent<Rigidbody>().velocity.magnitude;
            if (b.isDecaying && speedTest > slowSpeed) {
                b.ResetAge();
                b.SetDecay(false);
            } else if (!b.isDecaying && speedTest <= slowSpeed) {
                b.SetDecay(true);
            }

            // Any boulders which have remained slow for too long get culled
            if (b.cullMe) {
                boulders.Remove(b);
                Destroy(b);
            }
        }
        */
    }

    void OnMouseDown()
    {
        SpawnBoulder();
        while(boulders.Count > boulderLimit) {
            GameObject b = boulders[0];
            boulders.Remove(b);
            Destroy(b.gameObject);
        }
        
    }


    void SpawnBoulder() {

        Plane plane = new Plane(Vector3.up, playerPosition);
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            worldPosition = ray.GetPoint(distance);
        }
        GameObject b = Instantiate(boulderPrefab, worldPosition, Quaternion.identity);
        boulders.Add(b);
    }

    void OnCollisionEnter(Collision collision)
    {
        //ContactPoint contact = collision.contacts[0];
        //Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        //Vector3 pos = contact.point;
        //Instantiate(boulderPrefab, pos, rot);
    }

}
