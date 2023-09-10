using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

[RequireComponent(typeof(BoxCollider2D))]
public class UIButton : MonoBehaviour
{
	#region PublicVariables
	public UnityEvent onButtonClicked;
	#endregion

	#region PrivateVariables
	private bool mouseOver;
	private bool mouseDown;
	#endregion

	#region PublicMethod
	#endregion

	#region PrivateMethod
	private void OnMouseEnter()
	{
		mouseOver = true;
		transform.DOScale(1.05f, 0.1f);
	}
	private void OnMouseExit()
	{
		mouseOver = false;
		transform.DOScale(1f, 0.1f);
	}
	private void OnMouseDown()
	{
		mouseDown = true;
		transform.DOScale(0.95f, 0.1f);
	}
	private void OnMouseUp()
	{
		if(mouseDown && mouseOver)
		{
			transform.DOScale(1f, 0.1f).From(1.1f);
			onButtonClicked.Invoke();
		}
		mouseDown = false;
	}
	#endregion
}
