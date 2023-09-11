using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TH.Core {

public class WorldManager : Singleton<WorldManager>
{
	#region PublicVariables
		#endregion

	#region PrivateVariables
	[SerializeField] private AstarPath _aStar;
	[SerializeField] private int _areaPadding = 2;

	[SerializeField] private List<ObjectDataWrapper> _originalObjectDataList;
	[SerializeField] private List<AnimalDataWrapper> _originalAnimalDataList;
	[SerializeField] private List<ItemDataWrapper> _originalItemDataList;
	[SerializeField] private WorldSettingWrapper _originalWorldSetting;

	[ShowInInspector] private Dictionary<string, ObjectData> _objectDataDict;
	[ShowInInspector] private Dictionary<string, ItemData> _itemDataDict;
	private WorldSetting _worldSetting;

	[ShowInInspector] private Dictionary<int, List<Area>> _areaDict;
	[SerializeField, ReadOnly] private List<List<Area>> _areaList;
	#endregion

	#region PublicMethod
	public WorldSetting.SectionSetting GetSectionSetting(int section) {
		return _worldSetting.sectionSettings[section];
	}
	public ObjectData GetObjectData(string objectId)
	{
		return _objectDataDict[objectId];
	}
	public GameObject GetItemPrefab(string objectId)
	{
		return _objectDataDict[objectId].dropItem;
	}

	public ItemData GetItemData(string itemId) 
	{
		return _itemDataDict[itemId];	
	}

	public int GetAreaSize() {
		return _worldSetting.areaSize;
	}

	public int GetOpenedAreaCount(int section) {
		return _areaDict[section].FindAll(a => a.HasOpened).Count;
	}

	[Button]
	public void OpenArea(int section, int areaIdx) {
		_areaDict[section][areaIdx].Open();
	}

	public void MultiplyMaxStackableNumber(int multiplier) {
		foreach (var item in _itemDataDict) {
			item.Value.MaxStackableNumber *= multiplier;
		}
	}

	public void MultiplyMaxStackableNumber(string itemId, int multiplier) {
		_itemDataDict[itemId].MaxStackableNumber *= multiplier;
	}

	public void SetMaxStackableNumber(string itemId, int maxStackableNumber) {
		_itemDataDict[itemId].MaxStackableNumber = maxStackableNumber;
	}

	public Area GetAreaByUnitPos(Vector2Int unitPos) {
		return _areaList[unitPos.x][unitPos.y];
	}
	#endregion
    
	#region PrivateMethod
	protected override void Init()
	{
		GenerateWorld();
	}

	private void GenerateWorld() {
		LoadInitialSettings();
		GenerateTiles();
		_aStar.Scan();
		_areaDict[0][0].Open();
	}

	private void LoadInitialSettings() {
		List<ObjectData> objectDataList = new List<ObjectData>();
		foreach (var data in _originalObjectDataList) {
			objectDataList.Add(new ObjectData(data.objectData));
		}
		foreach (var data in _originalAnimalDataList)
		{
			objectDataList.Add(new AnimalData(data.objectData));
		}
		_objectDataDict = objectDataList.ToDictionary(o => o.objectID);
		
		List<ItemData> itemDataList = new List<ItemData>();
		foreach (var data in _originalItemDataList) {
			itemDataList.Add(new ItemData(data.itemData));
		}
		_itemDataDict = itemDataList.ToDictionary(i => i.ItemID);

		_worldSetting = new WorldSetting(_originalWorldSetting.worldSetting);
	}

	private void GenerateTiles() {
		_areaDict = new Dictionary<int, List<Area>>();
		_areaList = new List<List<Area>>();
		
		int wholeWorldUnitSize = _worldSetting.sectionSettings.Length + 1;
		for (int i = 0; i < (_worldSetting.sectionSettings.Length + 1); i++) {
			List<Area> areaList = new List<Area>();
			for (int j = 0; j < (_worldSetting.sectionSettings.Length + 1); j++) {
				areaList.Add(null);
			}
			_areaList.Add(areaList);
		}
		
		List<Area> area0List = new List<Area>();
		Area area0 = Instantiate(_worldSetting.sectionSettings[0].sectionPrefab).GetComponent<Area>();
		area0.Init(0, 0, Vector2Int.zero, GetSectionSetting(0));
		area0List.Add(area0);
		_areaDict.Add(0, area0List);
		_areaList[0][0] = area0;

		int gap = _worldSetting.areaSize + _areaPadding;
		int areaIdx;
		int areaUnitPosX;
		int areaUnitPosY;
		for (int i = 1; i < _worldSetting.sectionSettings.Length; i++) {
			List<Area> areaList = new List<Area>();
			int leftUpperMostX = -(gap * ((i + 1) / 2));
			int leftUpperMostY = gap * ((i + 1) / 2);
			areaIdx = 0;
			for (int j = 0; j < 4; j++) {
				if (i % 2 == 0) {
					Area area = Instantiate(_worldSetting.sectionSettings[i].sectionPrefab).GetComponent<Area>();
					int x = leftUpperMostX + (j % 2 == 0 ? 0 : (gap * i));
					int y = leftUpperMostY + (j / 2 == 0 ? 0 : -(gap * i));
					area.transform.position = new Vector3(x, y, 0);

					areaUnitPosX = x / gap + (wholeWorldUnitSize / 2);
					areaUnitPosY = y / gap + (wholeWorldUnitSize / 2);
					_areaList[areaUnitPosX][areaUnitPosY] = area;

					area.Init(i, areaIdx, new Vector2Int(areaUnitPosX, areaUnitPosY), GetSectionSetting(i));
					areaList.Add(area);

					areaIdx++;
				} else {
					int startX;
					int startY;
					if (j == 0) {
						startX = leftUpperMostX + gap;
						startY = leftUpperMostY;
					} else if (j == 1) {
						startX = leftUpperMostX + gap;
						startY = leftUpperMostY - (gap * (i + 1));
					} else if (j == 2) {
						startX = leftUpperMostX;
						startY = leftUpperMostY - gap;
					} else {
						startX = leftUpperMostX + (gap * (i + 1));
						startY = leftUpperMostY - gap;
					}

					for (int k = 0; k < i; k++) {
						Area area = Instantiate(_worldSetting.sectionSettings[i].sectionPrefab).GetComponent<Area>();
						int x = startX + (j / 2 == 0 ? gap * k : 0);
						int y = startY + (j / 2 == 0 ? 0 : -gap * k);
						area.transform.position = new Vector3(x, y, 0);
						
						areaUnitPosX = x / gap + (wholeWorldUnitSize / 2);
						areaUnitPosY = y / gap + (wholeWorldUnitSize / 2);
						_areaList[areaUnitPosX][areaUnitPosY] = area;

						area.Init(i, areaIdx, new Vector2Int(areaUnitPosX, areaUnitPosY), GetSectionSetting(i));
						areaList.Add(area);

						areaIdx++;
					}
				}	
			}
			if (i == 7) {
				for (int j = 0; j < 4; j++) {
					Area area = Instantiate(_worldSetting.sectionSettings[i].sectionPrefab).GetComponent<Area>();
					int x = leftUpperMostX + (j % 2 == 0 ? 0 : (gap * (i+1)));
					int y = leftUpperMostY + (j / 2 == 0 ? 0 : -(gap * (i+1)));
					area.transform.position = new Vector3(x, y, 0);

					areaUnitPosX = x / gap + (wholeWorldUnitSize / 2);
					areaUnitPosY = y / gap + (wholeWorldUnitSize / 2);
					_areaList[areaUnitPosX][areaUnitPosY] = area;

					area.Init(i, areaIdx, new Vector2Int(areaUnitPosX, areaUnitPosY), GetSectionSetting(i));
					areaList.Add(area);

					areaIdx++;
				} 
			}
			_areaDict.Add(i, areaList);
		}
	}
	#endregion
}

}