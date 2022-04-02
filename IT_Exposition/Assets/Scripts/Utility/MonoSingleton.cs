using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
	private static T _instance = null;
	private static bool canCreate = true;

	public int InstanceID { get; private set; }

	public new Transform transform { get; private set; }
	public new GameObject gameObject { get; private set; }

	public static bool hasInstance => null != _instance && canCreate;

	public static T Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = GameObject.FindObjectOfType(typeof(T)) as T;

				if (_instance == null && canCreate)
				{
					_instance = GetSingletonObject<T>();

					if (_instance is null)
					{
						_instance = new GameObject(typeof(T).ToString(), typeof(T)).GetComponent<T>();
					}
				}
				else
					_instance.Init();
			}

			return _instance;
		}
	}

	protected virtual void Awake()
	{
		if (_instance is null)
		{
			_instance = this as T;
			SetDefault();
			Init();
		}
		else
		{
			if (_instance != this)
				Destroy(base.gameObject);
		}
	}

	protected virtual void Init()
	{

	}

	protected void SetDefault()
	{
		transform = base.transform;
		gameObject = base.gameObject;
		InstanceID = GetInstanceID();

		if (Application.isPlaying)
			DontDestroyOnLoad(base.gameObject);
	}

	public static SINGLETON GetSingletonObject<SINGLETON>() where SINGLETON : MonoBehaviour
	{
		string loadPath = $"Singleton/{typeof(SINGLETON).Name}";

		var loadObj = Resources.Load(loadPath);
		if (loadObj is null)
		{
			return null;
		}

		Object createObj = Object.Instantiate(loadObj);
		GameObject go = createObj as GameObject;

		if (go is null)
		{
			Object.DestroyImmediate(createObj);
			return null;
		}

		go.name = go.name.Replace("(Clone)", string.Empty);
		return go.GetComponent<SINGLETON>();
	}

	protected virtual void OnApplicationQuit()
	{
		canCreate = false;
	}

	private void OnDestroy()
	{
		if (_instance != this)
			return;

		OnDestroyImpl();
		_instance = null;
	}

	protected virtual void OnDestroyImpl()
	{

	}

}
