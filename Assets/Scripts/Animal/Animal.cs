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
	private Animator _animator;
	private AnimalMove _move;
	#endregion

	#region PublicMethod
	public override void Init(string id, Vector2Int areaPos, Action<string, Vector2Int> onObjectDestroyed)
	{
		base.Init(id, areaPos, onObjectDestroyed);
		AnimalData data = WorldManager.Instance.GetObjectData(_objectID) as AnimalData;
		_move.SetSpeed(data.speedIdle);
		_drop.SetObjectID(_objectID);
		_move.SetObjectID(_objectID);
	}
	public void Idle()
	{
		_move.RandomMove();
	}
	#endregion

	#region PrivateMethod
	protected override void Awake()
	{
		base.Awake();
		TryGetComponent(out _move);
		transform.Find("Renderer").TryGetComponent(out _animator);
	}
	#endregion
}