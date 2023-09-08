using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TH.Core
{

	public class InventorySystem : Singleton<InventorySystem>
	{
		#region PublicVariables
		[SerializeField] public bool isDebugMode = false;
		[SerializeField] public bool showErrorMsg = true;
		[SerializeField] public float dragDelayTime = 0.3f;
		[SerializeField] public InventoryPack[] inventoryPacks;

		public const int NULL_ITEM_ID = -1;
		public const int TRASH_ITEM_ID = -2;
		#endregion

		#region PrivateVariables
		[SerializeField] private DraggingItem _draggingItem;
		[SerializeField] private ItemInfoPanel _itemInfoPanel;

		private bool _isShowingItemInfoPanel = false;
		private bool _isDragging = false;
		private int _dragStartSlotIdx = NULL_ITEM_ID;
		private Inventory _dragStartInventory;

		private Inventory _pointedInventory;
		private int _pointedSlotIdx = NULL_ITEM_ID;
		#endregion

		#region PublicMethod
		public void OnPointerEnterItemSlot(Inventory inventory, ItemData item, int slotIdx)
		{
			if (isDebugMode)
			{
				Debug.Log($"슬롯 포인트: {slotIdx}");
			}

			_pointedInventory = inventory;
			_pointedSlotIdx = slotIdx;

			if (item == null)
			{
				return;
			}
			_itemInfoPanel.gameObject.SetActive(true);
			_isShowingItemInfoPanel = true;
			_itemInfoPanel.SetItemInfo(item);
		}

		public void OnPointerExitItemSlot()
		{
			_itemInfoPanel.gameObject.SetActive(false);
			_isShowingItemInfoPanel = false;
			_pointedInventory = null;
			_pointedSlotIdx = NULL_ITEM_ID;
		}

		public void OnStartDragSlot(Inventory inventory, ItemData item, int slotIdx)
		{
			if (item == null)
			{
				return;
			}

			_dragStartInventory = inventory;
			_dragStartSlotIdx = slotIdx;

			_draggingItem.gameObject.SetActive(true);
			_draggingItem.SetDraggingItem(item.ItemSprite);
			_isDragging = true;
		}

		public Inventory GetInventory(IOwnInventory inventoryOwner)
		{
			for (int i = 0; i < inventoryPacks.Length; i++)
			{
				if (inventoryPacks[i].inventoryOwner == inventoryOwner)
				{
					return inventoryPacks[i].inventory;
				}
			}
			return null;
		}

		public UIInventory GetUIInventory(IOwnInventory inventoryOwner)
		{
			for (int i = 0; i < inventoryPacks.Length; i++)
			{
				if (inventoryPacks[i].inventoryOwner == inventoryOwner)
				{
					return inventoryPacks[i].uiInventory;
				}
			}
			return null;
		}
		#endregion

		#region PrivateMethod
		protected override void Init()
		{
			base.Init();

			_draggingItem = FindObjectOfType<DraggingItem>();
			_itemInfoPanel = FindObjectOfType<ItemInfoPanel>();

			_draggingItem.gameObject.SetActive(false);
			_itemInfoPanel.gameObject.SetActive(false);

			for (int i = 0; i < inventoryPacks.Length; i++)
			{
				inventoryPacks[i].uiInventory.Init(inventoryPacks[i].inventory);
			}
		}

		protected void Update()
		{
			if (_isShowingItemInfoPanel == true)
			{

				_itemInfoPanel.UpdatePosition();
			}

			if (_isDragging)
			{
				_draggingItem.transform.position = Input.mousePosition;
				if (Input.GetMouseButtonUp(0))
				{
					if (_pointedSlotIdx != -1)
					{
						if (_pointedInventory == _dragStartInventory)
						{
							if (_pointedSlotIdx == InventorySystem.TRASH_ITEM_ID)
							{
								_draggingItem.GetComponent<RectTransform>().DOScale(0, 0.3f).OnComplete(() =>
								{
									_isDragging = false;
									_draggingItem.gameObject.SetActive(false);
									_draggingItem.GetComponent<RectTransform>().localScale = Vector3.one;
								}).SetEase(Ease.OutBack);
								_pointedInventory.DeleteItem(_dragStartSlotIdx);
								return;
							}

							_pointedInventory.SwapItem(_pointedSlotIdx, _dragStartSlotIdx);
						}
					}
					_isDragging = false;
					_draggingItem.gameObject.SetActive(false);
				}
			}

			for (int i = 0; i < inventoryPacks.Length; i++)
			{
				if (
					inventoryPacks[i].inventory.HasInitialized == true
					&& inventoryPacks[i].inventory.HasInventoryDataChanged() == true
				)
				{
					inventoryPacks[i].uiInventory.UpdateInventory(inventoryPacks[i].inventory.GetInventoryItemsForUI());
				}
			}
		}
		#endregion

		[Serializable]
		public class InventoryPack
		{
			public Inventory inventory;
			public UIInventory uiInventory;
			[SerializeField, SerializeReference]
			public IOwnInventory inventoryOwner;
		}
	}

}