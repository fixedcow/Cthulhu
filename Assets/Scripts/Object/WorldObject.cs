using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private string _objectID;
	private bool _hasInitialized = false;
	private Vector2Int _areaPos;
	private Action<string, Vector2Int> _onObjectDestroyed;
	#endregion

	#region PublicMethod
	public void Init(string id, Vector2Int areaPos, Action<string, Vector2Int> onObjectDestroyed)
	{
		_hasInitialized = true;
		_objectID = id;
		_areaPos = areaPos;
		_onObjectDestroyed = onObjectDestroyed;
	}
	#endregion

	#region PrivateMethod
	#endregion
}
