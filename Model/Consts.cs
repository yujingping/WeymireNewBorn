using UnityEngine;
using System.Collections;

public class Consts
{
    public enum SaveType
    {
        PickUpItem,
        InteractableObject,
        Reminder,
        RealPicture,
        Conclusion,
        Truth
    }

	public enum DisplaySetting
	{
		FirstPersonNormal,
		ThirdPersonNormal,
		PhotoTaking,
		BackPack,
		Conversation,
		MainMenu,
		QuestLog,
		Reminder,
		Gallery,
		Tutoring,
	}

	public class QuestCondition
	{
		public const string Success = "success";
		public const string Active = "active";
		public const string Failed = "failed";
	}

	public class VariableName
	{
		public const string pickUpItemState = "PickUpItemState";
		public const string InteractableItemState = "InteractableItemState";
		public const string realPictureState = "RealPictureState";
		public const string reminderState = "ReminderState";
		public const string conclusionState = "ConclusionState";
		public const string truthState = "TruthState";
		public const string backPackPermanentItemName = "BackPackPermanentItemName";
		public const string backPackTemporaryItemName = "BackPackTemporaryItemName";
		public const string playerPosX = "PlayerPosX";
		public const string playerPosY = "PlayerPosY";
		public const string playerPosZ = "PlayerPosZ";
		public const string luaEnvironmentData = "LuaEnvironmentData";
        public const string galleryImageIndex = "GalleryImageIndex";
        public const string galleryImageNum = "GalleryImageNum";
        public const string galleryImageHead = "GalleryImageHead";//This value could only be added by 1. It could not reduce or add anything else.
	}

    public class FileName
    {
        public const string galleryBlankImage = "BlankImage";
    }
};