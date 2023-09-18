using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using TH.Core;

public class GameManager : Singleton<GameManager>
{
	#region PublicVariables
	public int Gold => _gold;
	public int Level => _tier;
	public int Point => _point;
	public int NeedPoint => TierPointCondition[_tier];

	public readonly static int[] TierPointCondition = new int[]
	{
		100, 200, 500, 1000, 1200, 1500, 2000, 2500, 5000
	};
	#endregion

	#region PrivateVariables
	[SerializeField] private Player _player;
	[ReadOnly][SerializeField] private int _gold = 0;
	[ReadOnly][SerializeField] private int _point = 0;
	[ReadOnly, SerializeField] private int _tier = 0;

	[SerializeField] private OfferUITest _offerui;
	[SerializeField] private GameObject _gameoverui;
    [SerializeField] private GameObject _DayUI;

    [SerializeField] private DayTimeController dayTimeController;
    #endregion

    #region PublicMethod
    public Player GetPlayer() => _player;
	public void AddGold(int amount)
	{
		_gold += amount;
		UIManager.Instance.Gold.UpdateAmount(_gold);
	}
	public void AddPoint(int amount)
	{
		_point += amount;
	}
	public void GameOver()
	{
		_gameoverui.SetActive(true);
		//_DayUI.SetActive(false);

        if (dayTimeController != null)
        {
            dayTimeController.PauseTime();
        }

    }
	#endregion

	#region PrivateMethod
	private void Start()
	{
		UIManager.Instance.Gold.UpdateAmount(_gold);
        StartCoroutine(nameof(MinusSanity));
    }

	private void Update()
	{
		if (TierPointCondition[_tier] < _point)
		{
			_point = 0;
			_tier++;
			_player.HealSanity(40);
			_offerui.gameObject.SetActive(true);
		}

	}

	public IEnumerator MinusSanity()
	{
		while (true)
		{
			yield return new WaitForSeconds(3f); //3초에 
			_player.MinusSanity(1); //1씩 정신력 닳음
		}
	}
}
    #endregion