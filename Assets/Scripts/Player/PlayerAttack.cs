using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player), typeof(Animator))]
public class PlayerAttack : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private Animator animator;

	[SerializeField] private float _damage;
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
		if (_inputExist == false)
			return;

		animator.SetBool("attack", true);
		Debug.Log("Attack!");
	}
	#endregion

	#region PrivateMethod
	private void Awake()
	{
		TryGetComponent(out animator);
	}
	#endregion
}
