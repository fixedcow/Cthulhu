using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TH.Core {

[CreateAssetMenu(fileName = "ItemData", menuName = "TH/ItemData", order = 0)]
public class ItemData : ScriptableObject
{
    #region PublicVariables
	[InfoBox("아이템의 이름, 설명, 스프라이트, 스택 가능 여부, 최대 스택 가능 개수를 설정합니다.")]
	[SerializeField] public string ItemID;
	[SerializeField] public string ItemName;
	[MultiLineProperty]
	[SerializeField] public string ItemDescription;
	[SerializeField] public Sprite itemSprite;

	[SerializeField] public bool isStackable;
	[ShowIf("isStackable", true)]
	[SerializeField] public int maxStackableNumber;
	#endregion

	#region PrivateVariables
	#endregion

	#region PublicMethod
	#endregion
    
	#region PrivateMethod
	#endregion
}

}