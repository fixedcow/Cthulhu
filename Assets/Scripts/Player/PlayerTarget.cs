using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerTarget : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private HitTarget _hittableUI;
	[SerializeField] private GameObject _interactableUI;

	private ITargetable _target;

	[SerializeField] private float interactableDistance;
	#endregion

	#region PublicMethod
	public ITargetable GetTarget() => _target;
	public void CheckTarget()
	{
		RaycastHit2D mouseHit = CheckMouseRay();
		if (mouseHit.collider != null)
		{
			ITargetable target;
			mouseHit.collider.gameObject.TryGetComponent(out target);
			if (target != null && Vector2.Distance(transform.position, mouseHit.transform.position) < interactableDistance)
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
			_interactableUI.SetActive(false);
			_hittableUI.Deactivate();
		}
	}
	#endregion

	#region PrivateMethod
	private RaycastHit2D CheckMouseRay()
	{
		RaycastHit2D hit = Physics2D.Raycast(Utils.MousePosition, Vector2.zero
			, float.MaxValue, 1 << LayerMask.NameToLayer("Target"));
		return hit;
	}
	private void HighlightInteractableObject()
	{
		if(_target is IInteractable)
		{
			_interactableUI.SetActive(true);
			_interactableUI.transform.position = _target.GetPosition();
		}
		else
		{
			_interactableUI.SetActive(false);
		}
	}
	private void HighlightAttackableObject()
	{
		if (_target is IHittable)
		{
			IHittable targetHit = _target as IHittable;
			_hittableUI.Activate(targetHit.GetHittableUIPositionA(), targetHit.GetHittableUIPositionB());
			_hittableUI.transform.position = _target.GetPosition();
		}
		else
		{
			_hittableUI.Deactivate();
		}
	}
	#endregion
}
