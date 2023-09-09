using System.Collections;
using System.Collections.Generic;
using TH.Core;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class DroppedItem : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private ItemData _data;
	[SerializeField] private int _quantity = 1;
	private TextMeshPro _quantityText;
	private SpriteRenderer _sr;
	private float _shineTimer;
	[SerializeField] private float _shineDuration;
	[SerializeField] private float _shineCycleDuration;
	private bool _isPicked;

	private Player _player;
	private PlayerItemGetter _getter;
	private float _speed;
	#endregion

	#region PublicMethod
	public void PickedBy(PlayerItemGetter getter, float speed)
	{
		if(getter.IsItemAvailableToInventory(_data, _quantity))
		{
			if (_isPicked == true)
				return;
			_getter = getter;
			_speed = speed;
			_isPicked = true;
		}
		else if(_isPicked == true)
		{
			Initialize();
		}
	}
	#endregion

	#region PrivateMethod
	private void Awake()
	{
		TryGetComponent(out _sr);
		transform.Find("Quantity").TryGetComponent(out _quantityText);
	}
	private void OnEnable()
	{
		Initialize();
	}
	private void Update()
	{
		UpdateQuantity();
		ShineSelf();
		PickedByPlayer();
		if (_isPicked == true)
		{
			RunToPlayer();
		}
	}
	private void Initialize()
	{
		_player = GameManager.Instance.GetPlayer();
		_isPicked = false;
		_speed = 0f;
		_getter = null;
		_shineTimer = 0f;
	}
	private void RunToPlayer()
	{
		transform.position = Vector2.Lerp(transform.position, _player.transform.position, _speed * Time.deltaTime);

	}
	private void PickedByPlayer()
	{
		if (_isPicked == true && Vector2.Distance(transform.position, _player.transform.position) < 0.5f)
		{
			AddToInventory();
		}
	}
	private void AddToInventory()
	{
		int rest = _quantity - _getter.AddItem(_data, _quantity);
		if (rest > 0)
		{
			Vector3 direction = (transform.position - _player.transform.position).normalized;
			_quantity = rest;
			transform.DOJump(transform.position + direction, 0.3f, 1, 0.3f);
			Initialize();
		}
		else
		{
			Destroy(gameObject);
		}
	}
	private void ShineSelf()
	{
		_shineTimer += Time.deltaTime * _shineDuration;
		_sr.material.SetFloat("_ShineLocation", Mathf.Clamp01(_shineTimer));
		if( _shineTimer > _shineCycleDuration * _shineDuration)
		{
			_shineTimer = 0f;
		}
	}
	private void UpdateQuantity()
	{
		if( _quantity > 1 )
		{
			_quantityText.text = "x" + _quantity;
		}
		else
		{
			_quantityText.text = "";
		}
	}
	#endregion
}
