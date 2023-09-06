using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TH.Core {

    public class UIInventorySlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        #region PublicVariables
        #endregion

        #region PrivateVariables
        private int _slotIdx;
		private Action<int> _onSelectedCallback;
		private Action<int> _onStartDragCallback;
		private bool _isPointerDown;
		private float _selectedTime;
		private bool _isNull;
		#endregion

        #region PublicMethod
		public void Init(Action<int> onSelectedCallback, Action<int> onStartDragCallback) {
			_onSelectedCallback = onSelectedCallback;
			_onStartDragCallback = onStartDragCallback;
			_isNull = true;
		}

		public void Init(int idx, ItemData itemData, Action<int> onSelectedCallback, Action<int> onStartDragCallback) 
		{
			_slotIdx = idx;
			_onSelectedCallback = onSelectedCallback;
			_onStartDragCallback = onStartDragCallback;
			_isNull = false;
		}

		public void SetSlot(bool isNull, int idx, ItemData itemData) {
			_slotIdx = idx;
			_isNull = isNull;
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