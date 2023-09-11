using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;

namespace TH.Core {

public class OfferingManager : MonoBehaviour
{
    #region PublicVariables
	#endregion

	#region PrivateVariables
	//[ValueDropdown("presents"), InfoBox("크툴루의 선물")]
	[SerializeField] private PresentOfCthulhu targetAction;
	#endregion

	#region PublicMethod
	#endregion
    
	#region PrivateMethod
	private static void A() {
		Debug.Log("A");
	}
	private static void B() {}

	[Button]
	private void wow() {
		targetAction.action();
	}

	//private PresentOfCthulhu[] presents = new PresentOfCthulhu[2] {foo, bar};

	private PresentOfCthulhu foo = new PresentOfCthulhu(A);
	private PresentOfCthulhu bar = new PresentOfCthulhu(B);

	[Serializable]
	public class PresentOfCthulhu {
		public Action action;

		public PresentOfCthulhu(Action action) {
			this.action = action;
		}
	}
	#endregion
}

}