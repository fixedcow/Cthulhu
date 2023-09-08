using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[RequireComponent(typeof(Player), typeof(Rigidbody2D), typeof(Animator))]
public class PlayerMove : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private Animator _animator;
	private Rigidbody2D _rb;
	private PlayerItemGetter _playerItemGetter;
	[SerializeField] private float _speed;

	private Vector2 _direction;
	#endregion

	#region PublicMethod
	public void Move(Vector2 direction)
	{
		int dirX = direction.x > 0 ? -1 : 1;
		transform.localScale = new Vector3(dirX, 1, 1); 
		_animator.SetBool("move", true);
		_direction = direction;
	}
	public void Stop()
	{
		_animator.SetBool("move", false);
		_direction = Vector2.zero;
	}
	public void HandleInput()
	{
		_rb.velocity = _direction * _speed;
		_playerItemGetter.PickItem();
	}
	#endregion
	 
	#region PrivateMethod
	private void Awake()
	{
		TryGetComponent(out _rb);
		TryGetComponent(out _animator);
		TryGetComponent(out _playerItemGetter);
	}
	#endregion
}
