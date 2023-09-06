using System.Collections;
using System.Collections.Generic;
using TH.Core;
using UnityEngine;
using DG.Tweening;

public class DroppedItem : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private ItemData _data;
	private bool _isPicked;
	private PlayerItemGetter _getter;
	private float _speed;
	#endregion

	#region PublicMethod
	public void PickedBy(PlayerItemGetter getter, float speed)
	{
		if (_isPicked == true)
			return;
		_getter = getter;
		_speed = speed;
		_isPicked = true;
	}

	#endregion

	#region PrivateMethod
	private void OnEnable()
	{
		_isPicked = false;
		_getter = null;
		_speed = 0f;
	}
	private void Update()
	{
		if (_isPicked == true)
		{
			RunToPlayer();
		}
	}
	private void RunToPlayer()
	{
		transform.position = Vector2.Lerp(transform.position, _getter.transform.position, _speed * Time.deltaTime);
		if (Vector2.Distance(transform.position, _getter.transform.position) < 0.5f)
		{
			AddToInventory();
		}
	}
	private void AddToInventory()
	{
		// 인벤토리에 Add.
		Destroy(gameObject);
	}
	#endregion
}
