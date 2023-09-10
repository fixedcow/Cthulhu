using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core {

public class SpawnData
{
    #region PublicVariables
	public string ObjectID => _objectID;
	public bool HasSpawnStarted => _hasSpawnStarted;
	#endregion

	#region PrivateVariables
	private Area _area;
	private string _objectID;
	private bool _hasInitialized = false;
	private float _lastSpawnTime;
	private float _spawnInterval;
	private int _unitSize;
	private int _precision;
	private bool _hasSpawnStarted = false;
	private bool _isCountMax = false;
	#endregion

	#region PublicMethod
	public SpawnData(Area area, string id, float spawnInterval, int precision) {
		_hasInitialized = true;
		_hasSpawnStarted = false;

		_area = area;
		_objectID = id;
		_spawnInterval = spawnInterval;
		_unitSize = WorldManager.Instance.GetAreaSize() / precision;
		_precision = precision;
	}

	public void StartSpawn() {
		_hasSpawnStarted = true;
		_lastSpawnTime = Time.time;
	}

	public bool CheckSpawn(out Vector2Int spawnPos) {
		if (!_hasInitialized) {
			spawnPos = new Vector2Int(-1, -1);
			return false;
		}

		if (Time.time - _lastSpawnTime > _spawnInterval) {
			_lastSpawnTime = Time.time;
			spawnPos = NextSpawnPos();
			_spawnInterval = NextSpawnCycle();
			if (spawnPos.x != -1) {
				return true;
			}
		}

		if (_isCountMax == true && AllCount() < _area.GetMaxSpawnCount(_objectID)) {
			_lastSpawnTime = Time.time;
			_spawnInterval = NextSpawnCycle();
			_isCountMax = false;
		}

		spawnPos = new Vector2Int(-1, -1);
		return false;
	}

	public Vector2Int NextSpawnPos() {
		Vector2Int unitPos = LeastUnitCountPos(out int leftCount);
		if (unitPos.x == -1) {
			return new Vector2Int(-1, -1);
		}

		int idx = Random.Range(0, leftCount);

		for (int i = 0; i < _unitSize; i++) {
			for (int j = 0; j < _unitSize; j++) {
				if (_area.SpawnObjectData[unitPos.x * _unitSize + i][unitPos.y * _unitSize + j] == null) {
					if (idx == 0) {
						return new Vector2Int(unitPos.x * _unitSize + i, unitPos.y * _unitSize + j);
					}
					idx--;
				}
			}
		}

		return new Vector2Int(-1, -1);
	}
	#endregion
    
	#region PrivateMethod
	private float NextSpawnCycle() {
		float min = _area.GetMinSpawnCycle(_objectID);
		float max = _area.GetMaxSpawnCycle(_objectID);

		if (AllCount() >= _area.GetMaxSpawnCount(_objectID)) {
			min *= _area.GetCoefficient(_objectID);
			max *= _area.GetCoefficient(_objectID);
			
			_isCountMax = true;
		}

		return Random.Range(min, max);
	}

	private Vector2Int LeastUnitCountPos(out int leftCount) {
		int leastCount = _unitSize * _unitSize;
		Vector2Int leastUnitPos = new Vector2Int(_precision, _precision);

		for (int i = 0; i < _precision; i++) {
			for (int j = 0; j < _precision; j++) {
				int unitAreaCount = GetUnitAreaObjectCount(i, j);
				if (Random.value > 0.4 ? (unitAreaCount < leastCount) : (unitAreaCount <= leastCount)) {
					leastCount = unitAreaCount;
					leastUnitPos = new Vector2Int(i, j);
				}
			}
		}

		if (leastCount == _unitSize * _unitSize) {
			leftCount = 0;
			return new Vector2Int(-1, -1);
		}
		leftCount = _unitSize * _unitSize - leastCount;
		return leastUnitPos;
	}

	private int GetUnitAreaObjectCount(int x, int y) {
		int count = 0;
		for (int i = 0; i < _unitSize; i++) {
			for (int j = 0; j < _unitSize; j++) {
				if (_area.SpawnObjectData[x * _unitSize + i][y * _unitSize + j] != null) {
					if (_area.SpawnObjectData[x * _unitSize + i][y * _unitSize + j].ObjectID == _objectID) {
						count++;
					}
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