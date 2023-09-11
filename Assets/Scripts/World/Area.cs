using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SectionSetting = TH.Core.WorldSetting.SectionSetting;
using SpawnObjectSetting = TH.Core.WorldSetting.SpawnObjectSetting;
using AnimalSpawnObjectSetting = TH.Core.WorldSetting.AnimalSpawnObjectSetting;
using UnityEngine.Tilemaps;
using DG.Tweening;

namespace TH.Core {

public class Area : MonoBehaviour
{
    #region PublicVariables 
	public IReadOnlyList<IReadOnlyList<WorldObject>> SpawnObjectData => _spawnObjectData.AsReadOnly();
	public bool HasOpened => _hasOpened;

	public Vector2Int UnitPos => _unitPos;
	public int Section { get { return _section; } }
	public int AreaIdx { get { return _areaIdx; } }
	#endregion

	#region PrivateVariables
	private int _section;
	private int _areaIdx;
	private Vector2Int _unitPos;
	private SectionSetting _sectionSetting;
	private List<List<WorldObject>> _spawnObjectData;

	private List<SpawnData> _berrySpawnDatas = new List<SpawnData>();
	private List<SpawnData> _mineSpawnDatas = new List<SpawnData>();
	private List<SpawnData> _animalSpawnDatas = new List<SpawnData>();
	
	[SerializeField] private Tilemap _areaTilemap;
	private TileTradeTriggerGroup _triggerGroup;

	private bool _hasOpened = false;

	#endregion

	#region PublicMethod
	public void Init(int section, int areaIdx, Vector2Int unitPos, SectionSetting sectionSetting)
	{
		_section = section;
		_areaIdx = areaIdx;
		_unitPos = unitPos;
		_sectionSetting = sectionSetting;

		int areaSize = WorldManager.Instance.GetAreaSize();
		_spawnObjectData = new List<List<WorldObject>>(areaSize);
		for (int i = 0; i < areaSize; i++) {
			_spawnObjectData.Add(new List<WorldObject>(areaSize));
			for (int j = 0; j < areaSize; j++) {
				_spawnObjectData[i].Add(null);
			}
		}

		transform.Find("Trigger").TryGetComponent(out _triggerGroup);
		_triggerGroup.SetArea(_unitPos);

		_hasOpened = false;
		_areaTilemap.gameObject.SetActive(false);
	}

	public void Open() {
		_hasOpened = true;
		_areaTilemap.gameObject.SetActive(true);
		_areaTilemap.transform.localScale = Vector3.zero;
		StartCoroutine(OpenAnimCoroutine());
	}

	public void StartSpawn() {
		foreach (SpawnObjectSetting spawnObjectSetting in _sectionSetting.spawnBerrySettings) {
			SpawnData spawnData = new SpawnData(
				this, 
				spawnObjectSetting.objectID, 
				Random.Range(spawnObjectSetting.spawnCycleMin, spawnObjectSetting.spawnCycleMax),
				spawnObjectSetting.distributePrecision
			);
			_berrySpawnDatas.Add(spawnData);

			if (spawnObjectSetting.isSpawnOnLoad == true) {
				for (int i = 0; i < spawnObjectSetting.initialSpawnCount; i++) {
					Vector2Int spawnPos = spawnData.NextSpawnPos();
					if (spawnPos.x == -1) {
						break;
					} else {
						_spawnObjectData[spawnPos.x][spawnPos.y] = SpawnObject(spawnData.ObjectID, spawnPos);
					}
				}
			}
		}

		foreach (SpawnObjectSetting spawnObjectSetting in _sectionSetting.spawnMineSettings) {
			SpawnData spawnData = new SpawnData(
				this, 
				spawnObjectSetting.objectID, 
				Random.Range(spawnObjectSetting.spawnCycleMin, spawnObjectSetting.spawnCycleMax),
				spawnObjectSetting.distributePrecision
			);
			
			_mineSpawnDatas.Add(spawnData);

			if (spawnObjectSetting.isSpawnOnLoad == true) {
				for (int i = 0; i < spawnObjectSetting.initialSpawnCount; i++) {
					Vector2Int spawnPos = spawnData.NextSpawnPos();
					if (spawnPos.x == -1) {
						break;
					} else {
						_spawnObjectData[spawnPos.x][spawnPos.y] = SpawnObject(spawnData.ObjectID, spawnPos);
					}
				}
			}
		}

		foreach (AnimalSpawnObjectSetting animalSpawnObjectSetting in _sectionSetting.spawnAnimalSettings) {
			SpawnData spawnData = new SpawnData(
				this, 
				animalSpawnObjectSetting.objectID, 
				Random.Range(animalSpawnObjectSetting.spawnCycleMin, animalSpawnObjectSetting.spawnCycleMax),
				animalSpawnObjectSetting.distributePrecision
			);
			
			_animalSpawnDatas.Add(spawnData);

			if (animalSpawnObjectSetting.isSpawnOnLoad == true) {
				for (int i = 0; i < animalSpawnObjectSetting.initialSpawnCount; i++) {
					Vector2Int spawnPos = spawnData.NextSpawnPos();
					if (spawnPos.x == -1) {
						break;
					} else {
						_spawnObjectData[spawnPos.x][spawnPos.y] = SpawnObject(spawnData.ObjectID, spawnPos);
					}
				}
			}
		}
	}

