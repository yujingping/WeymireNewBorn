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

	[SerializeField]private UITable reminderTable;
	[SerializeField]private UITable conclusionTable;
	[SerializeField]private UITable truthTable;
	[SerializeField]private UILabel slot1;
	[SerializeField]private UILabel slot2;
	[SerializeField]private UILabel contentArea;

	[SerializeField]private ContentLine referenceLine1;
	[SerializeField]private ContentLine referenceLine2;

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
		referenceLine1 = null;
		referenceLine2 = null;
		slot1.text = slot2.text = "";
		currentType = ContentType.Reminder;
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
		UITable targetTable = null;
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

	public void AddSlot (ContentLine line)
	{
		if (referenceLine1 == null)
		{
			referenceLine1 = line;
			slot1.text = "" + line.content.index;
		}
		else if (referenceLine2 == null)
		{
			referenceLine2 = line;
			slot2.text = "" + line.content.index;
			AnalyzeContent();
		}
	}

	private void AnalyzeContent ()
	{
		if (referenceLine1 == null || referenceLine2 == null)
			return;
		if (referenceLine1.content.another == referenceLine2.content.index && referenceLine2.content.another == referenceLine1.content.index)
		{
			if (referenceLine1.content.type == ContentType.Reminder)
			{
				UnlockNewContent(ContentType.Conclusion, Consts.FileName.conclusions, referenceLine1.content.higher);
				SwitchToContent(ContentType.Conclusion);
			}
			else
			{
				UnlockNewContent(ContentType.Truth, Consts.FileName.truths, referenceLine1.content.higher);
				SwitchToContent(ContentType.Truth);
			}
			PlayAnalyzeEffect(true);
		}
		else
		{
			ClearSlots();
			PlayAnalyzeEffect(false);
		}
	}

	private void ClearSlots ()
	{
		referenceLine1 = referenceLine2 = null;
		slot1.text = slot2.text = "";
	}

	public void SwitchToContent (ContentType type)
	{
		if (type == currentType)
			return;
		ClearSlots();
	}

	private void PlayAnalyzeEffect (bool isSuccess)
	{
		
	}

	//An animation is expected here to enhance gamer experience. Just for test currently.
	public void ReadContent (ContentLine line)
	{
		contentArea.text = line.content.content;
	}
}
