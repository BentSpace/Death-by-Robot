using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMgr : MonoBehaviour
{
    // Reference to the Prefab.
    public Boulder boulderPrefab;
    List<Boulder> boulders = new List<Boulder>();
    public float slowSpeed = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    void OnMouseDown()
    {
        SpawnBoulder();
    }


    void SpawnBoulder() {
        boulders.Add(Instantiate(boulderPrefab, new Vector3(445, 400, 700), Quaternion.identity));
    }

    void OnCollisionEnter(Collision collision)
    {
        //ContactPoint contact = collision.contacts[0];
        //Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        //Vector3 pos = contact.point;
        //Instantiate(boulderPrefab, pos, rot);
    }

}
