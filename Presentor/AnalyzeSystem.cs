using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;

public class AnalyzeSystem : MonoBehaviour 
{
	//AnalyzeSystem is also a singleton! This script includes both VIEW and PRESENTOR Section. However their logics are seperated. 
	//Never violate the rules of MVP!!
	private static AnalyzeSystem instance;

	public enum ContentType
	{
		Reminder,
		Conclusion,
		Truth
	}

	public struct Content
	{
		public ContentType type;
		public int index;
		public string title;
		public string content;
		public int another; // For truths, this value is -1 by default. After all truths cannot be composited any more ... 
		public int higher; //This is the corresponding higher-level content. Truths have -1 for this variable.

		public Content (ContentType ty, string t, string c, int i, int a = -1, int h = -1)
		{
			type = ty;
			title = t;
			content = c;
			index = i;
			another = a;
			higher = h;
		}
	}

	public static GameObject contentPrefab;

	[SerializeField]private UIGrid reminderTable;
	[SerializeField]private UIGrid conclusionTable;
	[SerializeField]private UIGrid truthTable;
	[SerializeField]private List<ContentLine> contentSlots;
	[SerializeField]private AnalyzeSystemGridController brainGridController;

	[SerializeField]private const float UPDATE_TIME = 1f;

	private List<int> variablesToCheck;

	private ContentType currentType;

	void Awake ()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			DestroyImmediate(gameObject);
		DontDestroyOnLoad(gameObject);
	}

	void Start ()
	{
		StartCoroutine(ContentUpdate());
	}

	void OnEnable ()
	{
		StopAllCoroutines();
		StartCoroutine(ContentUpdate());
	}

	void OnDisable ()
	{
		StopAllCoroutines();
	}

	private void Initialize ()
    {
		InitializeVariables();
		InitializeContents(ContentType.Reminder, Consts.VariableName.reminderState, Consts.FileName.reminders, Consts.Constants.REMINDER_NUM);
		InitializeContents(ContentType.Conclusion, Consts.VariableName.conclusionState, Consts.FileName.conclusions, Consts.Constants.CONCLUSION_NUM);
		InitializeContents(ContentType.Truth, Consts.VariableName.truthState, Consts.FileName.truths, Consts.Constants.TRUTH_NUM);
    }

	private void InitializeVariables ()
	{
		instance.contentSlots.Clear();
		currentType = ContentType.Reminder;
		variablesToCheck = new List<int>();
	}

	private void InitializeContents (ContentType type, string variableName, string targetFileName, int number)
	{
		for (int i = 0; i < number; i++)
		{
			if (DialogueLua.GetVariable(variableName + i).AsBool)
				UnlockNewContent(type, targetFileName, i);
			else if (type == ContentType.Reminder)
				variablesToCheck.Add(i);
		}
	}

	public static void UnlockNewContent (ContentType type, string targetFileName, int number)
	{
		string contentText = InfoSaver.GetStringFromResource(targetFileName, number);
		//!!!!!!UNSAFE coding way. Please make sure that the targetFile is absolutely correct!!!!!!
		string[] lineComponents = contentText.Split('#');
		int index = number;
		string title = lineComponents[1];
		string content = lineComponents[2];
		int another = ((type == ContentType.Truth) ? -1 : int.Parse (lineComponents[3]));
		int higher = ((type == ContentType.Truth) ? -1 : int.Parse (lineComponents[4]));
		Content newContentEntry = new Content(type, title, content, index, another, higher);
		//To be CONTINUED : WRITE A NEW CONTENT CLASS AND IMPLEMENT ITS INITIALIZER, THEN ADD IT HERE!!!!
		UIGrid targetTable = null;
		switch (type)
		{
			case ContentType.Reminder:
				targetTable = instance.reminderTable;
				break;
			case ContentType.Conclusion:
				targetTable = instance.conclusionTable;
				break;
			case ContentType.Truth:
				targetTable = instance.truthTable;
				break;
			default:
				break;
		}
		GameObject targetContent = NGUITools.AddChild(targetTable.gameObject, contentPrefab);
		ContentLine targetLine = targetContent.GetComponent<ContentLine>();
		targetLine.Initialize(newContentEntry);
	}

	//Please do not leave malicious comments on this part ... after all this is the by-product of the DialogueLua Environment!
	//(I WILL NEVER USE THIS SYSTEM AGAIN!)
	//Acturally only update REMINDERS. Conclusions and truths could not be received directly. 
	private IEnumerator ContentUpdate()
	{
		while (true)
		{
			foreach (int varName in variablesToCheck)
			{
				if (DialogueLua.GetVariable(Consts.VariableName.reminderState + varName).AsBool)
				{
					variablesToCheck.Remove(varName);
					UnlockNewContent(ContentType.Reminder, Consts.FileName.reminders, varName);
					InfoSaver.SaveLuaEnvironment();
				}
			}
			yield return UPDATE_TIME;
		}
	}

	//An animation is expected here to enhace gamer experience. Just for test currently.
	public static void AnalyzeContent ()
	{
		Debug.Log("I am analyzing! Please wait! Haha actually you will never get a result here now!");
	}

	public void SwitchToContent (ContentType type)
	{
		if (type == currentType)
			return;
		instance.contentSlots.Clear();
	}

	private void PlayAnalyzeEffect (bool isSuccess)
	{
		
	}

	public static void SwitchTable (ContentType type)
	{
		instance.reminderTable.gameObject.SetActive(false);
		instance.conclusionTable.gameObject.SetActive(false);
		instance.truthTable.gameObject.SetActive(false);
		switch (type)
		{
			case ContentType.Reminder:
				instance.reminderTable.gameObject.SetActive(true);
				break;
			case ContentType.Conclusion:
				instance.conclusionTable.gameObject.SetActive(true);
				break;
			case ContentType.Truth:
				instance.truthTable.gameObject.SetActive(true);
				break;
			default:
				break;
		}
		instance.contentSlots.Clear();
	}

	public static UIGrid GetCurrentGrid ()
	{
		switch (instance.currentType)
		{
			case ContentType.Reminder:
				return instance.reminderTable;
				break;
			case ContentType.Conclusion:
				return instance.conclusionTable;
				break;
			case ContentType.Truth:
				return instance.truthTable;
				break;
			default:
				break;
		}
		return null;
	}

	public static void FillSlots (List<ContentLine> contentList)
	{
		instance.contentSlots.Clear();
		instance.contentSlots = contentList;
	}
}
