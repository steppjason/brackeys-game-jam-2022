using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{

	public static GameManager Instance { get; private set; }

	public Camera cam;
	public float threshold = 100.0f;

	public GameObject player;

	void Start()
	{
		GetGameInstance();
		GetManagers();
	}

	void LateUpdate()
	{
		ResetOrigin();
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

	void ResetOrigin()
	{
		//Vector3 camPos = cam.transform.position;
		Vector3 camPos = player.transform.position;
		camPos.z = 0f;

		if (Math.Abs(camPos.x) > threshold){
			foreach (GameObject g in SceneManager.GetSceneAt(0).GetRootGameObjects()){
				g.transform.position -= new Vector3((float)Math.Round(camPos.x), 0, 0);
			}
		}

		if (Math.Abs(camPos.y) > threshold){
			foreach (GameObject g in SceneManager.GetSceneAt(0).GetRootGameObjects()){
				g.transform.position -= new Vector3(0, (float)Math.Round(camPos.y), 0);
			}
		}

	}
}
