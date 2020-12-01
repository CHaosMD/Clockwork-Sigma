using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameScript : MonoBehaviour
{
    public Text endText, scoreRecord;
    public Canvas endCanvas;
    public ScoreScript Score;
    AdManager adManager;
    int record;
    public AudioClip recordSound, commonSound;
    AudioSource endSound;

    // Start is called before the first frame update
    void Start()
    {
        record = PlayerPrefs.GetInt("Record", 0);
        endCanvas.gameObject.SetActive(false);
        adManager = GameObject.Find("AdManager").GetComponent<AdManager>();
        endSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyTag" || collision.tag=="EnemyTagZero")
        {
            Time.timeScale = 0;
            if (Score.score > record)
            {
                endText.text = "New highscore: " + Score.score + "!";
                scoreRecord.text = "Highscore: " + Score.score;
                PlayerPrefs.SetInt("Record", Score.score);
                endSound.clip = recordSound;
                endSound.Play();
            }
            else
            {
                endText.text = ("Your Score: " + Score.score);
                scoreRecord.text = "Highscore: " + record;
                endSound.clip = commonSound;
                endSound.Play();
            }
            endCanvas.gameObject.SetActive(true);
            adManager.VideoShow();
            adManager.BannerShow();
        }
    }
}
