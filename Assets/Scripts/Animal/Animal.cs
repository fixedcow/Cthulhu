using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour, IHittable
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private AnimalData data;
	private int hp;
	#endregion

	#region PublicMethod
	public Vector2 GetPosition() => transform.position;
	public void Hit()
	{

	}
	#endregion

	#region PrivateMethod
	#endregion

}
