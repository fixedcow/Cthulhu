using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core {

[Serializable]
public class ObjectData
{
    #region PublicVariables
	public string objectID;
	public string objectName;
	public GameObject objectPrefab;
	public GameObject dropItem;
	public int dropQuantity; 
	#endregion

	#region PrivateVariables
	#endregion

	#region PublicMethod
	public ObjectData(ObjectData data)
	{
		objectID = data.objectID;
		objectName = data.objectName;
		objectPrefab = data.objectPrefab;
		dropItem = data.dropItem;
		dropQuantity = data.dropQuantity;
	}
	#endregion
    
	#region PrivateMethod
	#endregion
}

}