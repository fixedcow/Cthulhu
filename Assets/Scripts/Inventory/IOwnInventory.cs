using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core {

public interface IOwnInventory
{
    #region PublicVariables
	#endregion

	#region PrivateVariables
	#endregion

	#region PublicMethod
	/// <summary>
	/// 아이템을 추가합니다. 매 프레임 호출됩니다.
	/// </summary>
	/// <param name="AddItemFunc">int AddItemFunc(ItemData targetItem, int quantitiy)를 호출하여 아이템을 추가할 수 있습니다. 추가된 아이템 수를 반환합니다.</param>
	public void AddItem(Func<ItemData, int, int> AddItemFunc);

	/// <summary>
	/// 아이템이 사용되었을 때 호출됩니다.
	/// </summary>
	/// <param name="item">사용된 아이템</param>
	/// <param name="quantity">사용된 아이템 수량</param>
	public void OnUseItem(ItemData item, int quantity);

	public void OnSelectItem(int idx);
	#endregion
    
	#region PrivateMethod
	#endregion
}

}