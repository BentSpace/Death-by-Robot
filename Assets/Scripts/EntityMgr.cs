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

    public static EntityMgr inst;

    // Start is called before the first frame update
    void Start()
    {
        inst = this;
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
        GameObject b = Instantiate(boulderPrefab, new Vector3(445, 400, 700), Quaternion.identity);
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
