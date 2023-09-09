using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTarget : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private GameObject tl;
	[SerializeField] private GameObject tr;
	[SerializeField] private GameObject bl;
	[SerializeField] private GameObject br;
	#endregion

	#region PublicMethod
	public void Activate(Vector2 pointA, Vector2 pointB)
	{
		float lx = pointA.x;
		float ly = pointA.y;
		float rx = pointB.x; 
		float ry = pointB.y;
		tl.transform.position = pointA;
		tr.transform.position = new Vector2(rx, ly);
		bl.transform.position = new Vector2(lx, ry);
		br.transform.position = pointB;
		gameObject.SetActive(true);
	}
	public void Deactivate()
	{
		gameObject.SetActive(false);
	}
	#endregion

	#region PrivateMethod
	#endregion
}