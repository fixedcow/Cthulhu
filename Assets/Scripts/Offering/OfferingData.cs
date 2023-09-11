using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

namespace TH.Core {

[CreateAssetMenu(fileName = "OfferingSetting", menuName = "TH/Offering Setting", order = 0)]
public class OfferingData : ScriptableObject
{
	const string EXPAND_INVENTORY_BY_ONE = "Expand Inventory +1";
	const string EXPAND_INVENTORY_BY_TWO = "Expand Inventory +2";
	const string EXPAND_INVENTORY_BY_THREE = "Expand Inventory +3";
	const string DOUBLE_MAX_STACKABLE_OBJECT_ALL = "Double Max Stackable Object All";
	const string TRIPLE_MAX_STACKABLE_OBJECT_ALL = "Triple Max Stackable Object All";
	const string DOUBLE_MAX_STACKABLE_OBJECT_COPPER = "Double Max Stackable Object Copper";
	const string DOUBLE_MAX_STACKABLE_OBJECT_CHICKEN = "Double Max Stackable Object Chicken";
	const string ADD_PLAYER_MAX_HP = "Add Player Max HP +20";
	const string ADD_PLAYER_MAX_SP = "Add Player Max SP +20";
	const string MAKE_BERRY_SPAWN_GOLD = "Make Berry Spawn Gold";
	const string MAKE_COPPER_SPAWN_SILVER = "Make Copper Spawn Silver";
	const string MAKE_ORE_SPAWN_FASTER = "Make Ore Spawn Faster";
	const string MAKE_BERRY_DROP_MORE = "Make Berry Drop More";
	const string MAKE_ORE_DROP_MORE = "Make Ore Drop More";
	const string MAKE_ANIMAL_DROP_MORE = "Make Animal Drop More";

	readonly static Dictionary<string, Func<PresentFunction.Inner>> presentFunctionPool = new Dictionary<string, Func<PresentFunction.Inner>>() {
		{ EXPAND_INVENTORY_BY_ONE, OfferingManager.ExpandInventoryByOne },
		{ EXPAND_INVENTORY_BY_TWO, OfferingManager.ExpandInventoryByTwo },
		{ EXPAND_INVENTORY_BY_THREE, OfferingManager.ExpandInventoryByThree },
		{ DOUBLE_MAX_STACKABLE_OBJECT_ALL, OfferingManager.DoubleMaxStackableObjectAll },
		{ TRIPLE_MAX_STACKABLE_OBJECT_ALL, OfferingManager.TripleMaxStackableObjectAll },
		{ DOUBLE_MAX_STACKABLE_OBJECT_COPPER, OfferingManager.DoubleMaxStackableCopper },
		{ DOUBLE_MAX_STACKABLE_OBJECT_CHICKEN, OfferingManager.DoubleMaxStackableChicken },
		{ ADD_PLAYER_MAX_HP, OfferingManager.AddPlayerMaxHealth },
		{ ADD_PLAYER_MAX_SP, OfferingManager.AddPlayerMaxSanity },
		{ MAKE_BERRY_SPAWN_GOLD, OfferingManager.MakeBerrySpawnGold },
		{ MAKE_COPPER_SPAWN_SILVER, OfferingManager.MakeCopperSpawnSilver },
		{ MAKE_ORE_SPAWN_FASTER, OfferingManager.MakeOreSpawnFaster },
		{ MAKE_BERRY_DROP_MORE, OfferingManager.MakeBerryDropMore },
		{ MAKE_ORE_DROP_MORE, OfferingManager.MakeOreDropMore },
		{ MAKE_ANIMAL_DROP_MORE, OfferingManager.MakeAnimalDropMore },
	};

    #region PublicVariables
	#endregion

	#region PrivateVariables
	[InfoBox("크툴루의 선물")]
	[SerializeField] private List<PresentOfCthulhu> tier1Presents = new List<PresentOfCthulhu>(3);
	[SerializeField] private List<PresentOfCthulhu> tier2Presents = new List<PresentOfCthulhu>(3);
	[SerializeField] private List<PresentOfCthulhu> tier3Presents = new List<PresentOfCthulhu>(3);
	[SerializeField] private List<PresentOfCthulhu> tier4Presents = new List<PresentOfCthulhu>(3);
	[SerializeField] private List<PresentOfCthulhu> tier5Presents = new List<PresentOfCthulhu>(3);
	[SerializeField] private List<PresentOfCthulhu> tier6Presents = new List<PresentOfCthulhu>(3);
	[SerializeField] private List<PresentOfCthulhu> tier7Presents = new List<PresentOfCthulhu>(3);
	[SerializeField] private List<PresentOfCthulhu> tier8Presents = new List<PresentOfCthulhu>(3);
	[SerializeField] private List<PresentOfCthulhu> tier9Presents = new List<PresentOfCthulhu>(3);
	#endregion

	#region PublicMethod
	public List<PresentOfCthulhu> GetTierPresents(int tier) {
		switch (tier) {
			case 1:
				return tier1Presents;
			case 2:
				return tier2Presents;
			case 3:
				return tier3Presents;
			case 4:
				return tier4Presents;
			case 5:
				return tier5Presents;
			case 6:
				return tier6Presents;
			case 7:
				return tier7Presents;
			case 8:
				return tier8Presents;
			case 9:
				return tier9Presents;
			default:
				return null;
		}
	}
	#endregion
    
	#region PrivateMethod
	#endregion	

	[Serializable]
	public class PresentOfCthulhu {
		[ValueDropdown("presentPool")]
		[SerializeField] private string cthulhuPresent;

		public void InvokePresent() {
			GetPresentFunction().Run().RealAction();
		}

		public string GetDescription() {
			return GetPresentFunction().Run().Description();
		}

		public string GetName() {
			return GetPresentFunction().Run().Name();
		}

		public static readonly List<string> presentPool = new List<string>() {
			EXPAND_INVENTORY_BY_ONE,
			EXPAND_INVENTORY_BY_TWO,
			EXPAND_INVENTORY_BY_THREE,
			DOUBLE_MAX_STACKABLE_OBJECT_ALL,
			TRIPLE_MAX_STACKABLE_OBJECT_ALL,
			DOUBLE_MAX_STACKABLE_OBJECT_COPPER,
			DOUBLE_MAX_STACKABLE_OBJECT_CHICKEN,
			ADD_PLAYER_MAX_HP,
			ADD_PLAYER_MAX_SP,
			MAKE_BERRY_SPAWN_GOLD,
			MAKE_COPPER_SPAWN_SILVER,
			MAKE_ORE_SPAWN_FASTER,
			MAKE_BERRY_DROP_MORE,
			MAKE_ORE_DROP_MORE,
			MAKE_ANIMAL_DROP_MORE,
		};

		private PresentFunction GetPresentFunction() {
			return new PresentFunction(presentFunctionPool[cthulhuPresent]);
		}
	}

	[Serializable]
	public struct PresentFunction {
		private Func<Inner> presentAction;

		private string defaultMsg;

		public PresentFunction(Func<Inner> action, string defaultMsg = "선물을 선택해주세요.") {
			this.presentAction = action;
			this.defaultMsg = defaultMsg;
		}

		public Inner Run() {
			return presentAction.Invoke();
		}

		public override string ToString() {
			if (presentAction != null) {
				return presentAction.Method.Name;
			}
			return defaultMsg;
		}

		public class Inner {
			public Func<string> Name;
			public Func<string> Description;
			public Action RealAction;

			public Inner(Func<string> name, Func<string> description, Action realAction) {
				Name = name;
				Description = description;
				RealAction = realAction;
			}
		}
	}
}

}