using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/Animal Data", fileName = "New Animal Data")]
public class AnimalData : MonoBehaviour
{
	public enum EAttackType
	{
		harmless = 0,
		neutral = 1,
		threatening = 2
	}
	public string race;
	public EAttackType attackType;
	public int hpMax;
	public int speed;
	public int damage;
	public int attackSpeed;
}
