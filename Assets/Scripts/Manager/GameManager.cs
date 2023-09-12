using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using TH.Core;

public class GameManager : Singleton<GameManager>
{
	#region PublicVariables
	public int Gold => _gold;
	public int Level => _tier;
	public int Point => _point;
	public int NeedPoint => TierPointCondition[_tier];

	public readonly static int[] TierPointCondition = new int[]
	{
		100, 200, 500, 1000, 1200, 1500, 2000, 2500, 5000
	};
	#endregion

	#region PrivateVariables
	[SerializeField] private Player _player;
	[ReadOnly][SerializeField] private int _gold = 0;
	[ReadOnly][SerializeField] private int _point = 0;
	[ReadOnly, SerializeField] private int _tier = 0;

	[SerializeField] private OfferUITest _offerui;
	#endregion

	#region PublicMethod
	public Player GetPlayer() => _player;
	public void AddGold(int amount)
	{
		_gold += amount;
		UIManager.Instance.Gold.UpdateAmount(_gold);
	}
	public void AddPoint(int amount)
	{
		_point += amount;
	}
	public void GameOver()
	{

	}
	#endregion

	#region PrivateMethod
	private void Start()
	{
		UIManager.Instance.Gold.UpdateAmount(_gold);
	}

	private void Update()
	{
		if (TierPointCondition[_tier] < _point)
		{
			_point = 0;
			_tier++;
			_player.HealSanity(20);
			_offerui.gameObject.SetActive(true);
		}
	}
	#endregion
}
