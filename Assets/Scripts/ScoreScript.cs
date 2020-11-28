using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;


public class ScoreScript : MonoBehaviour
{
    public int score=0;
    string shareMessage;
    public Canvas startCanvas;
    int isSoundOn;
    public Toggle audioToggle;
    public Text recordText, scoreText;

    // Start is called before the first frame update
    void Start()
    {   
        Time.timeScale = 0f;
        if (PlayerPrefs.HasKey("IsSoundOn"))
        {
            isSoundOn = PlayerPrefs.GetInt("IsSoundOn");
            if (isSoundOn == 1)
            {
                AudioListener.volume = 1f;
                audioToggle.isOn = true;
            }
            else
            {
                AudioListener.volume = 0;
                audioToggle.isOn = false;
            }
        }
        else PlayerPrefs.SetInt("IsSoundOn", 1);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKey(KeyCode.Space))
        {
            Time.timeScale = 1f;
        }*/
    }
    public void RestartButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShareButtonPressed()
    {
        shareMessage = "Wow! I scored " + score.ToString()+ " points in Clockwork Sigma!";
        StartCoroutine(TakeScreenshotAndShare());
    }

    private IEnumerator TakeScreenshotAndShare()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        // To avoid memory leaks
        Destroy(ss);

        new NativeShare().AddFile(filePath)
            .SetSubject("Clockwork Sigma").SetText(shareMessage)
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();

        // Share on WhatsApp only, if installed (Android only)
        //if( NativeShare.TargetExists( "com.whatsapp" ) )
        //	new NativeShare().AddFile( filePath ).AddTarget( "com.whatsapp" ).Share();
    }

    public void GameStart()
    {
        startCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void OpenTwitter()
    {
        Application.OpenURL("http://vk.com/javteq");
    }

    public void RateGame()
    {
        Application.OpenURL("market://details?id=com.Javteq.ClockworkSigma");
    }

    public void SetAudio(bool isOn)
    {
        if (isOn == true)
        {
            AudioListener.volume = 1f;
           PlayerPrefs.SetInt("IsSoundOn", 1);
           PlayerPrefs.Save();
        }
        else
        {
            AudioListener.volume = 0;
            PlayerPrefs.SetInt("IsSoundOn", 0);
            PlayerPrefs.Save();
        }
    }
    public void DeleteHighScore()
    {
        PlayerPrefs.DeleteKey("Record");
        recordText.text = "Highscore: 0";
        scoreText.text= ("Your Score: 0");
    }
}
