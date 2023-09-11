using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BridgeController : MonoBehaviour
{
	#region PublicVariables
	public enum EDirection
	{
		up = 0,
		down = 1,
		left = 2,
		right = 3
	}
	#endregion

	#region PrivateVariables
	[SerializeField] private GameObject up;
	[SerializeField] private GameObject down;
	[SerializeField] private GameObject left;
	[SerializeField] private GameObject right;

	private float _bridgeFadeDuration = 1f;
	#endregion

	#region PublicMethod
	public void OpenGate(EDirection dir)
	{
		switch(dir)
		{
			case EDirection.up:
				up.GetComponent<Collider2D>().enabled = false;
				up.GetComponent<SpriteRenderer>().DOFade(1, _bridgeFadeDuration);
				break;
			case EDirection.down:
				down.GetComponent<Collider2D>().enabled = false;
				down.GetComponent<SpriteRenderer>().DOFade(1, _bridgeFadeDuration);
				break;
			case EDirection.left:
				left.GetComponent<Collider2D>().enabled = false;
				left.GetComponent<SpriteRenderer>().DOFade(1, _bridgeFadeDuration);
				break;
			case EDirection.right:
				right.GetComponent<Collider2D>().enabled = false;
				right.GetComponent<SpriteRenderer>().DOFade(1, _bridgeFadeDuration);
				break;
			default:
				break;
		}
	}
	public void CloseGate(EDirection dir)
	{
		switch (dir)
		{
			case EDirection.up:
				up.GetComponent<Collider2D>().enabled = true;
				up.GetComponent<SpriteRenderer>().DOFade(0, _bridgeFadeDuration);
				break;
			case EDirection.down:
				down.GetComponent<Collider2D>().enabled = true;
				down.GetComponent<SpriteRenderer>().DOFade(0, _bridgeFadeDuration);
				break;
			case EDirection.left:
				left.GetComponent<Collider2D>().enabled = true;
				left.GetComponent<SpriteRenderer>().DOFade(0, _bridgeFadeDuration);
				break;
			case EDirection.right:
				right.GetComponent<Collider2D>().enabled = true;
				right.GetComponent<SpriteRenderer>().DOFade(0, _bridgeFadeDuration);
				break;
			default:
				break;
		}
	}
	#endregion

	#region PrivateMethod
	private void Awake()
	{
		up.GetComponent<SpriteRenderer>().DOFade(0, 0);
		down.GetComponent<SpriteRenderer>().DOFade(0, 0);
		left.GetComponent<SpriteRenderer>().DOFade(0, 0);
		right.GetComponent<SpriteRenderer>().DOFade(0, 0);
	}
	#endregion
}
