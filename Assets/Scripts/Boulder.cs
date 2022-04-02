using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{

    public int maxAge; 
    public int currentAge;
    public bool isDecaying;
    public bool cullMe;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Live();
        Die();
    }

    void Live() {
        if (isDecaying) {
            currentAge -= Time.deltaTime;
        }
    }

    // If it has exceeded its age, it marks itself for culling for the entity manager (if it hasn't already)
    void Die() {
        if (isDecaying && currentAge <= 0 && !cullMe) {
            cullMe = true;
        }
    }

    public void SetDecay(bool value) {
        isDecaying = value;
    }

    public void ResetAge() {
        cullMe = false;
        currentAge = maxAge;
    }
}
