using DG.Tweening;
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
	[SerializeField] [HorizontalGroup] private int _minCount;
	[SerializeField] [HorizontalGroup] private int _maxCount;
	#endregion

	#region PublicMethod
	[Button]
	public void Drop()
	{
		EffectManager.Instance.SpawnDropEffect(transform.position);
		int rand = Random.Range(_minCount, _maxCount);
		for(int i = 0; i < rand; ++i)
		{
			GameObject item = Instantiate(_itemPrefab, transform.position, Quaternion.identity);
			item.transform.DOJump((Vector2)transform.position + new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)), 0.3f, Random.Range(1, 3), 0.3f);
		}
	}
	#endregion

	#region PrivateMethod
	#endregion
}
