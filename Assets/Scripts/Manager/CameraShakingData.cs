using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="TH/Camera Shaking Data", fileName ="New Camera Shaking Data")]
public class CameraShakingData : ScriptableObject
{
	public float duration;
	public float strength;
	public int vibrato;
	public float randomness;
}
