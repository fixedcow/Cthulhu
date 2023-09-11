using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

[RequireComponent(typeof(BoxCollider2D))]
public class UIButton : MonoBehaviour
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
    private void OnMouseEnter()
	{
		_mouseOver = true;
		transform.DOScale(1.05f, 0.1f);
	}
	private void OnMouseExit()
	{
		_mouseOver = false;
		transform.DOScale(1f, 0.1f);
	}
	private void OnMouseDown()
	{
		_mouseDown = true;
		transform.DOScale(0.95f, 0.1f);
	}
	private void OnMouseUp()
	{
		if(_mouseDown && _mouseOver)
		{
			transform.DOScale(1f, 0.1f).From(1.1f);
			_onButtonClicked.Invoke();
		}
		_mouseDown = false;
	}
	#endregion
}
