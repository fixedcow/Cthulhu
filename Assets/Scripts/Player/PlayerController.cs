using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private PlayerInput _input;
	private Player _player;
	#endregion

	#region PublicMethod
	#endregion

	#region PrivateMethod
	private void Awake()
	{
		_input = new PlayerInput();
		TryGetComponent(out _player);
	}
	private void OnEnable()
	{
		_input.Enable();
		_input.Player.Move.performed += OnMovePerformed;
		_input.Player.Move.canceled += OnMoveCanceled;
		_input.Player.Attack.performed += OnAttackPerformed;
		_input.Player.Attack.canceled += OnAttackCanceled;
		_input.Player.Interact.performed += OnInteractPerformed;
	}
	private void OnDisable()
	{
		_input.Player.Move.performed -= OnMovePerformed;
		_input.Player.Move.canceled -= OnMoveCanceled;
		_input.Player.Attack.performed -= OnAttackPerformed;
		_input.Player.Attack.canceled -= OnAttackCanceled;
		_input.Player.Interact.performed -= OnInteractPerformed;
		_input.Disable();
	}
	private void OnMovePerformed(InputAction.CallbackContext context)
	{
		_player.Move(context.ReadValue<Vector2>().normalized);
	}
	private void OnMoveCanceled(InputAction.CallbackContext context)
	{
		_player.Stop();
	}
	private void OnAttackPerformed(InputAction.CallbackContext context)
	{
		_player.Attack();
	}
	private void OnAttackCanceled(InputAction.CallbackContext context)
	{
		_player.StopAttack();
	}
	private void OnInteractPerformed(InputAction.CallbackContext context)
	{
		_player.Interact();
	}
	#endregion
}
