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
	#endregion

	#region PublicMethod
	[Button]
	public override void Add(int amount)
	{
		base.Add(amount);
		UIManager.Instance.Sanity.UpdateGauge(currentValue);
		if (currentValue / maxValue < vignettePercentage)
		{
			CameraManager.Instance.SetVignetteAlpha(1 - currentValue / maxValue / vignettePercentage);
		}
		else
		{
			CameraManager.Instance.SetVignetteAlpha(0);
		}
	}
	#endregion

	#region PrivateMethod
	protected override void Start()
	{
		base.Start();
		UIManager.Instance.Sanity.Initialize(maxValue);
	}
	#endregion
}
