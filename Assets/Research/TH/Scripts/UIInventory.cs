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
	private int _maxItemNumber;
	private GameObject _draggingSlot;
	private bool _isDragging = false;
	private int _dragStartSlotIdx;
	private int _pointedSlotIdx;

	private Inventory _inventory;
	#endregion

	#region PublicMethod
	public void Init(int maxItemNumber, Inventory inventory) 
	{
		_maxItemNumber = maxItemNumber;
		_inventory = inventory;
	}

	public void UpdateInventory(InventoryItem[] itemList) {
		GameObject slotPrefab = Resources.Load<GameObject>("UI/UIInventorySlot");
		
		if (_slotList == null || _slotList.Length != itemList.Length) {
			_slotList = new UIInventorySlot[itemList.Length];
		}

		int i;
		for (i = 0; i < itemList.Length; i++) {
			if (_slotList[i] == null) {
				_slotList[i] = Instantiate(slotPrefab, _slotContentTransform).GetComponent<UIInventorySlot>();
				if (itemList[i] == null || itemList[i].TargetItem == null)
					_slotList[i].Init(OnSelectedSlot, OnStartDragSlot, OnSlotPointerEnter);
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
		_slotContentTransform = transform.Find("MainInventory/SlotContents").GetComponent<RectTransform>();
		_draggingSlot = transform.Find("DraggingSlot").gameObject;
		_draggingSlot.SetActive(false);
		_isDragging = false;
	}

	private void Update() {
		if (_isDragging) {
			_draggingSlot.transform.position = Input.mousePosition;
			if (Input.GetMouseButtonUp(0)) {
				if(_pointedSlotIdx != -1)
				{
					_inventory.SwapItem(_pointedSlotIdx, _dragStartSlotIdx);
				}
				_isDragging = false;
				_draggingSlot.SetActive(false);
			}
		}
	}

	private void OnSelectedSlot(int idx) 
	{
		Debug.Log($"선택된 슬롯: {idx}");
	}

	private void OnStartDragSlot(int idx) 
	{
		Debug.Log($"드래그 시작된 슬롯: {idx}");
		_draggingSlot.SetActive(true);
		_dragStartSlotIdx = idx;
		_isDragging = true;
		_draggingSlot.transform.Find("Image").GetComponent<Image>().sprite = _inventory.GetItem(idx).TargetItem.itemSprite;
	}

	private void OnSlotPointerEnter(int idx) 
	{
		Debug.Log($"슬롯에 마우스가 들어옴: {idx}");
		_pointedSlotIdx = idx;
	}
	#endregion
}

}