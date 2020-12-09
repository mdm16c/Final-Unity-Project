using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinOrLose : MonoBehaviour
{
    private GameHandler GH;
    private Healthbar health;
    public bool canDie = true;

    private void Start() {
        GH = GameObject.Find("HUD").GetComponent<GameHandler>();
        health = GameObject.Find("HealthBar").GetComponent<Healthbar>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Finish") {
            //show text on canvas
            GH.youWon = true;

            //pause time
            Time.timeScale = 0.0f;

            //delay then call levelwon
            StartCoroutine(WinCoroutine());
        }
        else if(other.gameObject.tag == "Death"){
            //show text on canvas
            GH.youLose = true;

            //pause time
            Time.timeScale = 0.0f;

            //delay then call levelwon
            StartCoroutine(LoseCoroutine());
        }
    }

    void Update() {
        if (canDie) {
            if (health.health <= health.minimumHealth) {
                //show text on canvas
                GH.youLose = true;

                //pause time
                Time.timeScale = 0.0f;

                //delay then call levelwon
                StartCoroutine(LoseCoroutine());
            }
        }
    }

    IEnumerator WinCoroutine() {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSecondsRealtime(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator LoseCoroutine() {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSecondsRealtime(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
