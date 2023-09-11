using System.Collections;
using System.Collections.Generic;
using TH.Core;
using UnityEngine;

public class TileTradeTrigger : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private Vector2Int _currentArea;
	[SerializeField] private Vector2Int _direction;
	#endregion

	#region PublicMethod
	public void SetCurrentArea(Vector2Int position)
	{
		_currentArea = position;
	}
	#endregion

	#region PrivateMethod
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			ShowPopup();
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			HidePopUp();
		}
	}
	private void ShowPopup()
	{
		Area target = GetTargetArea();
		Debug.Log(target);
		if(target != null)
		{
			target.ShowTradeText(_direction);
		}
	}
	private void HidePopUp()
	{
		Area target = GetTargetArea();
		if (target != null)
		{
			target.HideTradeText();
		}
	}
	private Area GetTargetArea()
	{
		Vector2Int target = _currentArea + _direction;
		if(target.x < 0 || target.x > 8 || target.y < 0 || target.y > 8)
		{
			return null;
		}
		return WorldManager.Instance.GetAreaByUnitPos(target);
	}
	#endregion
}
