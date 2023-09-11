using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInteract : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private PlayerTarget _target;
	private PlayerItemHandler _itemHandler;
	#endregion

	#region PublicMethod
	public void Interact()
	{
		ITargetable target = _target.GetTarget();
		if (target is IInteractable)
		{
			IInteractable targetInteract = target as IInteractable;
			int currentIndex = _itemHandler.GetCurrentInventoryIndex();
            if (currentIndex != -1)
            {
				targetInteract.Interact(_itemHandler.GetCurrentInventoryIndex());
				_itemHandler.PutIn();
			}
		}
	}
	#endregion

	#region PrivateMethod
	private void Awake()
	{
		TryGetComponent(out _target);
		transform.Find("Item Handler").TryGetComponent(out _itemHandler);
	}
	#endregion
}
