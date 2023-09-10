using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TH.Core;
using UnityEngine;

public class DropItemSpawner : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private string _objectID;
	#endregion

	#region PublicMethod
	[Button]
	public void Drop()
	{
		EffectManager.Instance.SpawnDropEffect(transform.position);
		int rand = Random.Range(WorldManager.Instance.GetObjectData(_objectID).dropQuantityMin
			, WorldManager.Instance.GetObjectData(_objectID).dropQuantityMax);
		for(int i = 0; i < rand; ++i)
		{
			GameObject item = Instantiate(WorldManager.Instance.GetItemPrefab(_objectID), transform.position, Quaternion.identity);
			item.transform.DOJump((Vector2)transform.position + new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)), 0.3f, Random.Range(1, 3), 0.3f);
		}
	}
	#endregion

	#region PrivateMethod
	#endregion
}
