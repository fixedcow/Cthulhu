using Pathfinding;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animal))]
public class AnimalMove : MonoBehaviour
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private AIPath _ai;
	private AIDestinationSetter _destination;
	#endregion

	#region PublicMethod
	public void SetSpeed(int speed) => _ai.maxSpeed = speed;
	[Button]
	public void FleeFromPlayer()
	{
		_destination.target = null;
		FleePath path = FleePath.Construct(transform.position, GameManager.Instance.GetPlayer().transform.position, 10000);
		path.aimStrength = 1;
		path.spread = 4000;

		_ai.SetPath(path);
	}
	[Button]
	public void IdleRandomMove()
	{
		_destination.target = null;
		_ai.SetPath(RandomPath.Construct(transform.position, 5));
	}
	[Button]
	public void ChasePlayer()
	{
		_destination.target = GameManager.Instance.GetPlayer().transform;
	}
	#endregion

	#region PrivateMethod
	private void Awake()
	{
		TryGetComponent(out _ai);
		TryGetComponent(out _destination);
	}
	#endregion
}
