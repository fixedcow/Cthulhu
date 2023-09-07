using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : PlayerStat
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private Player _player;
	#endregion

	#region PublicMethod
	public override void Add(int value)
	{
		base.Add(value);
		if(currentValue == minValue)
		{
			_player.Die();
		}
	}
	#endregion

	#region PrivateMethod
	private void Awake()
	{
		TryGetComponent(out _player);
	}
	#endregion
}
