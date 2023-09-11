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
	[SerializeField] protected RectTransform _slotContentTransform;
	[SerializeField] protected GameObject _inventoryUIPack;

	protected UIInventorySlot[] _slotList;
	protected UITrashSlot _trashSlot;
	protected GameObject _draggingSlot;

	protected Inventory _inventory;
	protected bool _isInventoryOpen = false;
	protected bool _hasInitialized = false;
	#endregion

	#region PublicMethod
	public virtual void Init(Inventory inventory) 
	{
		_inventory = inventory;

		CloseInventory();
	}

	public virtual void ToggleInventory() {
		if (_isInventoryOpen == true) {
			CloseInventory();
		} else {
			OpenInventory();
		}		
	}

	public virtual void CloseInventory() {
		if (_inventoryUIPack == null) {
			_inventoryUIPack = transform.Find("UIPack").gameObject;
		}

		_inventoryUIPack.SetActive(false);
	}

	public virtual void OpenInventory() {
		if (_inventoryUIPack == null) {
			_inventoryUIPack = transform.Find("UIPack").gameObject;
		}

		_inventoryUIPack.SetActive(true);
	}

	public void UpdateInventory(InventoryItem[] itemList) {
		if (_slotContentTransform == null) {
			_slotContentTransform = transform.Find("UIPack/SlotContents").GetComponent<RectTransform>();
		}

		GameObject slotPrefab = Resources.Load<GameObject>("Prefabs/UI/UIInventory/UIInventorySlot");
		
		if (_slotList == null || _slotList.Length != itemList.Length) {
			foreach (Transform child in _slotContentTransform) {
				Destroy(child.gameObject);
			}

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

		if (_hasInitialized == false) {
			_hasInitialized = true;
		}
	}
	#endregion
    
	#region PrivateMethod
	protected void Awake() 
	{
		_slotContentTransform = transform.Find("UIPack/SlotContents").GetComponent<RectTransform>();
		_trashSlot = GetComponentInChildren<UITrashSlot>();
		_trashSlot.Init(InventorySystem.TRASH_ITEM_ID, OnSelectedSlot, OnStartDragSlot, OnSlotPointerEnter);
	}

	protected virtual void Update() {
	}

	protected void OnSelectedSlot(int idx) 
	{
		if (InventorySystem.Instance.isDebugMode) {
			Debug.Log($"선택된 슬롯: {idx}");
		}
	}

	protected void OnStartDragSlot(int idx) 
	{
		if (InventorySystem.Instance.isDebugMode) {
			Debug.Log($"드래그 시작된 슬롯: {idx}");
		}
		InventorySystem.Instance.OnStartDragSlot(_inventory, _inventory.GetItem(idx) == null ? null : _inventory.GetItem(idx).TargetItem, idx);
	}

	protected void OnSlotPointerEnter(int idx) 
	{
		if (idx == InventorySystem.NULL_ITEM_ID) {
			InventorySystem.Instance.OnPointerExitItemSlot();
			return;
		}
		if (idx == InventorySystem.TRASH_ITEM_ID) {
			InventorySystem.Instance.OnPointerEnterItemSlot(_inventory, null, idx);
			return;
		}
		if (InventorySystem.Instance.isDebugMode) {
			Debug.Log($"슬롯에 마우스가 들어옴: {idx}");
		}
		InventorySystem.Instance.OnPointerEnterItemSlot(_inventory, _inventory.GetItem(idx) == null ? null : _inventory.GetItem(idx).TargetItem, idx);
	}
	#endregion
}

}