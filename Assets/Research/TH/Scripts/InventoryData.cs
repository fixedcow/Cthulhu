using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TH.Core {

public class InventoryData
{
    #region PublicVariables
	public IReadOnlyList<InventoryItem> SlotList => _slotList.ToList().AsReadOnly();
	public bool HasModifiedThisFrame => _hasModifiedThisFrame;
	#endregion

	#region PrivateVariables
	private InventoryItem[] _slotList;
	private bool _hasModifiedThisFrame = true;
	#endregion

	#region PublicMethod
	public InventoryData(int maxItemNumber) {
		_slotList = new InventoryItem[maxItemNumber];
	}

	public void DataAccepted() {
		_hasModifiedThisFrame = false;
	}

	public bool HasSameItemType(int targetIdx, int compareIdx) {
		if (_slotList[targetIdx] == null || _slotList[compareIdx] == null)
			return false;
		return _slotList[targetIdx].TargetItem.ItemID == _slotList[compareIdx].TargetItem.ItemID;
	}

	public bool IsNull(int targetIdx) {
		return _slotList[targetIdx] == null;
	}

	public bool IsStackable(int targetIdx) {
		return _slotList[targetIdx].TargetItem.IsStackable;
	}

	public int SafeStackedItemNumber(int targetIdx) {
		if (_slotList[targetIdx] == null) {
			InventoryDataError("인벤토리에 존재하지 않는 슬롯에 접근하였습니다.");
			return 0;
		}
			
		return _slotList[targetIdx].StackedNumber;
	}

	public int StackedItemNumber(int targetIdx) {
		return _slotList[targetIdx] == null ? 0 : _slotList[targetIdx].StackedNumber;
	}

	public bool CanMergeItems(int targetIdx, int originalIdx) {
		 return 
		 	_slotList[targetIdx].StackedNumber + _slotList[originalIdx].StackedNumber 
		 	<= 
			_slotList[targetIdx].TargetItem.MaxStackableNumber;
	}

	public InventoryItem ExtractItem(int targetIdx) {
		if (IsNull(targetIdx)) {
			InventoryDataError("아이템이 존재하지 않는 슬롯에서 아이템을 추출하려고 합니다.");
			return null;
		}

		InventoryItem extractedItem = _slotList[targetIdx];
		_slotList[targetIdx] = null;

		_hasModifiedThisFrame = true;

		return extractedItem;
	}

	public int DecreaseItem(int targetIdx, int quantity, out ItemData itemData) {
		if (IsNull(targetIdx)) {
			InventoryDataError("아이템이 존재하지 않는 슬롯에서 아이템을 감소시키려고 합니다.");
			itemData = null;
			return 0;
		}

		if (IsStackable(targetIdx) == false) {
			InventoryDataError("스택 불가능한 아이템에 개수가 변경되었습니다.");
			itemData = null;
			return 0;
		}

		if (StackedItemNumber(targetIdx) < quantity) {
			int remain = StackedItemNumber(targetIdx);
			InventoryItem extractedItem = ExtractItem(targetIdx);
			itemData = extractedItem.TargetItem;

			_hasModifiedThisFrame = true;

			return remain;
		}

		_slotList[targetIdx].StackedNumber -= quantity;
		itemData = _slotList[targetIdx].TargetItem;

		_hasModifiedThisFrame = true;

		return quantity;
	}

	public int MaxStackableNumber(int targetIdx) {
		return _slotList[targetIdx].TargetItem.MaxStackableNumber;
	}

	public void AddNewItem(int targetIdx, InventoryItem item) {
		if (IsNull(targetIdx) == false) {
			InventoryDataError("이미 아이템이 존재하는 슬롯에 아이템을 추가하려고 합니다.");
			return;
		}

		_slotList[targetIdx] = item;

		_hasModifiedThisFrame = true;
	}

	public void AddToExistingItem(int targetIdx, int quantity) {
		if (IsNull(targetIdx)) {
			InventoryDataError("아이템이 존재하지 않는 슬롯에 아이템을 추가하려고 합니다.");
			return;
		}

		if (IsStackable(targetIdx) == false) {
			InventoryDataError("스택 불가능한 아이템에 개수가 변경되었습니다.");
			return;
		}

		if (StackedItemNumber(targetIdx) + quantity > MaxStackableNumber(targetIdx)) {
			InventoryDataError("아이템의 개수가 최대 개수를 초과하여 추가되었습니다.");
			return;
		}

		_slotList[targetIdx].StackedNumber += quantity;

		_hasModifiedThisFrame = true;
	}

	/// <summary>
	/// targetAIdx의 아이템을 targetBIdx의 아이템과 교체합니다.
	/// </summary>
	/// <param name="targetAIdx"></param>
	/// <param name="targetBIdx"></param>
	public void SwapItem(int targetAIdx, int targetBIdx) {
		InventoryItem temp = _slotList[targetAIdx];
		_slotList[targetAIdx] = _slotList[targetBIdx];
		_slotList[targetBIdx] = temp;

		_hasModifiedThisFrame = true;
	}

	/// <summary>
	/// fromIdx의 아이템을 toIdx로 이동합니다.
	/// </summary>
	/// <param name="fromIdx"></param>
	/// <param name="toIdx"></param>
	public void MoveItem(int fromIdx, int toIdx) {
		if (IsNull(toIdx) == false) {
			InventoryDataError("이동하려는 슬롯이 이미 차 있습니다.");
			return;
		}

		_slotList[toIdx] = _slotList[fromIdx];
		_slotList[fromIdx] = null;

		_hasModifiedThisFrame = true;
	}
	
	/// <summary>
	/// 주어진 두 슬롯의 아이템을 재분배합니다.
	/// </summary>
	/// <param name="targetAIdx"></param>
	/// <param name="targetBIdx"></param>
	/// <param name="newANum"></param>
	/// <param name="newBNum"></param>
	public void RedistributeItems(int targetAIdx, int targetBIdx, int newANum, int newBNum) {
		if (IsBothNull(targetAIdx, targetBIdx)) {
			InventoryDataError("재분배하려는 아이템이 존재하지 않습니다.");
			return;
		}

		if (IsBothNotNull(targetAIdx, targetBIdx)) {
			if (IsSameType(targetAIdx, targetBIdx) == false) {
				InventoryDataError("재분배하려는 아이템의 타입이 다릅니다.");
				return;
			}
		}
		
		if (StackedItemNumber(targetAIdx) + StackedItemNumber(targetBIdx) != newANum + newBNum) {
			InventoryDataError("재분배하려는 아이템의 개수가 잘못되었습니다.");
			return;
		}

		if (newANum > MaxStackableNumber(targetAIdx) || newBNum > MaxStackableNumber(targetBIdx)) {
			InventoryDataError("재분배하려는 아이템의 개수가 최대 개수보다 많습니다.");
			return;
		}

		if (IsNull(targetAIdx) == true) {
			if (newANum != 0) {
				_slotList[targetAIdx] = new InventoryItem(_slotList[targetAIdx].TargetItem, newANum);
			}
		} else {
			_slotList[targetAIdx].StackedNumber = newANum;
		}

		if (IsNull(targetBIdx) == true) {
			if (newBNum != 0) {
				_slotList[targetBIdx] = new InventoryItem(_slotList[targetBIdx].TargetItem, newBNum);
			}
		} else {
			_slotList[targetBIdx].StackedNumber = newBNum;
		}

		_hasModifiedThisFrame = true;
	}

	/// <summary>
	/// fromIdx의 아이템을 toIdx의 아이템으로 병합합니다.
	/// </summary>
	/// <param name="fromIdx"></param>
	/// <param name="toIdx"></param>
	public void MergeItems(int fromIdx, int toIdx) {
		if (IsNull(fromIdx)) {
			InventoryDataError("병합하려는 아이템이 존재하지 않습니다.");
			return;
		}

		if (IsSameType(fromIdx, toIdx) == false) {
			InventoryDataError("병합하려는 아이템의 타입이 다릅니다.");
			return;
		}

		if (CanMergeItems(fromIdx, toIdx) == false) {
			InventoryDataError("병합하려는 아이템의 개수가 최대 개수를 초과합니다.");
			return;
		}

		_slotList[toIdx].StackedNumber += _slotList[fromIdx].StackedNumber;
		_slotList[fromIdx] = null;

		_hasModifiedThisFrame = true;
	}
	#endregion
    
	#region PrivateMethod
	private void InventoryDataError(string msg) {
		if (InventorySystem.Instance.showErrorMsg) {
			Debug.LogError(msg);
		}
	}

	private bool IsBothNull(int targetAIdx, int targetBIdx) {
		return _slotList[targetAIdx] == null && _slotList[targetBIdx] == null;
	}

	private bool IsBothNotNull(int targetAIdx, int targetBIdx) {
		return _slotList[targetAIdx] != null && _slotList[targetBIdx] != null;
	}

	private bool IsAnyNull(int targetAIdx, int targetBIdx) {
		return _slotList[targetAIdx] == null || _slotList[targetBIdx] == null;
	}

	private bool IsSameType(int targetAIdx, int targetBIdx) {
		return _slotList[targetAIdx].TargetItem.ItemID == _slotList[targetBIdx].TargetItem.ItemID;
	}
	#endregion
}

}