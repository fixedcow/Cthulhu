using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable : ITargetable
{
	public void Interact(int inventoryIndex);
}
