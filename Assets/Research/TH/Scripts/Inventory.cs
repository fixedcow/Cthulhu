using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
	
namespace TH.Core {

public class Inventory : MonoBehaviour
{
    #region PublicVariables
	public InventoryItem[] SlotList => _slotList;
	#endregion

	#region PrivateVariables
	[SerializeField] private int _maxItemNumber;
	private UIInventory _uiInventory;
	private bool _hasInitialized = false;
	private InventoryItem[] _slotList;
	#endregion

	#region PublicMethod
	/// <summary>
	/// 인벤토리의 적절한 자리에 아이템을 추가합니다.
	/// </summary>
	/// <param name="item"></param>
	/// <param name="quantity"></param>
	/// <returns>인벤토리에 수용된 아이템의 개수를 반환합니다. (모두 수용되었는지 검사 필요)</returns>
	[Button("AddItem")]
	public int AddItem(ItemData item, int quantity) {
		int slotIdx = FindAvailableItemSlotIdx(item, quantity);
		if (slotIdx == -1) 
		{
			Debug.Log("아이템을 추가할 수 있는 슬롯이 없습니다.");
			_uiInventory.UpdateInventory(_slotList);
			return 0;
		}

		if (item.isStackable == false) 
		{
			_slotList[slotIdx] = new InventoryItem(item, 1);
			_uiInventory.UpdateInventory(_slotList);
			return 1;
		}
		
		int appliedQuantity, initQuantity;
		for (initQuantity = quantity; initQuantity > 0 && slotIdx != -1; initQuantity -= appliedQuantity) 
		{
			if (_slotList[slotIdx] == null) 
			{
				appliedQuantity = Mathf.Min(initQuantity, item.maxStackableNumber);
				_slotList[slotIdx] = new InventoryItem(item, appliedQuantity);
			} 
			else 
			{
				appliedQuantity = Mathf.Min(initQuantity, item.maxStackableNumber - _slotList[slotIdx].StackedNumber);
				_slotList[slotIdx].StackedNumber += appliedQuantity;
			}
			slotIdx = FindAvailableItemSlotIdx(item, quantity);
		}
		_uiInventory.UpdateInventory(_slotList);
		return quantity - initQuantity;
	}

	/// <summary>
	/// 인벤토리의 해당 index 아이템을 인자로 주어진 아이템으로 대체합니다.
	/// </summary>
	/// <param name="item"></param>
	/// <param name="quantity"></param>
	/// <returns>Swap이 성공했는 지 여부를 반환합니다.</returns>
	public bool SwapItem(int targetIdx, int originalIdx) 
	{
		if (_slotList[originalIdx] == null) {
			Debug.LogError("원래 아이템이 존재하지 않습니다.");
			_uiInventory.UpdateInventory(_slotList);
			return false;
		}

		// 대상 슬롯이 비어있는 경우
		if (_slotList[targetIdx] == null) {
			_slotList[targetIdx] = _slotList[originalIdx];
			_slotList[originalIdx] = null;
			_uiInventory.UpdateInventory(_slotList);
			return true;
		}

		// 대상 슬롯의 아이템이 다른 경우
		if (_slotList[targetIdx].TargetItem.ItemID != _slotList[originalIdx].TargetItem.ItemID) {
			InventoryItem temp = _slotList[targetIdx];
			_slotList[targetIdx] = _slotList[originalIdx];
			_slotList[originalIdx] = temp;
			_uiInventory.UpdateInventory(_slotList);
			return true;
		}

		// 대상 슬롯의 아이템이 같은 경우
		if (_slotList[targetIdx].StackedNumber + _slotList[originalIdx].StackedNumber > _slotList[targetIdx].TargetItem.maxStackableNumber) {
			int remain = _slotList[targetIdx].StackedNumber + _slotList[originalIdx].StackedNumber - _slotList[targetIdx].TargetItem.maxStackableNumber;
			_slotList[targetIdx].StackedNumber = _slotList[targetIdx].TargetItem.maxStackableNumber;
			_slotList[originalIdx].StackedNumber = remain;
			_uiInventory.UpdateInventory(_slotList);
			return true;
		} else {
			_slotList[targetIdx].StackedNumber += _slotList[originalIdx].StackedNumber;
			_slotList[originalIdx] = null;
			_uiInventory.UpdateInventory(_slotList);
			return true;
		}
	}

	public InventoryItem GetItem(int idx) 
	{
		return _slotList[idx];
	}
	#endregion
    
	#region PrivateMethod
	private void Awake()
	{
		// private 변수 초기화
		_slotList = new InventoryItem[_maxItemNumber];
		_uiInventory = GameObject.FindAnyObjectByType<UIInventory>();
	}

	private void Update() 
	{
		if (_hasInitialized == false) 
		{
			_hasInitialized = true;

			_uiInventory.Init(_maxItemNumber, this);
			_uiInventory.UpdateInventory(_slotList);
		}
	}

	private int FindAvailableItemSlotIdx(ItemData item, int quantity) 
	{
		if (item.isStackable == false) 
		{
			if (quantity > 1) 
			{
				Debug.LogError("Stackable이 아닌 아이템에 대해 1개 이상의 아이템을 추가하려고 합니다.");
				return -1;
			}

			for (int i = 0; i < _maxItemNumber; i++) 
			{
				if (_slotList[i] == null) 
				{
					return i;
				}
			}
		} else {
			for (int i = 0; i < _maxItemNumber; i++) 
			{
				if (_slotList[i] == null) { return i; }

				if (_slotList[i].TargetItem.ItemID != item.ItemID) { continue; }

				if (_slotList[i].StackedNumber < item.maxStackableNumber) 
				{
					return i;
				}
			}
		}

		return -1;
	}
	#endregion
}

}

