using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[SerializeField] private Player _player;
	#endregion

	#region PublicMethod
	public Player GetPlayer() => _player;
	#endregion

	#region PrivateMethod
	#endregion
}
