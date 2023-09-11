using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using DG.Tweening;

namespace TH.Core {

    public class UIInventorySlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        #region PublicVariables
        #endregion

        #region PrivateVariables
        protected int _slotIdx;
		protected Action<int> _onSelectedCallback;
		protected Action<int> _onStartDragCallback;
		protected Action<int> _onPointerEnterCallback;
		private bool _isPointerDown;
		private float _selectedTime;
		private bool _isNull;
		private bool _isSelected;
		private Image _itemImage;
		private TextMeshProUGUI _stackedNumberText;
		#endregion

        #region PublicMethod
		public void Init(int idx, Action<int> onSelectedCallback, Action<int> onStartDragCallback, Action<int> onPointerEnterCallback) {
			_onSelectedCallback = onSelectedCallback;
			_onStartDragCallback = onStartDragCallback;
			_onPointerEnterCallback = onPointerEnterCallback;

			GetComponent<RectTransform>().localScale = Vector3.zero;
			GetComponent<RectTransform>().DOScale(1f, 0.2f).SetEase(Ease.OutBack);

			SetSlot(true, idx, null);
		}

		public void Init(int idx, InventoryItem item, Action<int> onSelectedCallback, Action<int> onStartDragCallback, Action<int> onPointerEnterCallback) 
		{
			_onSelectedCallback = onSelectedCallback;
			_onStartDragCallback = onStartDragCallback;
			_onPointerEnterCallback = onPointerEnterCallback;

			GetComponent<RectTransform>().localScale = Vector3.zero;
			GetComponent<RectTransform>().DOScale(1f, 0.2f).SetEase(Ease.OutBack);

			SetSlot(false, idx, item);
		}

		public virtual void SetSlot(bool isNull, int idx, InventoryItem item) {
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
			_onPointerEnterCallback(InventorySystem.NULL_ITEM_ID);
		}

		public void Select() {
			if (_isSelected == true) return;
			transform.DOScale(1.2f, 0.2f).SetEase(Ease.OutBack);
			_isSelected = true;
		}

		public void UnSelect() {
			if (_isSelected == false) return;
			transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack);
			_isSelected = false;
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