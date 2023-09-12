using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraManager : Singleton<CameraManager>
{
	#region PublicVariables
	#endregion

	#region PrivateVariables
	private Camera _main;
	private CameraShaker _shaker;
	private SpriteRenderer _vignette;
	[SerializeField] private Player _player;

	[SerializeField] private float _speed;
	#endregion

	#region PublicMethod
	public void Shake(CameraShaker.EShakingType type)
	{
		_shaker.Shake(type);
	}
	[Button]
	public void SetVignetteAlpha(float percentage01)
	{
		_vignette.DOFade(percentage01, 1f);
	}
	#endregion

	#region PrivateMethod
	private void Awake()
	{
		_main = Camera.main;
		_main.TryGetComponent(out _shaker);
		_main.transform.Find("Vignette").TryGetComponent(out _vignette);
	}
	private void Update()
	{
		transform.position = Destination();
	}
	private Vector3 Destination()
	{
		Vector3 destination = Vector3.Lerp(transform.position, _player.transform.position, _speed * Time.deltaTime);
		destination.z = -10f;
		return destination;
	}
	#endregion
}
