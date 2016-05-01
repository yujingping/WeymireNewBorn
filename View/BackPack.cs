using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackPack : MonoBehaviour 
{
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
}
