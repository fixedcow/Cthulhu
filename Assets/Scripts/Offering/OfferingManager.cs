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

public class OfferingManager : Singleton<OfferingManager>
{
    #region PublicVariables
	public OfferingData OfferingData => _offeringData;
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
			return "그분의 자비로 이 작은 소원을 이루어주시기를.";
		}

		static void RealAction() {
			InventoryOwner player = FindObjectOfType<InventoryOwner>();
			InventorySystem.Instance.GetInventory(player).ExpandInventory(1);
		}

		return new PresentFunction.Inner(Name, Description, RealAction);
	}

	public static PresentFunction.Inner DoubleMaxStackableObjectAll() {
		static string Name() {
			return "인벤토리 스택 개수 x2";
		}

		static string Description() {
			return "더욱 풍성한 삶을 선물해 주시기를 간청하나이다.";
		}

		static void RealAction() {
			WorldManager.Instance.MultiplyMaxStackableNumber(2);
		}

		return new PresentFunction.Inner(Name, Description, RealAction);
	}

	public static PresentFunction.Inner DoubleMaxStackableCopper() {
		static string Name() {
			return "구리 스택 개수 x2";
		}

		static string Description() {
			return "미약한 제게 자그마한 자비를 베푸소서.";
		}

		static void RealAction() {
			WorldManager.Instance.MultiplyMaxStackableNumber("Copper", 2);
		}

		return new PresentFunction.Inner(Name, Description, RealAction);
	}

	public static PresentFunction.Inner ExpandInventoryByTwo() {
		static string Name() {
			return "인벤토리 +2";
		}

		static string Description() {
			return "그분의 공포는 유한한 공간 너머에 있나니.";
		}

		static void RealAction() {
			InventoryOwner player = FindObjectOfType<InventoryOwner>();
			InventorySystem.Instance.GetInventory(player).ExpandInventory(2);
		}

		return new PresentFunction.Inner(Name, Description, RealAction);
	}

	public static PresentFunction.Inner ExpandInventoryByFour() {
		static string Name() {
			return "인벤토리 +4";
		}

		static string Description() {
			return "그분의 능력에는 한계가 없음이라.";
		}

		static void RealAction() {
			InventoryOwner player = FindObjectOfType<InventoryOwner>();
			InventorySystem.Instance.GetInventory(player).ExpandInventory(4);
		}

		return new PresentFunction.Inner(Name, Description, RealAction);
	}

	public static PresentFunction.Inner MakeBerrySpawnGold() {
		static string Name() {
			return "베리 대신 금";
		}

		static string Description() {
			return "찬양하라! 작은 열매도 그분의 손길에 놀라 빛나리라.";
		}

		static void RealAction() {
			WorldManager.Instance.SetBerrySpawnGold(true);
		}

		return new PresentFunction.Inner(Name, Description, RealAction);
	}

	public static PresentFunction.Inner AddPlayerMaxHealth() {
		static string Name() {
			return "최대체력 +20";
		}

		static string Description() {
			return "그분께서 내려주신 자비에 피로 보답하리라.";
		}

		static void RealAction() {
			GameManager.Instance.GetPlayer().AddMaxHealth(20);
		}

		return new PresentFunction.Inner(Name, Description, RealAction);
	}

	public static PresentFunction.Inner AddPlayerMaxSanity() {
		static string Name() {
			return "최대정신력 +20";
		}

		static string Description() {
			return "공포는 나의 힘이요 혼란은 나의 오랜 친구니라.";
		}

		static void RealAction() {
			GameManager.Instance.GetPlayer().AddMaxSanity(20);
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