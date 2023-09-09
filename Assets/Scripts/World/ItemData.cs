using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TH.Core {

[Serializable]
public class ItemData
{
    #region PublicVariables
	[InfoBox("아이템의 이름, 설명, 스프라이트, 스택 가능 여부, 최대 스택 가능 개수를 설정합니다.")]
	[SerializeField] public string ItemID;
	[SerializeField] public string ItemName;
	[MultiLineProperty]
	[SerializeField] public string ItemDescription;
	[MultiLineProperty]
	[SerializeField] public string ItemFlavorText;
	[SerializeField] public Sprite ItemSprite;

	[SerializeField] public bool IsStackable;
	[ShowIf("IsStackable", true)]
	[SerializeField] public int MaxStackableNumber;
	#endregion

	#region PrivateVariables
	#endregion

	#region PublicMethod
	public ItemData(ItemData data)
	{
		ItemID = data.ItemID;
		ItemName = data.ItemName;
		ItemDescription = data.ItemDescription;
		ItemFlavorText = data.ItemFlavorText;
		ItemSprite = data.ItemSprite;
		IsStackable = data.IsStackable;
		MaxStackableNumber = data.MaxStackableNumber;
	}
	#endregion
    
	#region PrivateMethod
	#endregion
}

}