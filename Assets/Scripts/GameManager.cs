using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager inst;
    public PotentialField player;
    public bool gameOver;
    public Canvas gameOverScreen;
    public Text txt;
    float startTime;


    void Awake() {
        inst = this;
    }
    // Start is called before the first frame update
    void Start() {
        gameOverScreen.gameObject.SetActive(false);

        inst = this;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update() {

    }

    public void EndGame() {
        gameOver = true;
    }

    public void GameOver() {
        EndGame();
        // Make game over screen visible
        gameOverScreen.gameObject.SetActive(true);
        float finalTime = Time.time - startTime;
        txt.text = string.Format("GAME OVER!\nYou got got.\nYou survived for {0} seconds", Time.time - startTime);
        // Destroy everybody
        EntityMgr.inst.ShutDownEntityGen();
    }
}
