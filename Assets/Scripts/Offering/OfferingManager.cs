using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using Sirenix.Utilities;
using System.Runtime.CompilerServices;

namespace TH.Core {

public class OfferingManager : MonoBehaviour
{
    #region PublicVariables
	#endregion

	#region PrivateVariables
	[InfoBox("크툴루의 선물")]
	[SerializeField] private List<PresentOfCthulhu> tier1Presents = new List<PresentOfCthulhu>(3);
	[SerializeField] private List<PresentOfCthulhu> tier2Presents = new List<PresentOfCthulhu>(3);
	[SerializeField] private List<PresentOfCthulhu> tier3Presents = new List<PresentOfCthulhu>(3);
	#endregion

	#region PublicMethod
	#endregion
    
	#region PrivateMethod
	private static PresentFunction.Inner ExpandInventoryByOne() {
		static string Name() {
			return "인벤토리 +1";
		}

		static string Description() {
			return "인벤토리를 1칸 확장합니다.";
		}

		static void RealAction() {
			InventoryOwner player = FindObjectOfType<InventoryOwner>();
			InventorySystem.Instance.GetInventory(player).ExpandInventory(1);
		}

		return new PresentFunction.Inner(Name, Description, RealAction);
	}

	private List<PresentOfCthulhu> GetTierPresents(int tier) {
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

	[Button]
	private void wow(int tier, int idx) {
		GetTierPresents(tier)[idx].InvokePresent();
	}
	

	[Serializable]
	public class PresentOfCthulhu {
		[ValueDropdown("presentPool")]
		[SerializeField] private PresentFunction presentFunction;

		public void InvokePresent() {
			presentFunction.Run().RealAction();
		}

		public string GetDescription() {
			return presentFunction.Run().Description();
		}

		public string GetName() {
			return presentFunction.Run().Name();
		}

		public static IEnumerable presentPool = new ValueDropdownList<PresentFunction>()
		{
			{ "Expand Inventory +1", new PresentFunction(ExpandInventoryByOne) },
		};
	}

	[Serializable]
	public class PresentFunction {
		private Func<Inner> presentAction;

		private string defaultMsg = "";

		public PresentFunction(Func<Inner> action) {
			this.presentAction = action;
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
	#endregion
}

}