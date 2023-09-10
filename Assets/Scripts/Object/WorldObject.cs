using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TH.Core;
using Sirenix.OdinInspector;

[RequireComponent(typeof(DropItemSpawner))]
public class WorldObject : MonoBehaviour, IHittable
{
	#region PublicVariables
	public string ObjectID => _objectID;
	#endregion

	#region PrivateVariables
	[SerializeField] protected string _objectID;
	private bool _hasInitialized = false;
	private Vector2Int _areaPos;
	private Action<string, Vector2Int> _onObjectDestroyed;

	private SpriteRenderer _sr;
	protected DropItemSpawner _drop;
	protected int _hp;
	[SerializeField] private Vector2 hittablePointA = new Vector2(-0.5f, 0.5f);
	[SerializeField] private Vector2 hittablePointB = new Vector2(0.5f, -0.5f);
	#endregion

	#region PublicMethod
	public virtual void Init(string id, Vector2Int areaPos, Action<string, Vector2Int> onObjectDestroyed)
	{
		_hasInitialized = true;
		_objectID = id;
		_areaPos = areaPos;
		_onObjectDestroyed = onObjectDestroyed;
		_hp = WorldManager.Instance.GetObjectData(_objectID).hpMax;
	}
	public Vector2 GetPosition() => transform.position;
	public Vector2 GetHittableUIPositionA() => (Vector2)transform.position + hittablePointA;
	public Vector2 GetHittableUIPositionB() => (Vector2)transform.position + hittablePointB;
	public virtual void Hit(int damage)
	{
		_sr.material.EnableKeyword("HITEFFECT_ON");
		Invoke(nameof(DisableHitEffect), 0.13f);
		_sr.transform.DOShakePosition(0.13f, 0.4f);
		_hp = Mathf.Clamp(_hp - damage, 0, WorldManager.Instance.GetObjectData(_objectID).hpMax);
		if (_hp == 0)
		{
			Die();
		}
	}
	public virtual void Die()
	{
		_drop.Drop();
		Destroy(gameObject);
	}
	#endregion

	#region PrivateMethod
	protected virtual void Awake()
	{
		TryGetComponent(out _drop);
		transform.Find("Renderer").TryGetComponent(out _sr);
	}
	private void OnDestroy()
	{
		_onObjectDestroyed(_objectID, _areaPos);
	}
	private void DisableHitEffect()
	{
		_sr.material.DisableKeyword("HITEFFECT_ON");
	}
	#endregion
}
