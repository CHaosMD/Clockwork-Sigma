using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
    static int adCounter=1, nextAd=1, firstStart=0;
    int stepToNextAd;
    //Ad Code
    public string gameId = "3881675";
    string placementID = "banner";

    // Start is called before the first frame update
    void Start()
    {
        if (firstStart < 1)
        {
            Advertisement.Initialize(gameId, false);
            firstStart++;
        }
        StartCoroutine(PrepareBanner());
    }

    public void HideBanner()
    {
        Advertisement.Banner.Hide();
    }

    public IEnumerator PrepareBanner()
    {
        while (!Advertisement.IsReady (placementID))
        {
            yield return new WaitForSeconds(0.3f);
        }
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
    }
    public void BannerShow()
    {
        Advertisement.Banner.Show(placementID);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void VideoShow()
    {
        if (adCounter == nextAd)
        {
            if (Advertisement.IsReady())
            {
                Advertisement.Show("video");
            }

            stepToNextAd = Random.Range(8, 16);
            nextAd += stepToNextAd;
            adCounter++;
        }
        adCounter++;
    }
}
