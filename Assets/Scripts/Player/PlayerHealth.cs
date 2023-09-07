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
	public override void ChangeValue(int value)
	{
		base.ChangeValue(value);
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
