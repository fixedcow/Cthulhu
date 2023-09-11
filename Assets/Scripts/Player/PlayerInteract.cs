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
	private PlayerItemHandler _handler;
	#endregion

	#region PublicMethod
	public void Interact()
	{
		ITargetable target = _target.GetTarget();
		if (target is IHittable)
		{
			IInteractable targetInteract = target as IInteractable;
			int currentIndex = _handler.GetCurrentInventoryIndex();
            if (currentIndex != -1)
            {
				targetInteract.Interact(_handler.GetCurrentInventoryIndex());
			}
		}
	}
	#endregion

	#region PrivateMethod
	#endregion
}
