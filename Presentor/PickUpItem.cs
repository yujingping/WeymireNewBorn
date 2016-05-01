using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class PickUpItem : PhotoObject 
{
	/// <summary>
	/// The Corresponding index of the item it represents. Essential to determine an item in the database.
	/// </summary>
	public int itemIndex;

	/// <summary>
	/// The Override Function of the PhotoTaken in the Base Abstract Class.
	/// </summary>
	public override void PhotoTaken (InventoryState state)
	{
		//To be implemented : 
		//Call for the PlayerInventory to Insert the item. 
		//Play effect.
	}

	/// <summary>
	/// The Notification must be based on the lens that is currently equipped. 
	/// </summary>
	/// <param name="screenPosition">Screen position.</param>
	/// <param name="state">State.</param>
	public override bool NotifyObjectOnScreen (InventoryState state, Vector2 screenPosition, float maxDistance)
	{
		return true;
	}


	public override bool DetermineAvailability (InventoryState state, Vector2 screenPosition, float maxDistance)
	{
		return true;
	}
}

