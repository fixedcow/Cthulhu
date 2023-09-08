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
	private AnimalMove _move;
	private int _hp;
	#endregion

	#region PublicMethod
	public Vector2 GetPosition() => transform.position;

	public void Hit(int damage)
	{
		_hp = Mathf.Clamp(_hp - damage, 0, _data.hpMax);
		if(_hp == 0)
		{
			Die();
		}
	}
	public void Die()
	{

	}
	#endregion

	#region PrivateMethod
	private void Awake()
	{
		TryGetComponent(out _move);
	}
	private void Start()
	{
		_hp = _data.hpMax;
	}
	#endregion

}
