using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TH.Core {

public class ItemInfoPanel : MonoBehaviour
{
    #region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private TextMeshProUGUI _itemNameText;
	[SerializeField] private TextMeshProUGUI _itemDescriptionText;
	[SerializeField] private TextMeshProUGUI _itemFlavorText;

	[SerializeField] private RectTransform _rectTransform;

	[SerializeField] private Vector2 _offset = new Vector3(5, -5);
	#endregion

	#region PublicMethod
	public void SetItemInfo(ItemData item) {
		_itemNameText.text = item.ItemName;
		_itemDescriptionText.text = item.ItemDescription;
		_itemFlavorText.text = item.ItemFlavorText;
	}

	public void UpdatePosition() {
		Vector2 mousePosition = Input.mousePosition;
		mousePosition.y = mousePosition.y - Screen.height; 

		if (mousePosition.x + _rectTransform.sizeDelta.x > Screen.width)
			mousePosition.x = mousePosition.x - _rectTransform.sizeDelta.x;
		if (mousePosition.y - _rectTransform.sizeDelta.y < - Screen.height)
			mousePosition.y = mousePosition.y + _rectTransform.sizeDelta.y;

		_rectTransform.anchoredPosition = mousePosition + _offset;
	}
	#endregion
    
	#region PrivateMethod
	private void Awake() {
		_itemNameText = transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
		_itemDescriptionText = transform.Find("ItemDescription").GetComponent<TextMeshProUGUI>();
		_itemFlavorText = transform.Find("ItemFlavor").GetComponent<TextMeshProUGUI>();
		_rectTransform = GetComponent<RectTransform>();
	}
	#endregion
}

}