using UnityEngine;
using System.Collections;

public abstract class PhotoObject : MonoBehaviour 
{
	public string objectName;
	public string objectIntroduction;

	public virtual void PhotoTaken (InventoryState state)
	{
		
	}
}
