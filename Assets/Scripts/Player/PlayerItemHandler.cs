using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TH.Core;
using UnityEngine;

public class PlayerItemHandler : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private SpriteRenderer _sr;
	private Sequence _handleSeq;
	private Inventory _inventory;
	private int _inventoryIndex;

	private bool _handleSomething;
	#endregion

	#region PublicMethod
	/// <summary>
	/// 설명입니다.
	/// </summary>
	/// <returns>이건 반환값 ㅋㅋ.</returns>
	public int GetCurrentInventoryIndex() => _inventoryIndex;
	public bool IsHandleSomething() => _handleSomething;
	[Button]
	public void Handle(int inventoryIndex)
	{
		_inventoryIndex = inventoryIndex;
		_sr.sprite = _inventory.GetItem(_inventoryIndex).TargetItem.ItemSprite;
		_handleSomething = true;
		_handleSeq.Restart();
	}
	[Button]
	public void PutIn()
	{
		_inventoryIndex = -1;
		_sr.sprite = null;
		_handleSomething = false;
	}
	#endregion

	#region PrivateMethod
	private void Awake()
	{
		TryGetComponent(out _sr);
		_handleSeq = DOTween.Sequence()
			.SetAutoKill(false)
			.Pause()
			.Append(transform.DOLocalMoveY(0.6f, 0.5f))
			.SetLoops(-1, LoopType.Yoyo);
	}
	#endregion
}
