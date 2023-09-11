using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGold : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private TextMeshProUGUI _text;
	#endregion

	#region PublicMethod
	public void UpdateAmount(int amount)
	{
		_text.text = amount.ToString();
	}
	#endregion

	#region PrivateMethod
	private void Awake()
	{
		transform.Find("Text").TryGetComponent(out _text);
	}
	#endregion
}
