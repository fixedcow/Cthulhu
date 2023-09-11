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
	/// <summary>
	/// CircleCollider2D radius값보다 아주 미세하게 길어야 함. 예를들면 0.4f의 경우 0.45f ~ 0.5f 정도.
	/// </summary>
	[SerializeField] private float _rayLength;

	private Vector2 _direction;
	#endregion

	#region PublicMethod
	public void Move(Vector2 direction)
	{
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
		if(TryMove(_direction.normalized) == false)
		{
			if(TryMove(new Vector2(_direction.x, 0)) == false)
			{
				TryMove(new Vector2(0, _direction.y));
			}
		}
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
	private bool TryMove(Vector2 direction)
	{
		RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, _rayLength, 1 << LayerMask.NameToLayer("Blocking"));
		Debug.DrawRay(transform.position, direction * _rayLength, Color.red);
		if(hit.collider == null)
		{
			_rb.velocity = direction * _speed;
			return true;
		}
		else
		{
			return false;
		}
	}
	#endregion
}
