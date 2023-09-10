using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core
{

[CreateAssetMenu(fileName = "ObjectData", menuName = "TH/Object Data", order = 0)]
public class ObjectDataWrapper : BaseObjectDataWrapper<ObjectData>
{
}

public class BaseObjectDataWrapper<T> : ScriptableObject where T : ObjectData
{
	#region PublicVariables
	public T objectData;
	#endregion
}

}