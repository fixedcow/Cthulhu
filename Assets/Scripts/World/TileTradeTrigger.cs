using System.Collections;
using System.Collections.Generic;
using TH.Core;
using UnityEngine;
using static System.Collections.Specialized.BitVector32;

public class TileTradeTrigger : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private Vector2Int _currentArea;
	[SerializeField] private Vector2Int _direction;

	private bool _isPlayerEnter = false;
	private bool _isGateOpened = false;
	#endregion

	#region PublicMethod
	public void SetCurrentArea(Vector2Int position)
	{
		_currentArea = position;
	}
	#endregion

	#region PrivateMethod
	private void Update()
	{
		if (_isPlayerEnter == true)
		{
			Area targetArea = GetTargetArea();
			if (targetArea.HasOpened && _isGateOpened == false)
			{
				_isGateOpened = true;
				BridgeController.EDirection direction = GetDirection(_direction);
				WorldManager.Instance.GetAreaByUnitPos(_currentArea).OpenGate(direction);
				targetArea.OpenGate(GetInverseDirection(direction));
				WorldManager.Instance.Rescan();
				return;
			}
			if (Input.GetKeyDown(KeyCode.F))
			{
				int price = WorldManager.Instance.AREA_TIER_COST[targetArea.Section];
				if (GameManager.Instance.Gold >= price)
				{
					HidePopUp();
					GameManager.Instance.AddGold(-price);
					WorldManager.Instance.OpenArea(targetArea.Section, targetArea.AreaIdx);
					BridgeController.EDirection direction = GetDirection(_direction);
					WorldManager.Instance.GetAreaByUnitPos(_currentArea).OpenGate(direction);
					targetArea.OpenGate(GetInverseDirection(direction));
					WorldManager.Instance.Rescan();
				}
			}
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.CompareTag("Player"))
		{
			ShowPopup();
			_isPlayerEnter = true;
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			HidePopUp();
			_isPlayerEnter = false;
		}
	}
	private void ShowPopup()
	{
		Area target = GetTargetArea();
		if (target.HasOpened == true)
			return;
		if(target != null)
		{
			target.ShowTradeText(_direction);
		}
	}
	private void HidePopUp()
	{
		Area target = GetTargetArea();
		if (target.HasOpened == true)
			return;
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
	private BridgeController.EDirection GetDirection(Vector2Int dir)
	{
		if(dir == Vector2Int.up)
		{
			return BridgeController.EDirection.up;
		}
		else if(dir == Vector2Int.down)
		{
			return BridgeController.EDirection.down;
		}
		else if(dir == Vector2Int.left)
		{
			return BridgeController.EDirection.left;
		}
		else
		{
			return BridgeController.EDirection.right;
		}
	}
	private BridgeController.EDirection GetInverseDirection(BridgeController.EDirection direction)
	{
		switch(direction)
		{
			case BridgeController.EDirection.up:
				return BridgeController.EDirection.down;
			case BridgeController.EDirection.down:
				return BridgeController.EDirection.up;
			case BridgeController.EDirection.left:
				return BridgeController.EDirection.right;
			case BridgeController.EDirection.right:
				return BridgeController.EDirection.left;
			default:
				return 0;
		}
	}
	#endregion
}
