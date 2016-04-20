﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhotoCamera : MonoBehaviour 
{
	private Consts.LensType LensType
	{
		get
		{
			return lensType;
		}
		set
		{
			if (lensType != value)
			{
				lensType = value;
				StartCoroutine(PlaySwitchEffect());
			}
		}
	}

	private List<PickUpItem> pickUpItems, allPickUpItems;
	private List<InteractableObject> interactableObjects, allInteractableObjects;
	private List<RealPicture> realPicture;
	private Consts.LensType lensType;
	private PhotoProcessor photoProcessor;

	private bool screenNotionState;
	private bool notifierState;

	void Awake ()
	{
		photoProcessor = GetComponent<PhotoProcessor>();
	}

	/// <summary>
	/// Find all the sensitive and interactable Objects in the camera view and assgin them to the list values in the script.
	/// </summary>
	private void SearchForObjects()
	{
		List<GameObject> tempList = new List<GameObject>();
		allPickUpItems.Clear();
		allInteractableObjects.Clear();
		tempList = new List<GameObject>(GameObject.FindGameObjectsWithTag(Tags.PickUpItem));
		foreach (GameObject go in tempList)
		{
			if (!go.GetComponent<PickUpItem>())
				continue;
			allPickUpItems.Add(go.GetComponent<PickUpItem>());
		}
		tempList = new List<GameObject>(GameObject.FindGameObjectsWithTag(Tags.Interactable));
		foreach (GameObject go in tempList)
		{
			if (!go.GetComponent<InteractableObject>())
				continue;
			allInteractableObjects.Add(go.GetComponent<PickUpItem>());
		}
	}

	/// <summary>
	/// Set the lensType of the photoCamera.
	/// </summary>
	/// <param name="type">Type.</param>
	public void SetLens(Consts.LensType type)
	{
		LensType = type;
	}

	/// <summary>
	/// Find the active objects in the scene and assign them to the list values in the script.
	/// </summary>
	private void EvaluatePhotoObjects()
	{
		
	}

	/// <summary>
	/// Sets the state of the screen notion.
	/// </summary>
	/// <param name="state">If set to <c>true</c> state.</param>
	private void SetScreenNotionState(bool state)
	{
		
	}

	/// <summary>
	/// Sets the state of the screen notifiers.
	/// </summary>
	/// <param name="state">If set to <c>true</c> state.</param>
	private void SetNotifierState(bool state)
	{
		
	}

	/// <summary>
	/// Takes the photo. This coroutine encapsulates all the process of taking photograph.
	/// </summary>
	/// <returns>The photo.</returns>
	public IEnumerator TakePhoto ()
	{
		yield return null;
	}

	private IEnumerator PlayTakeEffect ()
	{
		yield return null;
	}

	private IEnumerator PlaySwitchEffect()
	{
		yield return null;
	}
}
