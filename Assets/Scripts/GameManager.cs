using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager inst;
    public PotentialField player;
    public bool gameOver;
    public Canvas gameOverScreen;

    void Awake() {
        inst = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.gameObject.SetActive(false);
        inst = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndGame() {
        gameOver = true;
    }

    public void GameOver() {
        EndGame();
        // Make game over screen visible
        gameOverScreen.gameObject.SetActive(true);
        // Destroy everybody
        EntityMgr.inst.ShutDownEntityGen();
    }
}
