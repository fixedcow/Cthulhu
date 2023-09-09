using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core {

public class WorldManager : MonoBehaviour
{
    #region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private List<ObjectDataWrapper> _originalObjectDataList;
	[SerializeField] private List<ItemDataWrapper> _originalItemDataList;
	[SerializeField] private WorldSettingWrapper _originalWorldSetting;

	[SerializeField] private Dictionary<string, ObjectData> _objectDataDict;
	[SerializeField] private Dictionary<string, ItemData> _itemDataDict;
	[SerializeField] private WorldSetting _worldSetting;
	#endregion

	#region PublicMethod
	#endregion
    
	#region PrivateMethod
	private void GenerateWorld() {
		LoadInitialSettings();
	}

	private void LoadInitialSettings() {
		List<ObjectData> objectDataList = new List<ObjectData>();
		foreach (var data in _originalObjectDataList) {
			objectDataList.Add(new ObjectData(data.objectData));
		}
		
		List<ItemData> itemDataList = new List<ItemData>();
		foreach (var data in _originalItemDataList) {
			itemDataList.Add(new ItemData(data.itemData));
		}

		_worldSetting = new WorldSetting(_originalWorldSetting.worldSetting);
	}

	private void GenerateTiles() {
		
	}
	#endregion
}

}