using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core {

[CreateAssetMenu(fileName = "ItemData", menuName = "TH/Item Data", order = 0)]
public class ItemDataWrapper : ScriptableObject
{
	#region PublicVariables
	[SerializeField] public ItemData itemData;
	#endregion
}

}