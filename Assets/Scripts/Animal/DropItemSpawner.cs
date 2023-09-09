using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemSpawner : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private GameObject _itemPrefab;
	[SerializeField] [HorizontalGroup] private int _minDropCount;
	[SerializeField] [HorizontalGroup] private int _maxDropCount;
	#endregion

	#region PublicMethod
	public void Drop()
	{

	}
	#endregion

	#region PrivateMethod
	#endregion
}
