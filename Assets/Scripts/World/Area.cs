using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SectionSetting = TH.Core.WorldSetting.SectionSetting;

namespace TH.Core {

public class Area : MonoBehaviour
{
    #region PublicVariables 
	public IReadOnlyList<IReadOnlyList<string>> SpawnObjectData => _spawnObjectData.AsReadOnly();
	#endregion

	#region PrivateVariables
	private int _section;
	private int _areaIdx;
	private SectionSetting _sectionSetting;
	private List<List<string>> _spawnObjectData;

	#endregion

	#region PublicMethod
	public void Init(int section, int areaIdx, SectionSetting sectionSetting)
	{
		_section = section;
		_areaIdx = areaIdx;
		_sectionSetting = sectionSetting;
	}

	public void SpawnOnLoad() {
		int areaSize = WorldManager.Instance.GetAreaSize();
		_spawnObjectData = new List<List<string>>(areaSize);
		for (int i = 0; i < areaSize; i++) {
			_spawnObjectData.Add(new List<string>(areaSize));
			for (int j = 0; j < areaSize; j++) {
				_spawnObjectData[i].Add("");
			}
		}
	}

	public float GetMinSpawnCycle(string objectID) {
		return _sectionSetting.GetSpawnObjectSetting(objectID).spawnCycleMin;
	}

	public float GetMaxSpawnCycle(string objectID) {
		return _sectionSetting.GetSpawnObjectSetting(objectID).spawnCycleMax;
	}

	public int GetMaxSpawnCount(string objectID) {
		return _sectionSetting.GetSpawnObjectSetting(objectID).spawnCountMax;
	}

	public float GetCoefficient(string objectID) {
		return _sectionSetting.GetSpawnObjectSetting(objectID).cycleCoefficientOnMax;
	}
	#endregion
    
	#region PrivateMethod
	#endregion
}

}