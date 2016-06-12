﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// It is still controversial whether the BackPack should be a grid or an abstract gameObject which is never deactivated but controls the grid. 
/// </summary>
public class BackPack : MonoBehaviour 
{
	/// <summary>
	/// The prefab of the itemView. 
	/// </summary>
	public GameObject prefab;

	/// <summary>
	/// The label which shows the name of the selected object.
	/// </summary>
	public UILabel nameLabel;

	/// <summary>
	/// The label which shows the introduction of the selected object. 
	/// </summary>
	public UILabel introductionLabel;

	/// <summary>
	/// The item view grid. It hosts all the itemView components in the system. 
	/// </summary>
	public UIGrid itemViewGrid;

	/// <summary>
	/// The button which controls equip. 
	/// </summary>
	public UIButton equipButton;

	/// <summary>
	/// The escape button. Used to leave the backPack page. 
	/// </summary>
	public UIButton escapeButton;

	/// <summary>
	/// The 3D object that displays the model of the selected item. 
	/// </summary>
	public GameObject objectShower;

	/// <summary>
	/// The default object which means no object is selected or the object is being loaded currently. This is a prefab. 
	/// </summary>
	public GameObject default3DPrefab;

	/// <summary>
	/// The GameObejcts of the items. Please note that the "Item" Attribute of the ItemView is the reference from PlayerInventory.
	/// </summary>
	private List <ItemView> items = new List <ItemView> ();

	/// <summary>
	/// The equipped Item. This is a reference-only;
	/// </summary>
	private ItemView equippedItem;

	/// <summary>
	/// The selected item. This is auto-initialized to the equippedItem when the backPack is opened. 
	/// </summary>
	private ItemView selectedItem;

	/// <summary>
	/// The center component of the backpack Grid. 
	/// </summary>
	private UICenterOnChild gridCenterComponent;

	/// <summary>
	/// Insert a list of items into the backPack. Invoked by PlayerInventory only.
	/// </summary>
	/// <param name="items">Items.</param>
	public void InsertItemList(List<Item> items)
	{
		
	}

	/// <summary>
	/// Insert a single item into the backPack. Invoked by PlayerInventory only.
	/// </summary>
	/// <param name="item">Item.</param>
	public void InsertItem(Item item)
	{
		
	}

	/// <summary>
	/// Show the detail of the item by invoking a new NGUI window.
	/// </summary>
	/// <param name="item">Item.</param>
	public void ShowItemDetail (Item item)
	{
		
	}

	/// <summary>
	/// Equip the selected item. This function is recommended to be invoked by the information detail window only.
	/// </summary>
	public void EquipItem()
	{
		//The parameter is omitted here. Because the item to be equipped is always definitely the one that is selected!
		equippedItem = selectedItem;
		PlayerInventory.EquipItem(selectedItem.item);
	}

	/// <summary>
	/// Update the Item List of the Backpack based on the condition of playerInventory. Do not call for this function too frequently --- it is inefficient!
	/// </summary>
	public void UpdateBackPackContent ()
	{
		//In order to avoid deleting elements while traversing the list
		List <Item> backPackItemInfo = new List <Item>();
		List <Item> inventoryItems = PlayerInventory.GetItemList();
		List <Item> toDelete = new List <Item>();

		foreach (ItemView itemView in items)
		{
			Item item = itemView.item;
			if (!inventoryItems.Contains(item))
			{
				toDelete.Add(item);
			}
			else
				backPackItemInfo.Add(item);
		}

		foreach (Item item in inventoryItems)
		{
			if (!backPackItemInfo.Contains(item))
			{
				//This part only serves as a test.
				ItemView itemView = NGUITools.AddChild(itemViewGrid.gameObject, prefab).GetComponent <ItemView> ();
				itemView.item = item;
				items.Add(itemView);
				itemViewGrid.AddChild(itemView.transform);
				//Don't forget to change the parameters of the itemView here ... otherwise, galigeigei! 
			}
		}

		foreach (Item item in toDelete)
		{
			foreach (ItemView itemView in items)
			{
				if (itemView.item == item)
				{
					items.Remove (itemView);
					DestroyImmediate(itemView.gameObject);
					break;
				}
			}
		}

		//itemViewGrid.Reposition();
	}

	/// <summary>
	/// This function is called whenever the BackPack UI Component is active in the hiearchy.
	/// </summary>
	private void OnEnable ()
	{
		UpdateBackPackContent();
		//Debug.Log ("UpdateBackContent Function has been called!");
		//Later on, Don't forget to center on the equipped item! Also make sure to test the efficiency of this method.
	}

	/// <summary>
	/// This function is called whenever the BackPack UI Component is deactived in th hiearchy. 
	/// </summary>
	private void OnDisable ()
	{
		
	}

	private void Start ()
	{
		//It is guranteed that the itemViewGrid must have a UICenterOnChild Component!
		gridCenterComponent = itemViewGrid.GetComponent<UICenterOnChild>();
		gridCenterComponent.onCenter += OnSelectItem;
	}

	/// <summary>
	/// Raises the select item event. The parameter must be of class GameObject because it is pre-defined else-where. 
	/// </summary>
	/// <param name="itemView">Item view.</param>
	private void OnSelectItem (GameObject itemView)
	{
		//Determines the state of the Equip Button First!
		ItemView chosenItemView = itemView.GetComponent<ItemView> ();
		if (selectedItem == chosenItemView)
			return;
		if (equippedItem == chosenItemView)
		{
			equipButton.isEnabled = false;
		}
		else
		{
			equipButton.isEnabled = true;
		}

		selectedItem = chosenItemView;
		//Asynchoronously load the model and apply it to the objectShower. 
		string modelName = selectedItem.item.ModelName;
		StartCoroutine(LoadModel(modelName));
		ShowItemInformation();
	}

	/// <summary>
	/// The Coroutine which Asyncly loads the model and apply. 
	/// </summary>
	/// <returns>The model.</returns>
	/// <param name="modelName">Model name.</param>
	private IEnumerator LoadModel (string modelName)
	{
		Vector3 pos = objectShower.transform.position;
		DestroyImmediate(objectShower);
		#if UNITY_EDITOR
		Object loadedResource = Resources.Load(modelName, typeof(GameObject));
		objectShower = Instantiate(loadedResource as GameObject, pos, Quaternion.identity) as GameObject;
		yield return null;
		#else
		ResourceRequest requestState = Resources.LoadAsync(modelName);
		yield return requestState;
		objectShower = Instantiate (requestState.asset as GameObject, pos, Quaternion.identity) as GameObject;
		#endif
		if (!objectShower.GetComponent<BoxCollider>())
			objectShower.AddComponent<BoxCollider>();
		if (!objectShower.GetComponent<TestNGUISpinWithMouse>())
			objectShower.AddComponent<TestNGUISpinWithMouse>();
		objectShower.transform.parent = transform;
	}

	/// <summary>
	/// Shows the concise information of the selected object. 
	/// It is still under discussion ... The convertion from one item to another should have an animation or something ...  
	/// </summary>
	private void ShowItemInformation ()
	{
		string itemName = selectedItem.item.Name;
		string itemIntroduction = selectedItem.item.Introduction;
		nameLabel.text = itemName;
		introductionLabel.text = itemIntroduction;
	}
}
