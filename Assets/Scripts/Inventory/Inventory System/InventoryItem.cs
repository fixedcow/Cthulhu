using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core {

/// <summary>
/// 아이템 슬롯에 해당하는 아이템 클래스
/// </summary>
[Serializable]
public class InventoryItem
{
    #region PublicVariables
	public ItemData TargetItem => _targetItem;
	public int StackedNumber {
		get => _stackedNumber;
		set {
			if (value < 0) 
			{
				Debug.LogError("슬롯에 0보다 작은 개수의 아이템이 추가되었습니다.");
				return;
			}

			if (value > WorldManager.Instance.GetItemData(_targetItem.ItemID).MaxStackableNumber) 
			{
				Debug.LogError("슬롯에 최대 개수를 초과하여 아이템이 추가되었습니다.");
				return;
			}

			if (_targetItem.IsStackable == false) {
				Debug.LogError("스택 불가능한 아이템에 개수가 변경되었습니다.");
				return;
			}

			_stackedNumber = value;
		}
	}
	#endregion

	#region PrivateVariables
	private ItemData _targetItem;
	private int _stackedNumber;
	#endregion

	#region PublicMethod
	public InventoryItem(ItemData item, int quantity) 
	{
		_targetItem = item;
		_stackedNumber = quantity;
	}
	#endregion
    
	#region PrivateMethod
	#endregion
}

}