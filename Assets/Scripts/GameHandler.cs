using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    public GameObject youWonPanel;
    public GameObject youLosePanel;
    public bool youWon = false;
    public bool youLose = false;
    private bool done = false;
    public AudioClip loseSound;
    public AudioClip winSound;
    public AudioClip bgMusicSound;
    private AudioSource bgMusic;
    private AudioSource rollSound;
    private AudioSource otherSound;
    public Transform playerPos;
    public Text TitleText;
    float TmStart;

    // Start is called before the first frame update
    void Start()
    {
        youWonPanel.SetActive(false);
        youLosePanel.SetActive(false);
        Time.timeScale = 1.0f;
        bgMusic = GetComponent<AudioSource>();
        bgMusic.clip = bgMusicSound;
        rollSound = GameObject.Find("RobotSphereParent").GetComponent<AudioSource>();
        otherSound = GameObject.Find("Audio").GetComponent<AudioSource>();
        playerPos = GameObject.Find("RobotSphereParent").GetComponent<Transform>();
        TmStart=Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!done) {
            if (youWon) {
                youWonPanel.SetActive(true);
                bgMusic.Stop();
                rollSound.Stop();
                otherSound.Stop();
                if (!bgMusic.isPlaying) {
                    bgMusic.PlayOneShot(winSound);
                }
                done = true;
            }
            else if (youLose) {
                youLosePanel.SetActive(true);
                bgMusic.Stop();
                rollSound.Stop();
                otherSound.Stop();
                if (!bgMusic.isPlaying) {
                    AudioSource.PlayClipAtPoint(loseSound, playerPos.position);
                }
                done = true;
            }
        }
        if(Time.time>TmStart + 3f){
            TitleText.gameObject.SetActive(false);
        }
    }
}
