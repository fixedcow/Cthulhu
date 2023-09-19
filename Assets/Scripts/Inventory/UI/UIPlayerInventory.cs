using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;

namespace TH.Core
{

    public class UIPlayerInventory : UIInventory
    {
        #region PublicVariables
        #endregion

        #region PrivateVariables
        [SerializeField] private int _horizontalSlotNumber = 10;
        [SerializeField] private int _verticalSlotNumber = 2;
        [SerializeField] private float _slotSize = 90f;
        [SerializeField] private float _slotMargin = 20f;
        [SerializeField] private float _bottomOffset = 20f;
        [SerializeField] private int _showingLine = 1;

        private Button _toggleExpansionButton;
        private RectTransform _toggleExpansionButtonImageTransform;
        #endregion

        #region PublicMethod
        public override void Init(Inventory inventory)
        {
            _slotContentTransform = transform.Find("UIPack/SlotContents").GetComponent<RectTransform>();
            _toggleExpansionButton = transform.Find("UIPack/ToggleExpansionButton").GetComponent<Button>();
            _toggleExpansionButtonImageTransform = (RectTransform)_toggleExpansionButton.transform.Find("ButtonImage");
            _toggleExpansionButton.onClick.AddListener(ToggleExpansion);

            ((RectTransform)transform).sizeDelta = new Vector2(
                (_slotSize + _slotMargin) * _horizontalSlotNumber + _slotMargin,
                (_slotSize + _slotMargin) * _verticalSlotNumber + _slotMargin + _bottomOffset
            );
            base.Init(inventory);
        }

        public void ToggleExpansion()
        {
            _toggleExpansionButtonImageTransform.rotation = Quaternion.Euler(0, 0, _showingLine == 1 ? 180 : 0);
            if (_showingLine == 1)
            {
                SetExpansion(_verticalSlotNumber);
            }
            else
            {
                SetExpansion(1);
            }
        }

        public override void OpenInventory()
        {
            SetExpansion(1);
            ToggleExpansion();
        }

        public override void CloseInventory()
        {
            SetExpansion(_verticalSlotNumber);
            ToggleExpansion();
        }

        public override void ToggleInventory()
        {
            ToggleExpansion();
        }
        //현재 인덱스가 몇 번인지 알아야함.
        #endregion

        #region PrivateMethod
        private void SetExpansion(int showLine)
        {
            ((RectTransform)transform).DOAnchorPosY(
                -(_slotMargin + (_bottomOffset / 2) + (_slotSize + _slotMargin) * (_verticalSlotNumber - showLine)),
                0.5f
            ).SetEase(Ease.OutBack);
            _showingLine = showLine;
        }

        private bool _isUnselect = false;

        protected override void Update()
        {
            base.Update();

            if (_hasInitialized == true)
            {
                if (Input.GetKeyDown(InventorySystem.NEXT_KEYCODE))
                {
                    _inventory.SelectNextItem();
                }

                for (int i = 1; i < Mathf.Min(_slotList.Length + 1, _horizontalSlotNumber + 1, 11); i++)
                {
                    if (Input.GetKeyDown(InventorySystem.KEY_CODE_TABLE[i]))
                    {
                        _inventory.SelectItemIdx(i - 1);
                    }
                }

                //Scroll Management
                if (Input.GetAxis("Mouse ScrollWheel") < 0)
                {
                    _inventory.SelectNextItem();
                }
                if (Input.GetAxis("Mouse ScrollWheel") > 0)
                {
                    _inventory.SelectPreviousItem();
                }


                for (int i = 0; i < _slotList.Length; i++)
                {
                    if (_inventory.SelectedItemIdx == i)
                    {
                        _slotList[i].Select();
                    }
                    else
                    {
                        _slotList[i].UnSelect();
                    }
                }
            }
        }
        #endregion
    }

}