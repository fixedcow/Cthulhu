using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private Player _player;
	private int _gold;
	private int _point;
	#endregion

	#region PublicMethod
	public Player GetPlayer() => _player;
	public void AddGold(int amount)
	{

	}
	public void AddPoint(int amount)
	{

	}
	#endregion

	#region PrivateMethod
	#endregion
}
