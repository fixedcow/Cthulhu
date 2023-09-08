using System;	
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core {

public class UITrashSlot : UIInventorySlot
{
    #region PublicVariables
	public override void SetSlot(bool isNull, int idx, InventoryItem item) {
		_slotIdx = idx;
		return;
	}
	#endregion

	#region PrivateVariables
	#endregion

	#region PublicMethod
	#endregion
    
	#region PrivateMethod
	#endregion
}

}