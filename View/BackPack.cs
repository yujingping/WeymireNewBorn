using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// It is still controversial whether the BackPack should be a grid or an abstract gameObject which is never deactivated but controls the grid. 
/// </summary>
public class BackPack : UIGrid 
{
	/// <summary>
	/// The prefab of the itemView. 
	/// </summary>
	public GameObject prefab;

	/// <summary>
	/// The GameObejcts of the items. Please note that the "Item" Attribute of the ItemView is the reference from PlayerInventory.
	/// </summary>
	private List <ItemView> items = new List <ItemView> ();

	/// <summary>
	/// The equipped Item. This is a reference-only;
	/// </summary>
	private ItemView equippedItem;

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
	/// <param name="item">Item.</param>
	public void EquipItem(Item item)
	{
		
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
				ItemView itemView = NGUITools.AddChild(gameObject, prefab).GetComponent <ItemView> ();
				items.Add(itemView);
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

		Reposition();
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
}
