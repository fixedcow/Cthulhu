using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTradeTriggerGroup : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private List<TileTradeTrigger> elements = new List<TileTradeTrigger>();
	#endregion

	#region PublicMethod
	public void SetArea(Vector2Int AreaUnitPosition)
	{
		foreach(var element in elements)
		{
			element.SetCurrentArea(AreaUnitPosition);
		}
	}
	#endregion

	#region PrivateMethod
	#endregion
}
