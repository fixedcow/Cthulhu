using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHitObj : MonoBehaviour, IHittable
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private Vector2 hittablePointA = new Vector2(-0.5f, 0.5f);
	[SerializeField] private Vector2 hittablePointB = new Vector2(0.5f, -0.5f);
	#endregion

	#region PublicMethod
	#endregion

	#region PrivateMethod
	#endregion
	public Vector2 GetPosition() => transform.position;
	public Vector2 GetHittableUIPositionA() => (Vector2)transform.position + hittablePointA;
	public Vector2 GetHittableUIPositionB() => (Vector2)transform.position + hittablePointB;
	public void Hit(int damage)
	{

	}
}
