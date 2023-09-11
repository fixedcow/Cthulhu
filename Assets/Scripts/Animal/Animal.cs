using DG.Tweening;
using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TH.Core;

public class Animal : WorldObject
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private AnimalAI _ai;
	private Animator _animator;
	private AnimalMove _move;

	private int _damage;
	#endregion

	#region PublicMethod
	public override void Init(string id, Vector2Int areaPos, Action<string, Vector2Int> onObjectDestroyed)
	{
		base.Init(id, areaPos, onObjectDestroyed);
		AnimalData data = WorldManager.Instance.GetObjectData(_objectID) as AnimalData;
		_damage = data.damage;
		_move.SetSpeed(data.speedIdle);
		_drop.SetObjectID(_objectID);
		_move.SetObjectID(_objectID);
		_ai.SetRange(_objectID);
	}
	public string GetObjectID() => _objectID;
	public void Idle()
	{
		_move.RandomMove();
	}
	public void ChasePlayer()
	{
		_move.ChasePlayer();
	}
	public void Attack(Collider2D target)
	{
		_animator.SetTrigger("attack");
		target.GetComponent<Player>().Hit(_damage);
	}
	#endregion

	#region PrivateMethod
	protected override void Awake()
	{
		base.Awake();
		TryGetComponent(out _move);
		TryGetComponent(out _ai);
		transform.Find("Renderer").TryGetComponent(out _animator);
	}
	#endregion
}