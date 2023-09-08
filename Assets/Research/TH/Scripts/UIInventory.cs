using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TH.Core {

public class UIInventory : MonoBehaviour
{
    #region PublicVariables
	#endregion

	#region PrivateVariables
	private UIInventorySlot[] _slotList;
	[SerializeField] private RectTransform _slotContentTransform;
	private GameObject _draggingSlot;

	private Inventory _inventory;
	#endregion

	#region PublicMethod
	public void Init(Inventory inventory) 
	{
		_inventory = inventory;
	}

	public void UpdateInventory(InventoryItem[] itemList) {
		if (_slotContentTransform == null) {
			_slotContentTransform = transform.Find("SlotContents").GetComponent<RectTransform>();
		}

		GameObject slotPrefab = Resources.Load<GameObject>("UI/UIInventorySlot");
		
		if (_slotList == null || _slotList.Length != itemList.Length) {
			_slotList = new UIInventorySlot[itemList.Length];
		}

		int i;
		for (i = 0; i < itemList.Length; i++) {
			if (_slotList[i] == null) {
				_slotList[i] = Instantiate(slotPrefab, _slotContentTransform).GetComponent<UIInventorySlot>();
				if (itemList[i] == null || itemList[i].TargetItem == null)
					_slotList[i].Init(i, OnSelectedSlot, OnStartDragSlot, OnSlotPointerEnter);
				else
					_slotList[i].Init(i, itemList[i], OnSelectedSlot, OnStartDragSlot, OnSlotPointerEnter);
					
			} else {
				_slotList[i].SetSlot(itemList[i] == null, i, itemList[i]);
			}
		}
	}
	#endregion
    
	#region PrivateMethod
	private void Awake() 
	{
		_slotContentTransform = transform.Find("SlotContents").GetComponent<RectTransform>();
	}

	private void Update() {
	}

	private void OnSelectedSlot(int idx) 
	{
		Debug.Log($"선택된 슬롯: {idx}");
	}

	private void OnStartDragSlot(int idx) 
	{
		Debug.Log($"드래그 시작된 슬롯: {idx}");
		InventorySystem.Instance.OnStartDragSlot(_inventory, _inventory.GetItem(idx) == null ? null : _inventory.GetItem(idx).TargetItem, idx);
	}

	private void OnSlotPointerEnter(int idx) 
	{
		if (idx == -1) {
			InventorySystem.Instance.OnPointerExitItemSlot();
			return;
		}
		Debug.Log($"슬롯에 마우스가 들어옴: {idx}");
		InventorySystem.Instance.OnPointerEnterItemSlot(_inventory, _inventory.GetItem(idx) == null ? null : _inventory.GetItem(idx).TargetItem, idx);
	}
	#endregion
}

}