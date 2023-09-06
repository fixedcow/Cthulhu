using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerTarget : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private GameObject hittableUI;
	[SerializeField] private GameObject interactableUI;

	private ITargetable _target;

	[SerializeField] private float interactableDistance;
	#endregion

	#region PublicMethod
	public ITargetable GetTarget() => _target;
	public void CheckTarget()
	{
		Vector2 targetDirection = (Utils.MousePosition - (Vector2)transform.position).normalized;
		RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection
			, interactableDistance, 1 << LayerMask.NameToLayer("Target"));
		Debug.DrawRay(transform.position, targetDirection * interactableDistance, Color.red);
		if(hit.collider != null)
		{
			ITargetable target;
			hit.collider.gameObject.TryGetComponent(out target);
			if(target != null)
			{
				_target = target;
			}
			else
			{
				_target = null;
			}
		}
		else
		{
			_target = null;
		}
	}
	public void HighlightTarget()
	{
		if(_target != null)
		{
			HighlightInteractableObject();
			HighlightAttackableObject();
		}
		else
		{
			RemoveHighlight();
		}
	}
	#endregion

	#region PrivateMethod
	private void HighlightInteractableObject()
	{
		if(_target is IInteractable)
		{
			interactableUI.SetActive(true);
			interactableUI.transform.position = _target.GetPosition();
		}
	}
	private void HighlightAttackableObject()
	{
		if (_target is IHittable)
		{
			hittableUI.SetActive(true);
			hittableUI.transform.position = _target.GetPosition();
		}
	}
	private void RemoveHighlight()
	{
		interactableUI.SetActive(false);
		hittableUI.SetActive(false);
	}
	#endregion
}
