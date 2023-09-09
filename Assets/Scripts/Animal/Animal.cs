using DG.Tweening;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour, IHittable
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private AnimalData _data;
	private SpriteRenderer _sr;
	private Animator _animator;
	private AnimalMove _move;
	private int _hp;
	[SerializeField] private Vector2 hittablePointA = new Vector2(-0.5f, 0.5f);
	[SerializeField] private Vector2 hittablePointB = new Vector2(0.5f, -0.5f);
	#endregion

	#region PublicMethod
	public Vector2 GetPosition() => transform.position;
	public Vector2 GetHittableUIPositionA() => (Vector2)transform.position + hittablePointA;
	public Vector2 GetHittableUIPositionB() => (Vector2)transform.position + hittablePointB;
	public void Hit(int damage)
	{
		_sr.material.EnableKeyword("HITEFFECT_ON");
		Invoke(nameof(DisableHitEffect), 0.13f);
		_sr.transform.DOShakePosition(0.13f, 0.4f);
		_hp = Mathf.Clamp(_hp - damage, 0, _data.hpMax);
		if(_hp == 0)
		{
			Die();
		}
	}
	public void Die()
	{

	}
	public void Idle()
	{
		_move.RandomMove();
	}
	#endregion

	#region PrivateMethod
	private void Awake()
	{
		TryGetComponent(out _move);
		transform.Find("Renderer").TryGetComponent(out _sr);
		transform.Find("Renderer").TryGetComponent(out _animator);
	}
	private void Start()
	{
		_hp = _data.hpMax;
	}
	private void DisableHitEffect()
	{
		_sr.material.DisableKeyword("HITEFFECT_ON");
	}
	#endregion
}