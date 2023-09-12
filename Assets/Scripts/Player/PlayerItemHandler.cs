using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TH.Core;
using UnityEngine;
using TMPro;

public class PlayerItemHandler : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private SpriteRenderer _sr;
	private Sequence _handleSeq;
	private Inventory _inventory;
	private TextMeshPro _quantityText;
	/// <summary>
	/// 인벤토리에 선택한 개체가 없을 경우 -1 반환.
	/// </summary>
	private int _inventoryIndex = -1;

	private bool _handleSomething;
	#endregion

	#region PublicMethod
	public int GetCurrentInventoryIndex() => _inventoryIndex;
	public bool IsHandleSomething() => _handleSomething;
	public void HandleItem(int inventoryIndex)
	{
		if(inventoryIndex != -1)
		{
			PutOut(inventoryIndex);
		}
		else
		{
			PutIn();
		}
	}
	public void PutOut(int inventoryIndex)
	{
		_inventoryIndex = inventoryIndex;
		InventoryItem item = GetInventory().GetItem(_inventoryIndex);
		_sr.sprite = item.TargetItem.ItemSprite;
		if(item.StackedNumber > 1)
		{
			_quantityText.text = "x" + item.StackedNumber.ToString();
		}
		else
		{
			_quantityText.text = "";
		}
		_handleSomething = true;
		_handleSeq.Restart();
	}
	public void PutIn()
	{
		_quantityText.text = "";
		_inventoryIndex = -1;
		_sr.sprite = null;
		_handleSomething = false;
	}
	public void SetDirectionX(Vector3 dir)
	{
		transform.localScale = dir;
	}
	#endregion

	#region PrivateMethod
	private void Awake()
	{
		TryGetComponent(out _sr);
		transform.Find("Quantity").TryGetComponent(out _quantityText);
		_handleSeq = DOTween.Sequence()
			.SetAutoKill(false)
			.Pause()
			.Append(transform.DOLocalMoveY(0.6f, 0.5f))
			.SetLoops(-1, LoopType.Yoyo);
	}

	private void Update() {
		if (_inventoryIndex != -1 && Input.GetKeyDown(KeyCode.E)) {
			InventoryItem item = GetInventory().GetItem(_inventoryIndex);
			if (item.TargetItem.ItemID == "Berry") {
				GameManager.Instance.GetPlayer().HealHealth(10 * item.StackedNumber);
				GetInventory().DeleteItem(_inventoryIndex);
				PutIn();
			}
		}
	}

	private Inventory GetInventory()
	{
		_inventory ??= InventorySystem.Instance.GetInventory(GetComponentInParent<InventoryOwner>());
		return _inventory;
	}
	#endregion
}
