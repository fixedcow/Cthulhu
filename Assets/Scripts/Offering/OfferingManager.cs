using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using Sirenix.Utilities;
using PresentOfCthulhu = TH.Core.OfferingData.PresentOfCthulhu;
using PresentFunction = TH.Core.OfferingData.PresentFunction;

namespace TH.Core {

public class OfferingManager : MonoBehaviour
{
    #region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private OfferingData _offeringData;
	#endregion

	#region PublicMethod
	#endregion
    
	#region PrivateMethod
	public static PresentFunction.Inner ExpandInventoryByOne() {
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

	[Button]
	private void wow(int tier, int idx) {
		_offeringData.GetTierPresents(tier)[idx].InvokePresent();
	}
	#endregion
}

}