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
	#endregion

	#region PublicMethod
	public void Init(string id)
	{
		_hasInitialized = true;
		_objectID = id;
	}
	#endregion

	#region PrivateMethod
	#endregion
}
