using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class Ore : MonoBehaviour, IHittable
{
	//체력이 0이되면 여러개 나옴
	//광석의 종류
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private OreData _data;
	private DropItemSpawner _drop;
	private SpriteRenderer _sr;
	[SerializeField] private Vector2 hittablePointA = new Vector2(-0.5f, 0.5f);
	[SerializeField] private Vector2 hittablePointB = new Vector2(0.5f, -0.5f);
	private int _hp;

	public Vector2 GetHittableUIPositionA() => (Vector2)transform.position + hittablePointA;
	public Vector2 GetHittableUIPositionB() => (Vector2)transform.position + hittablePointB;
	public Vector2 GetPosition() => transform.position;
	#endregion

	#region PublicMethod
	public void Hit(int damage)
	{
		_sr.material.EnableKeyword("HITEFFECT_ON");
		Invoke(nameof(DisableHitEffect), 0.13f);
		_sr.transform.DOShakePosition(0.13f, 0.4f);
		_hp = Mathf.Clamp(_hp - damage, 0, _data.hpMax);
		if (_hp == 0)
		{
			Die();
		}
	}
	public void Die()
	{
		_drop.Drop();
		Destroy(gameObject);
	}
	#endregion

	#region PrivateMethod
	private void Awake()
	{
		TryGetComponent(out _sr);
		TryGetComponent(out _drop);
	}
	private void DestroyOre()
    {
        Destroy(gameObject);
    }
	private void DisableHitEffect()
	{
		_sr.material.DisableKeyword("HITEFFECT_ON");
	}
    #endregion
}
