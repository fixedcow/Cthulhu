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
			return "은혜와 축복으로 이루어진 이 작은 소원을 이루어주시기를.";
		}

		static void RealAction() {
			InventoryOwner player = FindObjectOfType<InventoryOwner>();
			InventorySystem.Instance.GetInventory(player).ExpandInventory(1);
		}

		return new PresentFunction.Inner(Name, Description, RealAction);
	}

	public static PresentFunction.Inner DoubleMaxStackableObjectAll() {
		static string Name() {
			return "최대 스택 가능 개수 x2";
		}

		static string Description() {
			return "더 많은 축복과 풍성한 삶을 선물해 주시기를 간절히 기도합니다.";
		}

		static void RealAction() {
			WorldManager.Instance.MultiplyMaxStackableNumber(2);
		}

		return new PresentFunction.Inner(Name, Description, RealAction);
	}

	public static PresentFunction.Inner ExpandInventoryByTwo() {
		static string Name() {
			return "인벤토리 +2";
		}

		static string Description() {
			return "은혜와 인도로 인벤토리가 번성하고 풍요롭게.";
		}

		static void RealAction() {
			InventoryOwner player = FindObjectOfType<InventoryOwner>();
			InventorySystem.Instance.GetInventory(player).ExpandInventory(1);
		}

		return new PresentFunction.Inner(Name, Description, RealAction);
	}

	public static PresentFunction.Inner ExpandInventoryByFour() {
		static string Name() {
			return "인벤토리 +4";
		}

		static string Description() {
			return "은혜와 축복으로 이루어진 이 작은 소원을 이루어주시기를.";
		}

		static void RealAction() {
			InventoryOwner player = FindObjectOfType<InventoryOwner>();
			InventorySystem.Instance.GetInventory(player).ExpandInventory(1);
		}

		return new PresentFunction.Inner(Name, Description, RealAction);
	}

	public static PresentFunction.Inner 

	[Button]
	private void wow(int tier, int idx) {
		_offeringData.GetTierPresents(tier)[idx].InvokePresent();
	}
	#endregion
}

}