using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
	public UIGold Gold => _gold;
	public UIHealth Health => _health;
	public UISanity Sanity => _sanity;

	[SerializeField] private UIGold _gold;
	[SerializeField] private UIHealth _health;
	[SerializeField] private UISanity _sanity;
}
