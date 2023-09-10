using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core {

public class SpawnData
{
    #region PublicVariables
	#endregion

	#region PrivateVariables
	private Area _area;
	private string _objectID;
	private bool _hasInitialized = false;
	private float _lastSpawnTime;
	private float _spawnInterval;
	private int _unitSize;
	private int _precision;
	#endregion

	#region PublicMethod
	public void Init(Area area, string id, float spawnInterval, int precision) {
		_hasInitialized = true;

		_area = area;
		_objectID = id;
		_spawnInterval = spawnInterval;
		_unitSize = WorldManager.Instance.GetAreaSize() / precision;
		_precision = precision;
	}

	public void CheckSpawn() {
		if (!_hasInitialized) {
			return;
		}

		if (Time.time - _lastSpawnTime > _spawnInterval) {
			_lastSpawnTime = Time.time;
			Vector2Int spawnPos = NextSpawnPos();
			_spawnInterval = NextSpawnCycle();
			if (spawnPos.x != -1) {
				//_area.SpawnObject(_objectID, spawnPos);
			}
		}
	}
	#endregion
    
	#region PrivateMethod
	private Vector2Int NextSpawnPos() {
		Vector2Int unitPos = LeastUnitCountPos();
		if (unitPos.x == -1) {
			return new Vector2Int(-1, -1);
		}

		int x = Random.Range(unitPos.x * _unitSize, (unitPos.x + 1) * _unitSize);
		int y = Random.Range(unitPos.y * _unitSize, (unitPos.y + 1) * _unitSize);

		return new Vector2Int(x, y);
	}

	private float NextSpawnCycle() {
		float min = _area.GetMinSpawnCycle(_objectID);
		float max = _area.GetMaxSpawnCycle(_objectID);

		if (AllCount() >= _area.GetMaxSpawnCount(_objectID)) {
			min *= _area.GetCoefficient(_objectID);
			max *= _area.GetCoefficient(_objectID);
		}

		return Random.Range(min, max);
	}

	private Vector2Int LeastUnitCountPos() {
		int leastCount = _unitSize * _unitSize;
		Vector2Int leastUnitPos = new Vector2Int(_precision, _precision);

		for (int i = 0; i < _precision; i++) {
			for (int j = 0; j < _precision; j++) {
				int unitAreaCount = GetUnitAreaObjectCount(i, j);
				if (unitAreaCount < leastCount) {
					leastCount = unitAreaCount;
					leastUnitPos = new Vector2Int(i, j);
				}
			}
		}

		if (leastCount == _unitSize * _unitSize) {
			return new Vector2Int(-1, -1);
		}
		return leastUnitPos;
	}

	private int GetUnitAreaObjectCount(int x, int y) {
		int count = 0;
		for (int i = 0; i < _unitSize; i++) {
			for (int j = 0; j < _unitSize; j++) {
				if (_area.SpawnObjectData[x * _unitSize + i][y * _unitSize + j] == _objectID) {
					count++;
				}
			}
		}
		return count;
	}

	private int AllCount() {
		int count = 0;
		for (int i = 0; i < _precision; i++) {
			for (int j = 0; j < _precision; j++) {
				count += GetUnitAreaObjectCount(i, j);
			}
		}
		return count;
	}
	#endregion
}

}