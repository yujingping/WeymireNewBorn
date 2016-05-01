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
	/// Notifies the object on screen based on the screen position transferred to the function. Return a bool value
	/// To Represent whether this object could be taken or not.
	/// </summary>
	/// <param name="screenPosition">Screen position.</param>
	public abstract bool NotifyObjectOnScreen (InventoryState state, Vector2 screenPosition, float maxDistance);
		
	/// <summary>
	/// Determines whether this object could be taken under the current inventoryState and the angle of the camera.
	/// </summary>
	/// <returns><c>true</c>, if availability was determined, <c>false</c> otherwise.</returns>
	/// <param name="">.</param>
	/// <param name="screenPosition">Screen position.</param>
	/// <param name="maxDistance">Max distance.</param>
	public abstract bool DetermineAvailability (InventoryState state, Vector2 screenPosition, float maxDistance);
}
