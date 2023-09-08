using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMove))]
public class PlayerItemGetter : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private float _pickedRadius;
	[SerializeField] private float _pickedSpeed;
	#endregion

	#region PublicMethod   
	public void PickItem()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _pickedRadius, 1 << LayerMask.NameToLayer("Item"));
		foreach(Collider2D collider in colliders)
		{
			DroppedItem item;
			collider.TryGetComponent(out item);
			if(item != null)
			{
				item.PickedBy(_pickedSpeed);
			}
		}
	}
	#endregion

	#region PrivateMethod
	#endregion
}
