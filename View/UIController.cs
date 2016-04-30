using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using System.Collections.Generic;

//UIController.cs serves as the controller of all the sprites, images, buttons and interactable 2D elements in the game view. Hence it includes UI part and notifier part.
public class UIController : MonoBehaviour 
{
	private static UIController instance;

	[SerializeField]private static GameObject pickUpItemNotifier;
	[SerializeField]private static GameObject interactableNotifier;
	[SerializeField]private static GameObject reminderNotifier;
	[SerializeField]private static GameObject incapableOfInteractNotifier;
	[SerializeField]private static GameObject tooFarNotifier;

	[SerializeField]private static GameObject UGUIRoot;
	[SerializeField]private static GameObject NGUIRoot;

	[SerializeField]private static Camera mainCamera;

	void Awake ()
	{
		mainCamera = Camera.main;
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			DestroyImmediate(this.gameObject);
		}
		DontDestroyOnLoad(this);
	}

	//To be implemented after the UI Controls have been completely determined.
	public static void SetUISituation (Consts.DisplaySetting setting)
	{
		
	}
}
