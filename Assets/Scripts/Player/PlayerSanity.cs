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
	public override void Add(int amount)
	{
		base.Add(amount);
		UIManager.Instance.Sanity.UpdateGauge(currentValue);
		if (currentValue / maxValue < vignettePercentage)
		{
			CameraManager.Instance.SetVignetteAlpha(currentValue / maxValue / vignettePercentage);
		}
		else
		{
			CameraManager.Instance.SetVignetteAlpha(1);
		}
	}
	#endregion

	#region PrivateMethod
	#endregion
}
