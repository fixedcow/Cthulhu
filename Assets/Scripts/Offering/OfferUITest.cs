using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

namespace TH.Core {

public class OfferUITest : MonoBehaviour
{
    #region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private OfferCardUITest[] _cardUI;
	#endregion

	#region PublicMethod
	[Button]
	public void SetCardUI() {
		for (int i = 0; i < _cardUI.Length; i++) {
			_cardUI[i].SetText(
				OfferingManager.Instance.OfferingData.GetTierPresents(1)[i].GetName(), 
				OfferingManager.Instance.OfferingData.GetTierPresents(1)[i].GetDescription(),
				() => OfferingManager.Instance.OfferingData.GetTierPresents(1)[2].InvokePresent(),
				UnselectOther,
				i
			);
		}
	}

	public void UnselectOther(int idx) {
		for (int i = 0; i < _cardUI.Length; i++) {
			if (i != idx) {
				_cardUI[i].Unselect();
			}
		}
	}

	public void Deactivate() {
		gameObject.SetActive(false);
	}
	#endregion
    
	#region PrivateMethod
	private void OnEnable() {
		SetCardUI();
	}
	#endregion
}

}