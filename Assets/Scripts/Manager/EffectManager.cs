using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private GameObject _dropEffectPrefab;
	#endregion

	#region PublicMethod
	public void SpawnDropEffect(Vector2 position)
	{
		Instantiate(_dropEffectPrefab, position, Quaternion.identity, transform);
	}
	#endregion

	#region PrivateMethod
	#endregion
}
