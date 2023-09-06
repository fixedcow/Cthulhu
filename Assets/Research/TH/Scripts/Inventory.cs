using System.Collections;
using System.Collections.Generic;
using UnityEngine;
	
namespace TH.Core {

public class Inventory : MonoBehaviour
{
    #region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private int _maxItemNumber;
	private InventoryItem[] _slotList;
	#endregion

	#region PublicMethod
	public void AddItem(ItemData item, int quantity) {

	}

	//public bool 
	#endregion
    
	#region PrivateMethod
	private void Awake()
	{
		// private 변수 초기화
		_slotList = new InventoryItem[_maxItemNumber];
	}

	// private int FindAvailableItemSlotIdx(ItemData item, int quantity) 
	// {
	// 	if (item.isStackable == false) 
	// 	{
	// 		if (quantity > 1) 
	// 		{
	// 			Debug.LogError("Stackable이 아닌 아이템에 대해 1개 이상의 아이템을 추가하려고 합니다.");
	// 			return -1;
	// 		}

	// 		for (int i = 0; i < _maxItemNumber; i++) 
	// 		{
	// 			if (_slotList[i] == null) 
	// 			{
	// 				return i;
	// 			}
	// 		}
	// 	} else {
	// 		for (int i = 0; i < _maxItemNumber; i++) 
	// 		{
	// 			if (_slotList[i] == null || _slotList[i].TargetItem == null) { continue; }

	// 			if (_slotList[i].TargetItem.ItemID != item.ItemID) { continue; }

	// 			if (_slotList[i].StackedNumber + quantity <= item.maxStackableNumber) 
	// 			{
	// 				return i;
	// 			} else {
	// 				continue;
	// 			}
	// 		}
	// 	}
	// }
	#endregion
}

}

