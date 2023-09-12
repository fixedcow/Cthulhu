using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace TH.Core {

public class OfferUITest : MonoBehaviour
{
    #region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private OfferCardUITest[] _cardUI;
	[SerializeField] private GameObject _confirmButton;
	#endregion

	#region PublicMethod
	[Button]
	public void SetCardUI() {
		for (int i = 0; i < _cardUI.Length; i++) {
			_cardUI[i].SetText(
				OfferingManager.Instance.OfferingData.GetTierPresents(GameManager.Instance.Level)[i].GetName(), 
				OfferingManager.Instance.OfferingData.GetTierPresents(GameManager.Instance.Level)[i].GetDescription(),
				(k) => OfferingManager.Instance.OfferingData.GetTierPresents(GameManager.Instance.Level)[k].InvokePresent(),
				UnselectOther,
				i
			);;
		}
		_confirmButton.SetActive(false);
	}

	public void UnselectOther(int idx) {
		for (int i = 0; i < _cardUI.Length; i++) {
			if (i != idx) {
				_cardUI[i].Unselect();
			}
		}
		_confirmButton.SetActive(true);
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