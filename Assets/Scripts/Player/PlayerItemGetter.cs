using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMove))]
public class PlayerItemGetter : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private float pickedRadius;
	[SerializeField] private float pickedSpeed;
	#endregion

	#region PublicMethod   
	public void PickItem()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pickedRadius, 1 << LayerMask.NameToLayer("Item"));
		foreach(Collider2D collider in colliders)
		{
			DroppedItem item;
			collider.TryGetComponent(out item);
			if(item != null)
			{
				item.PickedBy(this, pickedSpeed);
			}
		}
	}
	#endregion

	#region PrivateMethod
	#endregion
}
