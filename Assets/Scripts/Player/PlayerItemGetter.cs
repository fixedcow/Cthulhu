using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TH.Core;
using System;

[RequireComponent(typeof(PlayerMove))]
public class PlayerItemGetter : InventoryOwner
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private float _pickedRadius;
	[SerializeField] private float _pickedSpeed;
	#endregion

	#region PublicMethod
	public override void AddItem(Func<ItemData, int, int> AddItemFunc)
	{
		base.AddItem(AddItemFunc);
	}
	public override void OnUseItem(ItemData item, int quantity)
	{
		base.OnUseItem(item, quantity);
	}
	public void PickItem()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _pickedRadius, 1 << LayerMask.NameToLayer("Item"));
		foreach(Collider2D collider in colliders)
		{
			DropItem item;
			collider.TryGetComponent(out item);
			if(item != null)
			{
				item.PickedBy(this, _pickedSpeed);
			}
		}
	}
	#endregion

	#region PrivateMethod
	#endregion
}
