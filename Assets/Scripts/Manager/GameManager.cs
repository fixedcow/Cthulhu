using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private Player _player;
	[ReadOnly][SerializeField] private int _gold = 0;
	[ReadOnly][SerializeField] private int _point = 0;
	#endregion

	#region PublicMethod
	public Player GetPlayer() => _player;
	public void AddGold(int amount)
	{
		_gold += amount;
	}
	public void AddPoint(int amount)
	{
		_point += amount;
	}
	#endregion

	#region PrivateMethod
	#endregion
}
