using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TH.Core {

public class DraggingItem : MonoBehaviour
{
    #region PublicVariables
	#endregion

	#region PrivateVariables
	private Image _draggingItemImage;
	#endregion

	#region PublicMethod
	public void SetDraggingItem(Sprite sprite) {
		_draggingItemImage.sprite = sprite;
	}
	#endregion
    
	#region PrivateMethod
	private void Awake() {
		_draggingItemImage = transform.Find("Image").GetComponent<Image>();
	}
	#endregion
}

}