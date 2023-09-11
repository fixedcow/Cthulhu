using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player), typeof(Animator), typeof(PlayerTarget))]
public class PlayerAttack : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private Animator _animator;
	private PlayerTarget _target;

	[SerializeField] private int _damage;
	[SerializeField] private float _speed;

	private bool _inputExist;
	#endregion

	#region PublicMethod
	public void Attack()
	{
		_inputExist = true;
	}
	public void StopAttack()
	{
		_inputExist = false;
	}
	public void HandleInput()
	{
		if (_inputExist == false || _animator.GetBool("attack") == true)
			return;

		_animator.SetBool("attack", true);
		ITargetable target = _target.GetTarget();
		if(target is IHittable)
		{
			IHittable targetHit = target as IHittable;
			targetHit.Hit(_damage);
		}
	}
	#endregion

	#region PrivateMethod
	private void Awake()
	{
		TryGetComponent(out _target);
		TryGetComponent(out _animator);
	}
	#endregion
}
