using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMgr : MonoBehaviour
{
    // Reference to the Prefab.
    public GameObject boulderPrefab;
    public List<GameObject> boulders;
    public GameObject robotPrefab;
    public List<GameObject> robots;
    public GameObject explosionPrefab;

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
        boulders = new List<GameObject>();
        //robots = new List<GameObject>();
        InvokeRepeating("SpawnRobot", 2.0f, 1f);
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

    public void AddBoulder()
    {
        if (!GameManager.inst.gameOver && GameManager.inst.canDrop) {
            SpawnBoulder();
            GameManager.inst.HandleBoulderDrop();
            while (boulders.Count > boulderLimit) {
                GameObject b = boulders[0];
                CullBoulder(b);
            }
        }
    }


    void SpawnBoulder()
    {
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

    public void CullRobot(GameObject tobor) {
        GameObject explosion = Instantiate(explosionPrefab, tobor.transform.position, Quaternion.identity);
        Destroy(explosion, 3.0f);
        robots.Remove(tobor);
        Destroy(tobor);
    }

    public void CullBoulder(GameObject rollder) {
        boulders.Remove(rollder);
        Destroy(rollder);
    }

    void SpawnRobot()
    {
        var randomLocation = new Vector3(Random.Range(100.0f, 400.0f), 0, Random.Range(100.0f, 250.0f));
        GameObject r = Instantiate(robotPrefab, randomLocation, Quaternion.identity);
        robots.Add(r);
    }

    public void ShutDownEntityGen() {
        CancelInvoke();
        ClearAllEntities();
    }

    void ClearAllEntities() {
        foreach (GameObject b in boulders) {
            Destroy(b);
        }
        boulders.Clear();
        foreach (GameObject r in robots) {
            Destroy(r);
        }
        robots.Clear();
    }
}
