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
	public virtual void ChangeValue(int value)
	{
		currentValue = Mathf.Clamp(currentValue + value, minValue, maxValue);
	}
	#endregion

	#region PrivateMethod
	#endregion
}
