using System.Collections;
using System.Collections.Generic;
using TH.Core;
using UnityEngine;

public class SalesBox : MonoBehaviour, IInteractable
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private Inventory _inventory;
	#endregion

	#region PublicMethod
	public void Sell(int inventoryIndex)
	{
		
	}
	public Vector2 GetPosition() => transform.position;
	/// <summary>
	/// index가 -1일 때는 호출하면 안 됨. 즉, 반드시 무언가를 캐릭터가 들고 있어야(선택해야) 함.
	/// 막아줄까 말까 고민하다가 버그 터질 때에는 터지는 게 차라리 나을 것 같아서 안 막아 줌.
	/// </summary>
	/// <param name="inventoryIndex"></param>
	public void Interact(int inventoryIndex)
	{
		InventoryItem item = GetInventory().GetItem(inventoryIndex);
		Debug.Log(item.TargetItem.Gold + " / " + item.StackedNumber);
		GameManager.Instance.AddGold(item.TargetItem.Gold * item.StackedNumber);
		GetInventory().DeleteItem(inventoryIndex);
	}
	#endregion

	#region PrivateMethod
	private Inventory GetInventory()
	{
		_inventory ??= InventorySystem.Instance.GetInventory(GameManager.Instance.GetPlayer().GetComponent<InventoryOwner>());
		return _inventory;
	}
	#endregion
}
