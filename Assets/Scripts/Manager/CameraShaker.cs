using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class CameraShaker : MonoBehaviour
{
	#region PublicVariables
	/// <summary>
	/// datas의 데이터 순서와 해당 enum타입 순서를 맞춰줘야 함.
	/// </summary>
	public enum EShakingType
	{
		crash = 0,

	}
	#endregion

	#region PrivateVariables
	/// <summary>
	/// EShakingData의 데이터 순서와 해당 list의 순서를 맞춰줘야 함.
	/// </summary>
	[SerializeField] private List<CameraShakingData> datas = new List<CameraShakingData>();
	#endregion

	#region PublicMethod
	[Button]
	public void Shake(EShakingType type)
	{
		CameraShakingData data = datas[(int)type];
		transform.DOShakePosition(data.duration, data.strength, data.vibrato, data.randomness);
	}
	#endregion

	#region PrivateMethod
	#endregion
}
