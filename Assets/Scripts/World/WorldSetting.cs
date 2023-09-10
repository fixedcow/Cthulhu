using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using ListDrawer = Sirenix.OdinInspector.ListDrawerSettingsAttribute;

namespace TH.Core {

[Serializable]
public class WorldSetting
{
    #region PublicVariables
	[SerializeField] public int areaSize = 12;
	[ListDrawer(HideRemoveButton = true, ShowIndexLabels = true), InfoBox("각 티어 별 스폰 정보를 설정합니다.")]
	[SerializeField] public SectionSetting[] sectionSettings = new SectionSetting[8];
	#endregion

	#region PrivateVariables
	#endregion

	#region PublicMethod
	public WorldSetting(WorldSetting worldSetting) {
		areaSize = worldSetting.areaSize;
		sectionSettings = new SectionSetting[worldSetting.sectionSettings.Length];
		for (int i = 0; i < worldSetting.sectionSettings.Length; i++) {
			sectionSettings[i] = new SectionSetting(worldSetting.sectionSettings[i]);
		}
	}
	#endregion
    
	#region PrivateMethod
	#endregion

	[Serializable]
	public class SectionSetting {
		public GameObject sectionPrefab;
		public List<SpawnObjectSetting> spawnBerrySettings = new List<SpawnObjectSetting>();
		public List<SpawnObjectSetting> spawnMineSettings = new List<SpawnObjectSetting>();
		public List<AnimalSpawnObjectSetting> spawnAnimalSettings = new List<AnimalSpawnObjectSetting>();
		
		private Dictionary<string, SpawnObjectSetting> spawnObjectSettingDict;

		public SectionSetting(SectionSetting sectionSetting) {
			sectionPrefab = sectionSetting.sectionPrefab;
			spawnBerrySettings = new List<SpawnObjectSetting>();
			foreach (SpawnObjectSetting spawnObjectSetting in sectionSetting.spawnBerrySettings) {
				spawnBerrySettings.Add(new SpawnObjectSetting(spawnObjectSetting));
			}

			spawnMineSettings = new List<SpawnObjectSetting>();
			foreach (SpawnObjectSetting spawnObjectSetting in sectionSetting.spawnMineSettings) {
				spawnMineSettings.Add(new SpawnObjectSetting(spawnObjectSetting));
			}

			spawnAnimalSettings = new List<AnimalSpawnObjectSetting>();
			foreach (AnimalSpawnObjectSetting animalSpawnObjectSetting in sectionSetting.spawnAnimalSettings) {
				spawnAnimalSettings.Add(new AnimalSpawnObjectSetting(animalSpawnObjectSetting));
			}

			spawnObjectSettingDict = new Dictionary<string, SpawnObjectSetting>();

			foreach (SpawnObjectSetting spawnObjectSetting in spawnBerrySettings) {
				spawnObjectSettingDict.Add(spawnObjectSetting.objectID, spawnObjectSetting);
			}
			foreach (SpawnObjectSetting spawnObjectSetting in spawnMineSettings) {
				spawnObjectSettingDict.Add(spawnObjectSetting.objectID, spawnObjectSetting);
			}
			foreach (SpawnObjectSetting spawnObjectSetting in spawnAnimalSettings) {
				spawnObjectSettingDict.Add(spawnObjectSetting.objectID, spawnObjectSetting);
			}
		}

		public SpawnObjectSetting GetSpawnObjectSetting(string objectID) {
			return spawnObjectSettingDict[objectID];
		}
	}

	[Serializable]
	public class  SpawnObjectSetting {
		#region PublicVariables
		[InfoBox("스폰 될 오브젝트 아이디")]
		public string objectID;

		[InfoBox("월드 생성 시 스폰 여부")]
		public bool isSpawnOnLoad;
		[ShowIf("isSpawnOnLoad", true), InfoBox("월드 생성 시 최초 스폰 개수")]
		public int initialSpawnCount;
		[InfoBox("최대 스폰 개수")]
		public int spawnCountMax;
		[Range(1, 100), InfoBox("최대 스폰 개수에 도달 시 스폰 주기 계수")]
		public float cycleCoefficientOnMax = 1;

		[InfoBox("스폰 주기 최소값")]
		public float spawnCycleMin;
		[InfoBox("스폰 주기 최대값")]
		public float spawnCycleMax;

		[ValueDropdown("precisions"), InfoBox("스폰 주기 분포 정밀도")]
		public int distributePrecision;
		#endregion

		#region PrivateVariables
		private static int[] precisions = new int[] { 2, 3, 4, 6 };
		#endregion

		#region PublicMethod
		public SpawnObjectSetting (SpawnObjectSetting spawnObjectSetting) {
			objectID = spawnObjectSetting.objectID;
			isSpawnOnLoad = spawnObjectSetting.isSpawnOnLoad;
			initialSpawnCount = spawnObjectSetting.initialSpawnCount;
			spawnCountMax = spawnObjectSetting.spawnCountMax;
			cycleCoefficientOnMax = spawnObjectSetting.cycleCoefficientOnMax;
			spawnCycleMin = spawnObjectSetting.spawnCycleMin;
			spawnCycleMax = spawnObjectSetting.spawnCycleMax;
			distributePrecision = spawnObjectSetting.distributePrecision;
		}
		#endregion
	}

	[Serializable]
	public class AnimalSpawnObjectSetting : SpawnObjectSetting {
		#region PublicVariables
		[InfoBox("스폰 조건 - 어느 섹션 기준인가?")]
		public int spawnConditionOnSection;
		[InfoBox("스폰 조건 - 해당 섹션이 얼마나 해금되었는가")]
		public int spawnConditionOnOpenedAreaNumber;

		public AnimalSpawnObjectSetting(AnimalSpawnObjectSetting animalSpawnObjectSetting) : base(animalSpawnObjectSetting) {
			spawnConditionOnSection = animalSpawnObjectSetting.spawnConditionOnSection;
			spawnConditionOnOpenedAreaNumber = animalSpawnObjectSetting.spawnConditionOnOpenedAreaNumber;
		}
		#endregion

		#region PrivateVariables
		#endregion
	}

	[Serializable]
	public class MineSpawnObjectSetting: SpawnObjectSetting {
		public MineSpawnObjectSetting(MineSpawnObjectSetting mineSpawnObjectSetting) : base(mineSpawnObjectSetting) { }
	}
	
}

}