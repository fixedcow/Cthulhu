using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable : ITargetable
{
	public void Hit(int damage);
}