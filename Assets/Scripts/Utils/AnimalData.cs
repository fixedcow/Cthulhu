using System;
using System.Collections;
using System.Collections.Generic;
using TH.Core;
using UnityEngine;

[Serializable]
public class AnimalData : ObjectData
{
	public int speedIdle;
	public int speedAttack;
	public int damage;
	public int attackCooldown;
	public float recognitionRange;
	public float attackRange;

	public AnimalData(AnimalData data) : base(data)
	{
		speedIdle = data.speedIdle;
		speedAttack = data.speedAttack;
		damage = data.damage;
		attackCooldown = data.attackCooldown;
		recognitionRange = data.recognitionRange;
		attackRange = data.attackRange;
	}
}
