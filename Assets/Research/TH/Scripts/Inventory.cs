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
				appliedQuantity = Mathf.Min(initQuantity, item.MaxStackableNumber);
				_inventoryData.AddNewItem(slotIdx, new InventoryItem(item, appliedQuantity));
			} 
			else 
			{
				appliedQuantity = Mathf.Min(initQuantity, item.MaxStackableNumber - _inventoryData.StackedItemNumber(slotIdx));
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
			Debug.LogError("원래 아이템이 존재하지 않습니다.");
			return false;
		}

		// 대상 슬롯이 비어있는 경우
		if (_inventoryData.IsNull(targetIdx)) {
			_inventoryData.MoveItem(originalIdx, targetIdx);
			return true;
		}

		// 대상 슬롯의 아이템이 다른 경우
		if (_inventoryData.HasSameItemType(targetIdx, originalIdx) == false) {
			_inventoryData.SwapItem(targetIdx, originalIdx);
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
			return true;
		}
	}

	public InventoryItem GetItem(int idx) 
	{
		return _inventoryData.SlotList[idx];
	}

	public bool HasInventoryDataChanged() 
	{
		return _inventoryData.HasModifiedThisFrame;
	}

	public InventoryItem[] GetInventoryItemsForUI() {
		_inventoryData.DataAccepted();
		return _inventoryData.SlotList.ToArray();
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
				Debug.LogError("Stackable이 아닌 아이템에 대해 1개 이상의 아이템을 추가하려고 합니다.");
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

				if (slotList[i].StackedNumber < item.MaxStackableNumber) 
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

