using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	
	public static GameManager Instance { get; private set; }



	void Start()
	{
		GetGameInstance();
		GetManagers();
	}

	void Update()
	{

	}

	void GetGameInstance()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	void GetManagers()
	{

	}
}
