using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TileTradeText : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private TextMeshPro _text;
	private TextMeshPro _priceText;
	private SpriteRenderer _coin;

	private int _price;
	#endregion

	#region PublicMethod
	public void SetPrice(int price)
	{
		_price = price;
		_priceText.text = _price.ToString();
	}
	public void SetAlpha(float percentage01)
	{
		if (GameManager.Instance.Gold < _price)
			_priceText.DOColor(Color.red, 0f);
		else
			_priceText.DOColor(Color.white, 0f);

		_text.DOFade(percentage01, 0.4f);
		_priceText.DOFade(percentage01, 0.4f);
		_coin.DOFade(percentage01, 0.4f);
	}
	#endregion

	#region PrivateMethod
	private void Awake()
	{
		transform.Find("Text/TradeText").TryGetComponent(out _text);
		transform.Find("Text/PriceText").TryGetComponent(out _priceText);
		transform.Find("Text/Coin").TryGetComponent(out _coin);
		SetAlpha(0);
	}
	#endregion
}
