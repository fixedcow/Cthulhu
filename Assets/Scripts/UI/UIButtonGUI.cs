using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIButtonGUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    #region PublicVariables
    public UnityEvent _onButtonClicked;
    #endregion

    #region PrivateVariables
    private bool _mouseOver;
    private bool _mouseDown;
    #endregion

    #region PublicMethod
    #endregion

    #region PrivateMethod
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("enter");
        _mouseOver = true;
        transform.DOScale(1.05f, 0.1f);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _mouseOver = false;
        transform.DOScale(1f, 0.1f);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        _mouseDown = true;
        transform.DOScale(0.95f, 0.1f);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (_mouseDown && _mouseOver)
        {
            transform.DOScale(1f, 0.1f).From(1.1f);
            _onButtonClicked.Invoke();
        }
        _mouseDown = false;
    }
    #endregion
}
