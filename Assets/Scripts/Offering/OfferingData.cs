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
	const string EXPAND_INVENTORY_BY_FOUR = "Expand Inventory +4";
	const string MULTIPLY_MAX_STACKABLE_OBJECT_ALL = "Multiply Max Stackable Object All";

	readonly static Dictionary<string, Func<PresentFunction.Inner>> presentFunctionPool = new Dictionary<string, Func<PresentFunction.Inner>>() {
		{ EXPAND_INVENTORY_BY_ONE, OfferingManager.ExpandInventoryByOne },
		{ MULTIPLY_MAX_STACKABLE_OBJECT_ALL, OfferingManager.DoubleMaxStackableObjectAll },
	};

    #region PublicVariables
	#endregion

	#region PrivateVariables
	[InfoBox("크툴루의 선물")]
	[SerializeField] private List<PresentOfCthulhu> tier1Presents = new List<PresentOfCthulhu>(3);
	[SerializeField] private List<PresentOfCthulhu> tier2Presents = new List<PresentOfCthulhu>(3);
	[SerializeField] private List<PresentOfCthulhu> tier3Presents = new List<PresentOfCthulhu>(3);
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
			MULTIPLY_MAX_STACKABLE_OBJECT_ALL,
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