using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable : ITargetable
{
	public virtual void Hit(int damage)
	{
	}
}
