using System.Collections;
using System.Collections.Generic;
using TH.Core;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/Animal Data", fileName = "New Animal Data")]
public class AnimalData : ScriptableObject
{
	public string race;
	public int hpMax;
	public int speedIdle;
	public int speedAttack;
	public int damage;
	public int attackCooldown;
	public float recognitionRange;
	public float attackRange;
}
