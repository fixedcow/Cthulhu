using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using Sirenix.Utilities;

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
	private static void A() {
		Debug.Log("A");
	}

	private static void B() {
		Debug.Log("B");
	}

	private static void ExpandInventoryMaximumSize() {
		InventoryOwner player = FindObjectOfType<InventoryOwner>();
		//InventorySystem.Instance.GetInventory(player).ExpandMaximumSize(1);
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
			presentFunction.presentAction();
		}

		public static IEnumerable presentPool = new ValueDropdownList<PresentFunction>()
		{
			{ "A", new PresentFunction(A) },
			{ "B", new PresentFunction(B) },
		};
	}

	[Serializable]
	public class PresentFunction {
		public Action presentAction;

		public PresentFunction(Action action) {
			presentAction = action;
			int a = 1;
		}

		public override string ToString() {
			return presentAction.Method.Name;
		}
	}
	#endregion
}

}