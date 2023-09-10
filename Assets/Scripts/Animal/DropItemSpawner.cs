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
	[SerializeField] private string _objectID; // 월드 매니저 이니셜라이즈 안 하면 제대로 값이 안 들어가서 임시로 Serialize 시켜둠
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
	public void SetObjectID(string objectID) => _objectID = objectID;
	#endregion

	#region PrivateMethod
	#endregion
}
