using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class UIGauge : MonoBehaviour
{
    #region PublicVariables
    #endregion


    #region PrivateVariables
    private Image image;
	[SerializeField] private TextMeshProUGUI _text;

    protected int currentValue;
    protected int minValue = 0;
    protected int maxValue;
    #endregion

    #region PublicMethod
    public void UpdateGauge(int currentValue)
    {
        this.currentValue = currentValue;
        float modifiedValue = 1 - ((float)currentValue / maxValue);
        image.material.SetFloat("_ClipUvRight", modifiedValue);
		_text.text = currentValue.ToString();
    }
	public void SetMaxValue(int value)
	{
		maxValue = value;
		float modifiedValue = 1 - ((float)currentValue / maxValue);
		image.material.SetFloat("_ClipUvRight", modifiedValue);
	}
    #endregion
    #region PrivateMethod
    private void Awake()
    {
		transform.Find("Text").TryGetComponent(out _text);
        transform.Find("Fill").TryGetComponent(out image);
    }
    #endregion
}
