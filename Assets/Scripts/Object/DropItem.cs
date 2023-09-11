using System.Collections;
using System.Collections.Generic;
using TH.Core;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Data.SqlTypes;

public class DropItem : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private string itemID;
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
	private float _pickDelayTimer = 0f;
	private static float _pickDelay = 0.5f;
	private bool _canPick = false;
	#endregion

	#region PublicMethod
	public void PickedBy(PlayerItemGetter getter, float speed)
	{
		if (_canPick == false)
			return;
		transform.DOKill();
		if(getter.IsItemAvailableToInventory(WorldManager.Instance.GetItemData(itemID), _quantity))
		{
			if (_isPicked == true)
				return;
			_getter = getter;
			_speed = speed;
			_isPicked = true;
		}
		else if(_isPicked == true)
		{
			Vector3 direction = (transform.position - _player.transform.position).normalized;
			transform.DOJump(transform.position + direction, 0.3f, 1, 0.3f);
			Initialize();
		}
	}
	public void Initialize()
	{
		_player = GameManager.Instance.GetPlayer();
		_isPicked = false;
		_speed = 0f;
		_getter = null;
		_shineTimer = 0f;
		_canPick = false;
		_pickDelayTimer = 0f;
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
		transform.DOJump((Vector2)transform.position + new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)), 0.3f, Random.Range(1, 3), 0.3f);
	}
	private void Update()
	{
		CheckPickDelay();
		UpdateQuantity();
		ShineSelf();
		PickedByPlayer();
		if (_isPicked == true)
		{
			RunToPlayer();
		}
	}
	private void CheckPickDelay()
	{
		if(_canPick == false)
		{
			_pickDelayTimer += Time.deltaTime;
			if( _pickDelayTimer > _pickDelay)
			{
				_canPick = true;
			}
		}
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
		int rest = _quantity - _getter.AddItem(WorldManager.Instance.GetItemData(itemID), _quantity);
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
