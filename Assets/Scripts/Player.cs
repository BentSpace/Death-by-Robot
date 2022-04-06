using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision) {
        Robot tobor = collision.gameObject.GetComponent<Robot>();
        if (tobor != null) {
            GameManager.inst.GameOver();
        }
    }
}
