using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TH.Core {

public class OfferCardUITest : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    #region PublicVariables
	#endregion

	#region PrivateVariables
	private Action _action;
	private Action<int> _onClick;
	private int _index;
	private bool _isClicked;
	#endregion

	#region PublicMethod
	public void SetText(string title, string description, Action action, Action<int> onClick, int index) {
		_action = action;
		_onClick = onClick;
		_index = index;
		_isClicked = false;
		transform.Find("CardTitle").GetComponent<TMPro.TextMeshProUGUI>().text = title;
		transform.Find("CardMiddleTitle").GetComponent<TMPro.TextMeshProUGUI>().text = description;
		GetComponent<Image>().material.SetFloat("_FadeAmount", 0);
		GetComponent<Image>().material.SetFloat("_InnerOutlineThickness", 0);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (_isClicked) return;
		_isClicked = true;
		_onClick(_index);
		_action();
		Select();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		GetComponent<RectTransform>().localScale = Vector3.one;
		GetComponent<RectTransform>().DOScale(1.1f, 0.1f);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		GetComponent<RectTransform>().localScale = Vector3.one * 1.1f;
		GetComponent<RectTransform>().DOScale(1, 0.1f);
	}

	public void Unselect() {
		DOTween.To(() => GetComponent<Image>().material.GetFloat("_FadeAmount"), x => GetComponent<Image>().material.SetFloat("_FadeAmount", x), 1, 1f);
		transform.Find("CardTitle").GetComponent<TMPro.TextMeshProUGUI>().text = "";
		transform.Find("CardMiddleTitle").GetComponent<TMPro.TextMeshProUGUI>().text = "";
	}

	public void Select () {
		GetComponent<Image>().material.SetFloat("_InnerOutlineThickness", 1);
	}
	#endregion
    
	#region PrivateMethod
	#endregion
}

}