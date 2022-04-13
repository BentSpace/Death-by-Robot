using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager inst;
    public PotentialField player;
    public GameObject playerGO;
    public bool gameOver;
    public Canvas gameOverScreen;
    public Text txt;
    float startTime;
    public GameObject readyCircle;
    public GameObject explosionPrefab;
    public GameObject bloodPrefab;

    public Color noBoulder;
    public Color baseColor;
    public Color canDropColor;
    float timeLastBoulderDropped;
    public float timerFactor;
    public bool canDrop;


    void Awake() {
        inst = this;
        canDrop = true;
    }
    // Start is called before the first frame update
    void Start() {
        gameOverScreen.gameObject.SetActive(false);

        inst = this;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update() {
        UpdatePlayerColor();
    }

    public void EndGame() {
        gameOver = true;
    }



    public void GameOver() {
        MakePlayerExplode();
        EndGame();
        // Make game over screen visible
        gameOverScreen.gameObject.SetActive(true);
        float finalTime = Time.time - startTime;
        txt.text = string.Format("GAME OVER!\nYou got got.\nYou survived for {0} seconds", Time.time - startTime);
        // Destroy everybody
        EntityMgr.inst.ShutDownEntityGen();
    }

    public void HandleBoulderDrop() {
        timeLastBoulderDropped = Time.time;
        canDrop = false;
    }

    void UpdatePlayerColor() {
        if (Time.time - timeLastBoulderDropped <= timerFactor) {
            // Set material to a lerp between bad color and base color
            SetPlayerColor(Color.Lerp(noBoulder, baseColor, (Time.time - timeLastBoulderDropped) / timerFactor));
        } else {
            canDrop = true;
            SetPlayerColor(Color.Lerp(canDropColor, baseColor, (Time.time - timeLastBoulderDropped - timerFactor) / timerFactor * 2));
        }
    }

    void SetPlayerColor(Color c) {
        readyCircle.gameObject.GetComponent<Renderer>().material.SetColor("_Color", c);
    }

    void MakePlayerExplode()
    {
        //playerGO = GameObject.Find("Player");
        GameObject explosion = Instantiate(explosionPrefab, playerGO.transform.position, Quaternion.identity);
        GameObject blood = Instantiate(bloodPrefab, playerGO.transform.position, Quaternion.identity);
        playerGO.SetActive(false);
        Destroy(explosion, 3.0f);
        Destroy(blood, 4.0f);
    }
}
