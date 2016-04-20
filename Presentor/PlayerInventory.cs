using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;

public class InventoryState
{
	public LensItem lensItem;
	public Item item;
}

public class PlayerInventory : MonoBehaviour 
{
	public LensItem EquippedLens
	{
		get
		{
			return equippedLens;
		}
	}

	public Item EquippedItem
	{
		get
		{
			return equippedItem;
		}
	}

	private LensItem equippedLens;
	private Item equippedItem;

	private BackPack backPack;
	private LensPack lensPack;

	private static PlayerInventory instance;

	private static float UPDATE_FREQ;

	void Awake()
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

	void Start ()
	{
		//Start the corresponding coroutines here.
	}

	private static void LoadItems()
	{
		
	}

	private static IEnumerator CheckItemVarables()
	{
		while (true)
		{

			yield return new WaitForSeconds(UPDATE_FREQ);
		}
	}

	public static void ProcessPhotoedObjects (List<PhotoObject> objects)
	{
		
	}

	public static void EquipItem(Item item)
	{
		
	}

	/// <summary>
	/// Put the items in the list into the playerInventory and set them under the control of the invertory system.
	/// </summary>
	/// <param name="items">Items.</param>
	public static void AddNewItemList(List<Item> items)
	{
		
	}

	/// <summary>
	/// Just in case, a function which conveniently process a single item is also provided.
	/// </summary>
	/// <param name="item">Item.</param>
	public static void AddNewItem(Item item)
	{
		
	}

	/// <summary>
	/// This method is seldomly invoked since only up to 4 different lenses are provided in the game. However when a new lens is unlocked, call for this method. 
	/// </summary>
	/// <param name="type">Type.</param>
	public static void UnlockNewLens(Consts.LensType type)
	{
		
	}

	/// <summary>
	/// Use the Equipped Item. If no item is equipped nothing would be done. 
	/// </summary>
	public static void UseItem()
	{
		
	}

	/// <summary>
	/// Equip the indicated lens.
	/// </summary>
	/// <param name="lens">Lens.</param>
	public static void EquipLens(LensItem lens)
	{
		
	}
}
