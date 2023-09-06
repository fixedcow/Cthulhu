using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core {

/// <summary>
/// 아이템 슬롯에 해당하는 아이템 클래스
/// </summary>
public class InventoryItem : MonoBehaviour
{
    #region PublicVariables
	public ItemData TargetItem => _targetItem;
	public int StackedNumber => _stackedNumber;
	#endregion

	#region PrivateVariables
	private bool _isNull;
	private ItemData _targetItem;
	private int _stackedNumber;
	#endregion

	#region PublicMethod
	#endregion
    
	#region PrivateMethod
	private void Awake() {

	}
	#endregion
}

}