	public float GetMinSpawnCycle(string objectID) {
		return WorldManager.Instance.GetSectionSetting(_section).GetSpawnObjectSetting(objectID).spawnCycleMin;
	}

	public float GetMaxSpawnCycle(string objectID) {
		return WorldManager.Instance.GetSectionSetting(_section).GetSpawnObjectSetting(objectID).spawnCycleMax;
	}

	public int GetMaxSpawnCount(string objectID) {
		return WorldManager.Instance.GetSectionSetting(_section).GetSpawnObjectSetting(objectID).spawnCountMax;
	}

	public float GetCoefficient(string objectID) {
		return WorldManager.Instance.GetSectionSetting(_section).GetSpawnObjectSetting(objectID).cycleCoefficientOnMax;
	}
	#endregion
    
	#region PrivateMethod;
	private void Update() 
	{
		if (_hasOpened == false) {
			return;
		}

		foreach (SpawnData spawnData in _berrySpawnDatas) {
			if (spawnData.HasSpawnStarted == false) {
				spawnData.StartSpawn();
			}

			if (spawnData.CheckSpawn(out Vector2Int spawnPos)) {
				_spawnObjectData[spawnPos.x][spawnPos.y] = SpawnObject(spawnData.ObjectID, spawnPos);
			}
		}

		foreach (SpawnData spawnData in _mineSpawnDatas) {
			if (spawnData.HasSpawnStarted == false) {
				spawnData.StartSpawn();
			}

			if (spawnData.CheckSpawn(out Vector2Int spawnPos)) {
				_spawnObjectData[spawnPos.x][spawnPos.y] = SpawnObject(spawnData.ObjectID, spawnPos);
			}
		}

		foreach (SpawnData spawnData in _animalSpawnDatas) {
			if (spawnData.HasSpawnStarted == false) {
				int spawnConditionSection = _sectionSetting.spawnAnimalSettings.Find(s => s.objectID == spawnData.ObjectID).spawnConditionOnSection;
				int spawnConditionCount = _sectionSetting.spawnAnimalSettings.Find(s => s.objectID == spawnData.ObjectID).spawnConditionOnOpenedAreaNumber;
				if (WorldManager.Instance.GetOpenedAreaCount(spawnConditionSection) >= spawnConditionCount) {
					spawnData.StartSpawn();
				}
			}

			if (spawnData.CheckSpawn(out Vector2Int spawnPos)) {
				_spawnObjectData[spawnPos.x][spawnPos.y] = SpawnObject(spawnData.ObjectID, spawnPos);
			}
		}
	}

	private WorldObject SpawnObject(string objectID, Vector2Int spawnPos) {
		ObjectData objectData = WorldManager.Instance.GetObjectData(objectID);
		WorldObject worldObject = Instantiate(objectData.objectPrefab, _areaTilemap.transform).GetComponent<WorldObject>();
		worldObject.transform.localPosition = GetRealPos(spawnPos.x, spawnPos.y);
		worldObject.Init(objectID, spawnPos, OnObjectDestroyed);
		return worldObject;
	}

	private Vector2 GetRealPos(int x, int y) {
		Vector2Int tilePos = new Vector2Int(x - (WorldManager.Instance.GetAreaSize() / 2), y - (WorldManager.Instance.GetAreaSize() / 2));
		return _areaTilemap.GetCellCenterLocal((Vector3Int)tilePos);
	}

	private void OnObjectDestroyed(string objectID, Vector2Int areaPos) {
		_spawnObjectData[areaPos.x][areaPos.y] = null;
	}

	private IEnumerator OpenAnimCoroutine() {
		_areaTilemap.transform.DOJump(_areaTilemap.transform.position, 1f, 1, 0.5f);
		_areaTilemap.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
		yield return new WaitForSeconds(0.5f);
		StartSpawn();
	}

	#endregion
}

}