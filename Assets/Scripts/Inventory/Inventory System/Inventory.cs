using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
	
namespace TH.Core {

public class Inventory : MonoBehaviour
{
    #region PublicVariables
	public bool HasInitialized => _hasInitialized;
	public int MaxItemNumber => _maxItemNumber;
	public int SelectedItemIdx => _selectedItemIdx;
	#endregion

	#region PrivateVariables
	[SerializeField] private int _maxItemNumber = 2;

	private InventoryData __inventoryData;
	private InventoryData _inventoryData {
		get {
			if (__inventoryData == null) {
				__inventoryData = new InventoryData(_maxItemNumber);
			}
			return __inventoryData;
		}

		set {
			__inventoryData = value;
		}
	}

	private bool _hasInitialized = false;
	private int _selectedItemIdx = -1;
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
			if (InventorySystem.Instance.showErrorMsg) {
				Debug.Log("아이템을 추가할 수 있는 슬롯이 없습니다.");
			}
			return 0;
		}

		if (item.IsStackable == false) 
		{
			_inventoryData.AddNewItem(slotIdx, new InventoryItem(item, 1));
			return 1;
		}
		
		int appliedQuantity, initQuantity;
		for (initQuantity = quantity; initQuantity > 0 && slotIdx != -1; initQuantity -= appliedQuantity) 
		{
			if (_inventoryData.IsNull(slotIdx)) 
			{
				appliedQuantity = Mathf.Min(initQuantity, WorldManager.Instance.GetItemData(item.ItemID).MaxStackableNumber);
				_inventoryData.AddNewItem(slotIdx, new InventoryItem(item, appliedQuantity));
			} 
			else 
			{
				appliedQuantity = Mathf.Min(initQuantity, WorldManager.Instance.GetItemData(item.ItemID).MaxStackableNumber - _inventoryData.StackedItemNumber(slotIdx));
				_inventoryData.AddToExistingItem(slotIdx, appliedQuantity);
			}
			slotIdx = FindAvailableItemSlotIdx(item, quantity);
		}
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
		if (_inventoryData.IsNull(originalIdx)) {
			if (InventorySystem.Instance.showErrorMsg) {
				Debug.LogError("원래 아이템이 존재하지 않습니다.");
			}
			return false;
		}

		// 대상 슬롯이 비어있는 경우
		if (_inventoryData.IsNull(targetIdx)) {
			_inventoryData.MoveItem(originalIdx, targetIdx);
			if (originalIdx == _selectedItemIdx) {
				_selectedItemIdx = -1;
			}
			return true;
		}

		// 대상 슬롯의 아이템이 다른 경우
		if (_inventoryData.HasSameItemType(targetIdx, originalIdx) == false) {
			_inventoryData.SwapItem(targetIdx, originalIdx);
			if (originalIdx == _selectedItemIdx) {
				_selectedItemIdx = -1;
			}
			return true;
		}

