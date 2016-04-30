using UnityEngine;
using System.Collections;

public class InteractableObject : PhotoObject 
{
	public int keyItemIndex;

	public override void PhotoTaken(InventoryState state)
	{
		if (state.item.Index == keyItemIndex)
		{
			PhotoSuccess();
		}
		else
		{
			PhotoFail();
		}
	}

	public virtual void PhotoSuccess ()
	{
		
	}

	public virtual void PhotoFail ()
	{
		
	}

	public override void NotifyObjectOnScreen (Vector2 screenPos)
	{
		
	}
}
