using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core {

public class OfferCardUITest : MonoBehaviour
{
    #region PublicVariables
	#endregion

	#region PrivateVariables
	#endregion

	#region PublicMethod
	public void SetText(string title, string description) {
		transform.Find("CardTitle").GetComponent<TMPro.TextMeshProUGUI>().text = title;
		transform.Find("CardMiddleTitle").GetComponent<TMPro.TextMeshProUGUI>().text = description;
	}
	#endregion
    
	#region PrivateMethod
	#endregion
}

}