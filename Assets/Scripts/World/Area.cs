using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SectionSetting = TH.Core.WorldSetting.SectionSetting;

namespace TH.Core {

public class Area : MonoBehaviour
{
    #region PublicVariables
	#endregion

	#region PrivateVariables
	private int _section;
	private int _areaIdx;
	private SectionSetting _sectionSetting;

	#endregion

	#region PublicMethod
	public void Init(int section, int areaIdx, SectionSetting sectionSetting)
	{
		_section = section;
		_areaIdx = areaIdx;
		_sectionSetting = sectionSetting;
	}

	public void SpawnOnLoad() {
		
	}
	#endregion
    
	#region PrivateMethod
	#endregion
}

}