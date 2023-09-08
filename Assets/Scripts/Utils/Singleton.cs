using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T instance;
	public static T Instance
	{
		get
		{
			if (instance == null)
			{
				SetupInstance();
			}
			return instance;
		}
	}
	private void Awake()
	{
		if (instance == null)
		{
			instance = this as T;
			DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			/*Destroy(gameObject);*/
			return;
		}
		Init();
	}
	private static void SetupInstance()
	{
		instance = FindObjectOfType<T>();
/*		if (instance == null)
		{
			GameObject gameObj = new GameObject();
			gameObj.name = typeof(T).Name;
			instance = gameObj.AddComponent<T>();
			DontDestroyOnLoad(gameObj);
		}*/
	}
	protected virtual void Init()
	{
	}
}