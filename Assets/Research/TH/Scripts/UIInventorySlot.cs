using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
		#endregion

        #region PublicMethod
		public void Init(Action<int> onSelectedCallback, Action<int> onStartDragCallback, Action<int> onPointerEnterCallback) {
			_onSelectedCallback = onSelectedCallback;
			_onStartDragCallback = onStartDragCallback;
			_onPointerEnterCallback = onPointerEnterCallback;

			SetSlot(true, -1, null);
		}

		public void Init(int idx, ItemData itemData, Action<int> onSelectedCallback, Action<int> onStartDragCallback, Action<int> onPointerEnterCallback) 
		{
			_onSelectedCallback = onSelectedCallback;
			_onStartDragCallback = onStartDragCallback;
			_onPointerEnterCallback = onPointerEnterCallback;

			SetSlot(false, idx, itemData);
		}

		public void SetSlot(bool isNull, int idx, ItemData itemData) {
			if (_itemImage == null) {
				_itemImage = transform.Find("Item Image").GetComponent<Image>();
			}
			
			_slotIdx = idx;
			_isNull = isNull;
			if (isNull) {
				_itemImage.color = Color.clear;
				_itemImage.sprite = null;
			} else {
				_itemImage.color = Color.white;
				_itemImage.sprite = itemData.itemSprite;
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
				if (Time.time - _selectedTime > 0.5f) {
					_isPointerDown = false;
					_onStartDragCallback(_slotIdx);
				}
			}
		}
        #endregion
    }

}