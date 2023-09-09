using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core {

[CreateAssetMenu(fileName = "WorldSetting", menuName = "TH/World Setting", order = 0)]
public class WorldSettingWrapper: ScriptableObject
{
	#region PublicVariables
	[SerializeField] public WorldSetting worldSetting;
	#endregion

	#region PublicMethod

	#endregion
}

}