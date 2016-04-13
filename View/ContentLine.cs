using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class ContentLine : MonoBehaviour 
{
	public AnalyzeSystem.Content content;

	public UILabel titleDisplay;
	private string finalTitle;

	public void Initialize (AnalyzeSystem.Content con)
	{
		content.type = con.type;
		content.title = con.title;
		content.content = con.content;
		content.index = con.index;
		content.higher = con.higher;
		finalTitle = "";
		switch (con.type)
		{
			case AnalyzeSystem.ContentType.Reminder:
				if (DialogueLua.GetVariable(Consts.VariableName.reminderReadState + content.index).AsBool)
					finalTitle = "[[66FA33]" + content.index + "[-]][[FFFF66]NEW[-]]";
				else
					finalTitle = "[[66FA33]" + content.index + "[-]]";
				break;
			case AnalyzeSystem.ContentType.Conclusion:
				if (DialogueLua.GetVariable(Consts.VariableName.conclusionReadState + content.index).AsBool)
					finalTitle = "[[66FA33]" + content.index + "[-]][[FFFF66]NEW[-]]";
				else
					finalTitle = "[[66FA33]" + content.index + "[-]]";
				break;
			case AnalyzeSystem.ContentType.Truth:
				if (DialogueLua.GetVariable(Consts.VariableName.truthReadState + content.index).AsBool)
					finalTitle = "[[66FA33]" + content.index + "[-]][[FFFF66]NEW[-]]";
				else
					finalTitle = "[[66FA33]" + content.index + "[-]]";
				break;
			default:
				break;
		}
		finalTitle += content.title;
		titleDisplay.text = finalTitle;
	}

	public void ReadContent ()
	{
		switch (content.type)
		{
			case AnalyzeSystem.ContentType.Reminder:
				DialogueLua.SetVariable(Consts.VariableName.reminderReadState + content.index, true);
				break;
			case AnalyzeSystem.ContentType.Conclusion:
				DialogueLua.SetVariable(Consts.VariableName.conclusionReadState + content.index, true);
				break;
			case AnalyzeSystem.ContentType.Truth:
				DialogueLua.SetVariable(Consts.VariableName.truthReadState + content.index, true);
				break;
			default:
				break;
		}
		finalTitle = "[[66FA33]" + content.index + "[-]]" + content.title;
		titleDisplay.text = finalTitle;
	}
}
