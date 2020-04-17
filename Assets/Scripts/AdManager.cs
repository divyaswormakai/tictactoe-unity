using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
    private string playStoreID = "N";

    private string insterstitialAd = "from the dashboard";

    public bool isTestAd;
    void Start()
    {

    }

    private void InitializeAdvertisement()
    {
        Advertisement.Initialize(playStoreID, isTestAd);
    }

    public void PlayInterstitialAd()
    {
        if (!Advertisement.IsReady(insterstitialAd)) { return; }
        Advertisement.Show(insterstitialAd);
    }

}
