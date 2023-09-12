using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UILevel : MonoBehaviour
{
	#region PublicVariables
	public TextMeshProUGUI leftPointText;
	public TextMeshProUGUI levelText;
	public Image img;
	#endregion

	#region PrivateVariables
	#endregion

	#region PublicMethod
	public void Set(int level, int point, int needPoint)
	{
		levelText.text = level.ToString();
		leftPointText.text = "æ’¿∏∑Œ " + (needPoint - point).ToString();
		img.material.SetFloat("_ClipUvRight",  (needPoint - point) / (float) needPoint);
	}
	#endregion

	#region PrivateMethod
	private void Update()
	{
		Set(GameManager.Instance.Level, GameManager.Instance.Point, GameManager.Instance.NeedPoint);
	}
	#endregion
}
