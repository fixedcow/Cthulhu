using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core
{

	[Serializable]
	public class ObjectData
	{
		#region PublicVariables
		public string objectID;
		public string objectName;
		public int hpMax;
		public GameObject objectPrefab;
		public GameObject dropItem;
		public int dropQuantityMin;
		public int dropQuantityMax;
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
			dropQuantityMin = data.dropQuantityMin;
			dropQuantityMax = data.dropQuantityMax;
			hpMax = data.hpMax;
		}
		#endregion

		#region PrivateMethod
		#endregion
	}

}