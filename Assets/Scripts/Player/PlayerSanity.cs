using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerSanity : PlayerStat
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField][Range(0, 1)] private float vignettePercentage;
	private Player _player;
	#endregion

	#region PublicMethod
	[Button]
	public override void Add(int amount)
	{
		base.Add(amount);
		UIManager.Instance.Sanity.UpdateGauge(currentValue);
		if ((float)currentValue / maxValue < vignettePercentage)
		{
			CameraManager.Instance.SetVignetteAlpha(1 - (((float)currentValue / maxValue) / vignettePercentage));
		}
		else
		{
			CameraManager.Instance.SetVignetteAlpha(0);
		}
        if (currentValue == minValue)
        {
			_player.Hit(5);
        }
    }
	#endregion

	#region PrivateMethod
	protected void Awake()
	{
		TryGetComponent(out _player);
	}
	protected override void Start()
	{
		base.Start();
		UIManager.Instance.Sanity.Initialize(maxValue);
	}
	#endregion
}
