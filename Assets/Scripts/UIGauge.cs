using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UIGauge : MonoBehaviour
{
    #region PublicVariables
    #endregion


    #region PrivateVariables
    private Image image;

    protected int currentValue;
    protected int minValue = 0;
    protected int maxValue;
    #endregion

    #region PublicMethod
    public void UpdateGauge()
    {
        float modifiedValue = 1 - ((float)currentValue / maxValue);
        image.material.SetFloat("_ClipUvRight", modifiedValue);
    }
    #endregion
    #region PrivateMethod
    private void Awake()
    {
        TryGetComponent(out image);
    }
    #endregion
}
