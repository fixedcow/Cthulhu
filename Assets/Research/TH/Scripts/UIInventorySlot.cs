using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

namespace TH.Core {

    public class UIInventorySlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        #region PublicVariables
        #endregion

        #region PrivateVariables
        private int _slotIdx;
		private Action<int> _onSelectedCallback;
		private Action<int> _onStartDragCallback;
		private Action<int> _onPointerEnterCallback;
		private bool _isPointerDown;
		private float _selectedTime;
		private bool _isNull;
		private Image _itemImage;
		private TextMeshProUGUI _stackedNumberText;
		#endregion

        #region PublicMethod
		public void Init(int idx, Action<int> onSelectedCallback, Action<int> onStartDragCallback, Action<int> onPointerEnterCallback) {
			_onSelectedCallback = onSelectedCallback;
			_onStartDragCallback = onStartDragCallback;
			_onPointerEnterCallback = onPointerEnterCallback;

			SetSlot(true, idx, null);
		}

		public void Init(int idx, InventoryItem item, Action<int> onSelectedCallback, Action<int> onStartDragCallback, Action<int> onPointerEnterCallback) 
		{
			_onSelectedCallback = onSelectedCallback;
			_onStartDragCallback = onStartDragCallback;
			_onPointerEnterCallback = onPointerEnterCallback;

			SetSlot(false, idx, item);
		}

		public void SetSlot(bool isNull, int idx, InventoryItem item) {
			if (_itemImage == null) 
			{
				_itemImage = transform.Find("ItemImage").GetComponent<Image>();
			}
			if (_stackedNumberText == null) 
			{
				_stackedNumberText = transform.Find("StackedNumberText").GetComponent<TextMeshProUGUI>();
			}
			
			_slotIdx = idx;
			_isNull = isNull;
			if (isNull) {
				_itemImage.color = Color.clear;
				_itemImage.sprite = null;

				_stackedNumberText.text = "";
			} else {
				_itemImage.color = Color.white;
				_itemImage.sprite = item.TargetItem.ItemSprite;
			
				if (item.StackedNumber == 1) {
					_stackedNumberText.text = "";
				} else {
					_stackedNumberText.text = item.StackedNumber.ToString();
				}
			}
		}
		
		public void OnPointerDown(PointerEventData eventData)
        {
            _onSelectedCallback(_slotIdx);
			_selectedTime = Time.time;
			_isPointerDown = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isPointerDown = false;
        }

		public void OnPointerEnter(PointerEventData eventData)
        {
			_onPointerEnterCallback(_slotIdx);
        }

		public void OnPointerExit(PointerEventData eventData)
		{
			_onPointerEnterCallback(-1);
		}
        #endregion

        #region PrivateMethod
		private void Update() 
		{
			if (_isPointerDown) {
				if (Time.time - _selectedTime > InventorySystem.Instance.dragDelayTime) {
					_isPointerDown = false;
					_onStartDragCallback(_slotIdx);
				}
			}
		}
        #endregion
    }

}