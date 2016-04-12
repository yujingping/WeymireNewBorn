using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;

public class AnalyzeSystem : MonoBehaviour 
{
	//AnalyzeSystem is also a singleton!
	private static AnalyzeSystem instance;

	public enum ContentType
	{
		Reminder,
		Conclusion,
		Truth
	}

	struct Content
	{
		public ContentType type;
		public int index;
		public string title;
		public string content;
		public int another; // For truths, this value is -1 by default. After all truths cannot be composited any more ... 

		public Content (ContentType ty, string t, string c, int i, int a = -1)
		{
			type = ty;
			title = t;
			content = c;
			index = i;
			another = a;
		}
	}

	public GameObject contentPrefab;

	[SerializeField]private UITable reminderTable;
	[SerializeField]private UITable conclusionTable;
	[SerializeField]private UITable truthTable;

	[SerializeField]private const float UPDATE_TIME = 1f;

	private List<int> variablesToCheck;

	void Awake ()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			DestroyImmediate(gameObject);
		DontDestroyOnLoad(gameObject);
	}

    private void Initialize ()
    {
		InitializeContents(ContentType.Reminder, Consts.VariableName.reminderState, Consts.FileName.reminders, Consts.Constants.REMINDER_NUM);
		InitializeContents(ContentType.Conclusion, Consts.VariableName.conclusionState, Consts.FileName.conclusions, Consts.Constants.CONCLUSION_NUM);
		InitializeContents(ContentType.Truth, Consts.VariableName.truthState, Consts.FileName.truths, Consts.Constants.TRUTH_NUM);
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

	private static void UnlockNewContent (ContentType type, string targetFileName, int number)
	{
		string contentText = InfoSaver.GetStringFromResource(targetFileName, number);
		//!!!!!!UNSAFE coding way. Please make sure that the targetFile is absolutely correct!!!!!!
		string[] lineComponents = contentText.Split('#');
		int index = number;
		string title = lineComponents[1];
		string content = lineComponents[2];
		int another = ((type == ContentType.Truth) ? -1 : int.Parse (lineComponents[3]));
		Content newContentEntry = new Content(type, title, content, index, another);
		//To be CONTINUED : WRITE A NEW CONTENT CLASS AND IMPLEMENT ITS INITIALIZER, THEN ADD IT HERE!!!!
	}

	//Please do not leave malicious comments on this part ... after all this is the by-product of the DialogueLua Environment!
	//(I WILL NEVER USE THIS SYSTEM AGAIN!)
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
				}
			}
			yield return UPDATE_TIME;
		}
	}
}
