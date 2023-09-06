using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
	public static Vector2 MousePosition
	{
		get
		{
			Vector3 result = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			result.z = 0;
			return result;
		}
	}
}
