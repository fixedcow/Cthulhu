using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private PlayerHealth _health;
	private PlayerSanity _sanity;
	private PlayerMove _move;
	private PlayerAttack _attack;
	private PlayerInteract _interact;
	private PlayerTarget _target;

	private bool _canAct = true;
	#endregion

	#region PublicMethod
	public void Move(Vector2 inputDirection)
	{
		if (_canAct == false)
			return;

		_move.Move(inputDirection);
	}
	public void Stop()
	{
		_move.Stop();
	}
	public void Attack()
	{
		if (_canAct == false)
			return;

		_attack.Attack();
	}
	public void StopAttack()
	{
		_attack.StopAttack();
	}
	public void Interact()
	{
		if (_canAct == false)
			return;

		_interact.Interact();
	}
	public void Hit(int amount)
	{
		_health.ChangeValue(-amount);
	}
	public void Die()
	{

	}
	#endregion

	#region PrivateMethod
	private void Awake()
	{
		TryGetComponent(out _health);
		TryGetComponent(out _sanity);
		TryGetComponent(out _move);
		TryGetComponent(out _attack);
		TryGetComponent(out _interact);
		TryGetComponent(out _target);
	}
	private void Update()
	{
		_target.CheckTarget();
		_target.HighlightTarget();
		if (_canAct == false)
			return;
		_attack.HandleInput();
		_move.HandleInput();
	}
	#endregion
}
