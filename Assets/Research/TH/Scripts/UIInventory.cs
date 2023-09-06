using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core {

public class UIInventory : MonoBehaviour
{
    #region PublicVariables
	#endregion

	#region PrivateVariables
	private UIInventorySlot[] _slotList;
	private RectTransform _slotContentTransform;
	private int _maxItemNumber;
	#endregion

	#region PublicMethod
	public void Init(int maxItemNumber)
	{
		_maxItemNumber = maxItemNumber;
	}

	public void UpdateInventory(InventoryItem[] itemList) {
		GameObject slotPrefab = Resources.Load<GameObject>("UI/UIInventorySlot");
		
		if (_slotList == null || _slotList.Length != itemList.Length) {
			_slotList = new UIInventorySlot[itemList.Length];
		}

		int i;
		for (i = 0; i < itemList.Length; i++) {
			if (_slotList[i] == null) {
				_slotList[i] = Instantiate(slotPrefab, transform).GetComponent<UIInventorySlot>();
				if (itemList[i].TargetItem != null)
					_slotList[i].Init(i, itemList[i].TargetItem, OnSelectedSlot, OnStartDragSlot);
				else 
					_slotList[i].Init(OnSelectedSlot, OnStartDragSlot);
			} else {
				_slotList[i].SetSlot(itemList[i].TargetItem == null, i, itemList[i].TargetItem);
			}
		}
	}
	#endregion
    
	#region PrivateMethod
	private void Awake() 
	{
		_slotContentTransform = transform.GetChild(0).GetComponent<RectTransform>();
	}

	private void OnSelectedSlot(int idx) 
	{
		Debug.Log($"선택된 슬롯: {idx}");
	}

	private void OnStartDragSlot(int idx) 
	{
		Debug.Log($"드래그 시작된 슬롯: {idx}");
	}
	#endregion
}

}