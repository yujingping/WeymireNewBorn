using UnityEngine;
using System.Collections;

public abstract class Item : MonoBehaviour 
{
	public int Index
	{
		get
		{
			return index;
		}
	}

	public string Name
	{
		get
		{
			return name;
		}
	}

	public string ModelName
	{
		get
		{
			return modelName;
		}
	}

	public string Introduction
	{
		get
		{
			return introduction;
		}
	}

	private int index;
	private string name;
	private string modelName;
	private string introduction;

	public virtual void EquipItem()
	{
		
	}

	public virtual void UseItem()
	{
		
	}
}
