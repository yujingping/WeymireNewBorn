using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour 
{
	private static TutorialManager instance;

	private void Awake ()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
			DestroyImmediate(gameObject);
	}

	/// <summary>
	/// Invoke a tutorial based on the corresponding index;
	/// </summary>
	/// <param name="index">Index.</param>
	public void InvokeTutorial (int index)
	{
		
	}

	/// <summary>
	/// Load the tutor from infoSaver.
	/// </summary>
	private void LoadTutorFromFile ()
	{
		
	}
}
