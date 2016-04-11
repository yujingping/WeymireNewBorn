using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;

public class InfoSaver : MonoBehaviour 
{
	private static InfoSaver instance;

	//Awake function checks whether the static singleton exists. If not, create one!
	void Awake ()
	{
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            DestroyImmediate(gameObject);
        }
        DontDestroyOnLoad(gameObject);
	}

    public static void SavePlayerPosition (Vector3 playerPos)
    {
        PlayerPrefs.SetFloat(Consts.VariableName.playerPosX, playerPos.x);
        PlayerPrefs.SetFloat(Consts.VariableName.playerPosY, playerPos.y);
        PlayerPrefs.SetFloat(Consts.VariableName.playerPosZ, playerPos.z);
    }

    public static void SetBoolState (Consts.SaveType saveType, string variableName, bool state)
    {
        string finalName = "";
        switch (saveType)
        {
            case Consts.SaveType.PickUpItem: 
                finalName = Consts.VariableName.pickUpItemState + variableName;
                break;
            case Consts.SaveType.InteractableObject:
                finalName = Consts.VariableName.InteractableItemState + variableName;
                break;
            case Consts.SaveType.RealPicture:
                finalName = Consts.VariableName.realPictureState + variableName;
                break;
            case Consts.SaveType.Reminder:
                finalName = Consts.VariableName.reminderState + variableName;
                break;
            case Consts.SaveType.Conclusion:
                finalName = Consts.VariableName.conclusionState + variableName;
                break;
            case Consts.SaveType.Truth:
                finalName = Consts.VariableName.truthState + variableName;
                break;
            default:
                break;
        }
        PlayerPrefs.SetInt(finalName, state ? 1 : 0);
    }

    public static bool GetBoolState (Consts.SaveType saveType, string variableName)
    {
        string finalName = "";
        switch (saveType)
        {
            case Consts.SaveType.PickUpItem: 
                finalName = Consts.VariableName.pickUpItemState + variableName;
                break;
            case Consts.SaveType.InteractableObject:
                finalName = Consts.VariableName.InteractableItemState + variableName;
                break;
            case Consts.SaveType.RealPicture:
                finalName = Consts.VariableName.realPictureState + variableName;
                break;
            case Consts.SaveType.Reminder:
                finalName = Consts.VariableName.reminderState + variableName;
                break;
            case Consts.SaveType.Conclusion:
                finalName = Consts.VariableName.conclusionState + variableName;
                break;
            case Consts.SaveType.Truth:
                finalName = Consts.VariableName.truthState + variableName;
                break;
            default:
                break;
        }
        return PlayerPrefs.GetInt(finalName) == 1;
    }

    public static void SaveLuaEnvironment ()
    {
        string luaEnvironment = PersistentDataManager.GetSaveData();
        PlayerPrefs.SetString(Consts.VariableName.luaEnvironmentData, luaEnvironment);
    }

    public static string GetLuaEnvironment ()
    {
        return PlayerPrefs.GetString(Consts.VariableName.luaEnvironmentData);
    }

    //!!!!!!NOTICE: PLEASE REMEMBER TO USE COROUTINE IN THE PRESENTOR SECTOR!!!!!!. 
    public void TakeScreenShot ()
    {
        Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        //For some unknown reason, compression of images is not supported on iOS platform.
        if (Application.platform == RuntimePlatform.Android)
            screenShot.Compress(true);
        screenShot.Apply();
        byte[] bytes = screenShot.EncodeToPNG();
        GallerySaveNewImage(bytes);
    }

    public static IEnumerator GetGalleryTextureByID (UITexture targetTexture, int index)
    {
        if (index == 0)
        {
            targetTexture.mainTexture = Resources.Load(Consts.FileName.galleryBlankImage, typeof(Texture2D)) as Texture2D;
        }
        else
        {
            string path = "file://" + Application.persistentDataPath + "/Screenshot" + PlayerPrefs.GetInt (Consts.VariableName.galleryImageIndex + index) + ".png";
            WWW www = new WWW(path);
            yield return www;
            targetTexture.mainTexture = www.texture;
        }
    }

    public static void GallerySaveNewImage (byte[] bytes)
    {
        int index = PlayerPrefs.GetInt(Consts.VariableName.galleryImageNum) + 1;
        PlayerPrefs.SetInt(Consts.VariableName.galleryImageNum, index);
        int head = PlayerPrefs.GetInt(Consts.VariableName.galleryImageHead) + 1;
        PlayerPrefs.SetInt(Consts.VariableName.galleryImageHead, head);
        string fileName = Application.persistentDataPath + "/Screenshot" + head + ".png";
        System.IO.File.WriteAllBytes(fileName, bytes);
    }

    //Return a new Texture to display after deleting the image by its index.
    public static IEnumerator GalleryDeleteImageByID (UITexture targetTexture, int index)
    {
        int totalImageNum = PlayerPrefs.GetInt(Consts.VariableName.galleryImageNum);
        int oldIndex = index;
        for (int i = index; i < totalImageNum; i++)
            PlayerPrefs.SetInt(Consts.VariableName.galleryImageIndex + i, PlayerPrefs.GetInt(Consts.VariableName.galleryImageIndex + (i + 1)));
        if (oldIndex == totalImageNum)
        {
            yield return instance.StartCoroutine(GetGalleryTextureByID (targetTexture, index - 1));
            index--;
        }
        PlayerPrefs.SetInt(Consts.VariableName.galleryImageNum, totalImageNum - 1);
        string deletePath = Application.persistentDataPath + "/Screenshot" + oldIndex + ".png";
        System.IO.File.Delete(deletePath);
        yield return instance.StartCoroutine(GetGalleryTextureByID(targetTexture, index));
    }
}
