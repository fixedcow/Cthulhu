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
	private PlayerItemHandler _itemHandler;

	private bool _canAct = true;
	#endregion

	#region PublicMethod
	public void AddMaxHealth(int amount)
	{
		_health.AddMaxValue(amount);
	}
	public void AddMaxSanity(int amount)
	{
		_sanity.AddMaxValue(amount);
	}
	public void HealHealth(int amount)
	{
		_health.Add(amount);
	}
	public void HealSanity(int amount)
	{
		_sanity.Add(amount);
	}
	public void FullHealHealth()
	{
		_health.FullHeal();
	}
	public void FullHealSanity()
	{
		_sanity.FullHeal();
	}
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
		if (_canAct == false || _itemHandler.IsHandleSomething() == true)
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
		CameraManager.Instance.Shake(CameraShaker.EShakingType.hit);
		_health.Add(-amount);
	}
	public void Die()
	{
		GameManager.Instance.GameOver();
	}
	public void HandleItem(int index)
	{
		_itemHandler.HandleItem(index);
	}
    public void MinusSanity(int amount)
    {
        _sanity.Add(-amount);
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
		transform.Find("Item Handler").TryGetComponent(out _itemHandler);
	}
	private void Update()
	{
		SetDirectionX();
		_target.CheckTarget();
		_target.HighlightTarget();
		if (_canAct == false)
			return;
		_attack.HandleInput();
		_move.HandleInput();
	}
	private void SetDirectionX()
	{
		int dirX = Utils.MousePosition.x > transform.position.x ? -1 : 1;
		Vector3 dir = new Vector3(dirX, 1, 1);
		transform.localScale = dir;
		_itemHandler.SetDirectionX(dir);
	}
	#endregion
}