		// 대상 슬롯의 아이템이 같은 경우
		if (_inventoryData.CanMergeItems(targetIdx, originalIdx) == false) {
			int remain = 
				_inventoryData.SafeStackedItemNumber(targetIdx) 
				+ _inventoryData.SafeStackedItemNumber(originalIdx) 
				- _inventoryData.MaxStackableNumber(targetIdx);
			
			_inventoryData.RedistributeItems(targetIdx, originalIdx, _inventoryData.MaxStackableNumber(targetIdx), remain);
			return true;
		} else {
			_inventoryData.MergeItems(originalIdx, targetIdx);
			if (originalIdx == _selectedItemIdx) {
				_selectedItemIdx = -1;
			}
			return true;
		}
	}

	public InventoryItem DeleteItem(int targetIdx) {
		if (_inventoryData.IsNull(targetIdx)) {
			if (InventorySystem.Instance.showErrorMsg) {
				Debug.LogError("원래 아이템이 존재하지 않습니다.");
			}
			return null;
		}

		InventoryItem inventoryItem = _inventoryData.ExtractItem(targetIdx);
		if (targetIdx == _selectedItemIdx) {
			_selectedItemIdx = -1;
		}
		return inventoryItem;
	}

	public int DeleteItem(int targetIdx, int quantity, out ItemData targetItemData) 
	{
		if (_inventoryData.IsNull(targetIdx)) {
			if (InventorySystem.Instance.showErrorMsg) {
				Debug.LogError("원래 아이템이 존재하지 않습니다.");
			}
			targetItemData = null;
			return 0;
		}

		int deleted = _inventoryData.DecreaseItem(targetIdx, quantity, out targetItemData);
		return deleted;
	}

	public InventoryItem GetItem(int idx) 
	{
		return _inventoryData.SlotList[idx];
	}

	public bool HasInventoryDataChanged() 
	{
		return _inventoryData.HasModifiedThisFrame;
	}

	public bool IsItemAvailableToInventory(ItemData item, int quantity) {
		return FindAvailableItemSlotIdx(item, quantity) != -1;
	}

	public InventoryItem[] GetInventoryItemsForUI() {
		_inventoryData.DataAccepted();
		return _inventoryData.SlotList.ToArray();
	}

	public void SelectNextItem() {
		for (int i = -1; i < _maxItemNumber; i++) {
			_selectedItemIdx++;
			if (_selectedItemIdx == _maxItemNumber) {
				_selectedItemIdx = -1;

				InventorySystem.Instance.GetInventoryOwner(this).OnSelectItem(_selectedItemIdx);
				break;
			}
			if (_inventoryData.IsNull(_selectedItemIdx)) {
				continue;
			}
			
			InventorySystem.Instance.GetInventoryOwner(this).OnSelectItem(_selectedItemIdx);
			break;
		}
		Debug.Log(SelectedItemIdx);
	}

	public void SelectPreviousItem()
	{
        for (int i = _maxItemNumber; i > 0; i--)
        {
            if (_selectedItemIdx == -1)
            {
                _selectedItemIdx = _maxItemNumber - 1;
            } else if (_selectedItemIdx == 0)
			{
                _selectedItemIdx = -1;

                InventorySystem.Instance.GetInventoryOwner(this).OnSelectItem(_selectedItemIdx);
                break;
            } else
			{
                _selectedItemIdx--;
            }
			
            if (_inventoryData.IsNull(_selectedItemIdx))
            {
                continue;
            }

            InventorySystem.Instance.GetInventoryOwner(this).OnSelectItem(_selectedItemIdx);
            break;
        }
        Debug.Log(SelectedItemIdx);
        }

	public void SelectItemIdx(int idx) {
		if (idx > _maxItemNumber - 1) {
			if (InventorySystem.Instance.showErrorMsg) {
				Debug.LogError("인벤토리 인덱스 오류");
			}
			return;
		}

		if (_selectedItemIdx == idx) {
			_selectedItemIdx = -1;
			InventorySystem.Instance.GetInventoryOwner(this).OnSelectItem(_selectedItemIdx);
			return;
		}

		if (_inventoryData.IsNull(idx)) {
			return;
		}

		_selectedItemIdx = idx;
		InventorySystem.Instance.GetInventoryOwner(this).OnSelectItem(_selectedItemIdx);
	}

	public void ExpandInventory(int expandNumber) {
		_maxItemNumber += expandNumber;
		_inventoryData.ExpandInventory(expandNumber);
	}
	#endregion
    
	#region PrivateMethod
	private void Awake()
	{
		// private 변수 초기화
		
	}

	private void Update() 
	{
		if (_hasInitialized == false) 
		{
			_hasInitialized = true;

			_inventoryData = new InventoryData(_maxItemNumber);
		}
    }

	private int FindAvailableItemSlotIdx(ItemData item, int quantity) 
	{
		IReadOnlyList<InventoryItem> slotList = _inventoryData.SlotList;

		if (item.IsStackable == false) 
		{
			if (quantity > 1) 
			{
				if (InventorySystem.Instance.showErrorMsg) {
					Debug.LogError("Stackable이 아닌 아이템에 대해 1개 이상의 아이템을 추가하려고 합니다.");
				}
				return -1;
			}

			for (int i = 0; i < _maxItemNumber; i++) 
			{
				if (slotList[i] == null) 
				{
					return i;
				}
			}
		} else {
			for (int i = 0; i < _maxItemNumber; i++) 
			{
				if (slotList[i] == null) { return i; }

				if (slotList[i].TargetItem.ItemID != item.ItemID) { continue; }

				if (slotList[i].StackedNumber < WorldManager.Instance.GetItemData(item.ItemID).MaxStackableNumber) 
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

