using DG.Tweening;
using Pathfinding;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animal))]
public class AnimalMove : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private Animator _animator;
	private AIPath _ai;
	private AIDestinationSetter _destination;
	#endregion

	#region PublicMethod
	public void SetSpeed(int speed) => _ai.maxSpeed = speed;
	public void RandomMove()
	{
		_animator.SetBool("move", true);
		_destination.target = null;
		_ai.SetPath(RandomPath.Construct(transform.position, 5));

	}
	public void ChasePlayer()
	{
		_animator.SetBool("move", true);
		_destination.target = GameManager.Instance.GetPlayer().transform;
	}
	#endregion

	#region PrivateMethod
	private void Awake()
	{
		TryGetComponent(out _ai);
		TryGetComponent(out _destination);
		transform.Find("Renderer").TryGetComponent(out _animator);
	}
	private void Update()
	{
		SetAnimation();
	}
	private void SetAnimation()
	{
		SetDirectionByDestination();
		StopAnimationWhenReachedDestination();
	}
	private void SetDirectionByDestination()
	{
		if(_ai.destination.x > transform.position.x)
		{
			_animator.transform.localScale = new Vector3(-1, 1, 1);
		}
		else
		{
			_animator.transform.localScale = new Vector3(1, 1, 1);
		}
	}
	private void StopAnimationWhenReachedDestination()
	{
		if (_ai.reachedDestination == true)
		{
			_animator.SetBool("move", false);
		}
	}
	#endregion
}
