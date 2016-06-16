using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using System.Collections.Generic;

//UIController.cs serves as the controller of all the sprites, images, buttons and interactable 2D elements in the game view. Hence it includes UI part and notifier part.
public class UIController : MonoBehaviour 
{
	private GameObject uiRoot;
	private GameObject backPack;
	private GameObject gallery;
	private GameObject analyzeSystem;
	private GameObject questLog;

	private static UIController instance;

	[SerializeField]private static GameObject pickUpItemNotifier;
	[SerializeField]private static GameObject interactableNotifier;
	[SerializeField]private static GameObject reminderNotifier;
	[SerializeField]private static GameObject incapableOfInteractNotifier;
	[SerializeField]private static GameObject tooFarNotifier;

	[SerializeField]private static GameObject UGUIRoot;
	[SerializeField]private static GameObject NGUIRoot;

	[SerializeField]private static Camera mainCamera;

	public static GameObject UIRoot
	{
		get
		{
			return instance.uiRoot;
		}
	}

	public static GameObject BackPack 
	{
		get
		{
			return instance.backPack;
		}
	}

	public static GameObject Gallery
	{
		get
		{
			return instance.gallery;
		}
	}

	public static GameObject AnalyzeSystem
	{
		get
		{
			return instance.analyzeSystem;
		}
	}

	public static GameObject QuestLog
	{
		get
		{
			return instance.questLog;
		}
	}

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

	/// <summary>
	/// Adds the sprite to the UI Root and provide it with a layer number to make sure that it works fine. 
	/// </summary>
	/// <param name="targetSprite">Target sprite.</param>
	/// <param name="layerNumber">Layer number.</param>
	public static void AddTutorSpriteAndSetLayer (GameObject targetSprite, int layerNumber)
	{
		/// Note that it is still controversy whether I should set the layer number in the prefab, or define it here. 
		/// The tutor window 
	}
}
