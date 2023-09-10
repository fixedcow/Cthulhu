using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core {

[CreateAssetMenu(fileName = "ObjectData", menuName = "TH/Object Data", order = 0)]
public class ObjectDataWrapper: ScriptableObject
{
	#region PublicVariables
	public ObjectData objectData;
	#endregion
}

}