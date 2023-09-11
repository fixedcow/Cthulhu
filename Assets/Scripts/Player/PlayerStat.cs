using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerStat : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] protected int minValue = 0;
	[SerializeField] protected int maxValue;
	[ReadOnly][SerializeField] protected int currentValue;
	#endregion

	#region PublicMethod
	public virtual void Add(int amount)
	{
		currentValue = Mathf.Clamp(currentValue + amount, minValue, maxValue);
	}
	public void AddMaxValue(int amount)
	{
		maxValue += amount;
	}
	public void FullHeal()
	{
		currentValue = maxValue;
	}
	#endregion

	#region PrivateMethod
	#endregion
}
