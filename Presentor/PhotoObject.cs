using UnityEngine;
using System.Collections;

public abstract class PhotoObject : MonoBehaviour 
{
	public string objectName;
	public string objectIntroduction;

	/// <summary>
	/// Defines the behaviour after the photo is successfully taken.
	/// </summary>
	/// <param name="state">State.</param>
	public virtual void PhotoTaken (InventoryState state)
	{
		
	}

	/// <summary>
	/// Notifies the object on screen based on the screen position transferred to the function. 
	/// </summary>
	/// <param name="screenPosition">Screen position.</param>
	public virtual void NotifyObjectOnScreen (InventoryState state, Vector2 screenPosition, float maxDistance)
	{
		
	}
}
