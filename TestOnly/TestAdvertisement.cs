using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class TestAdvertisement : MonoBehaviour 
{
	public void ShowAd ()
	{
		if (Advertisement.IsReady())
		{
			Advertisement.Show("rewardedVideo", new ShowOptions(){resultCallback = Handler});
		}
	}

	private void Handler (ShowResult options)
	{
		switch (options)
		{
			case ShowResult.Failed:
				Debug.Log("Gali gege!");
				break;
			case ShowResult.Finished:
				Debug.Log("Aligato!");
				break;
			case ShowResult.Skipped:
				Debug.Log("2333333");
				break;
			default :
				Debug.Log("U must be kidding!");
				break;
		}
	}
}
