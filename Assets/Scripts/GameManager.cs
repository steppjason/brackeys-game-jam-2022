using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	public static GameManager Instance { get; private set; }

	public Camera cam;
	public float threshold = 100.0f;

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
		Vector3 camPos = cam.transform.position;
		camPos.y = 0f;


		if (camPos.magnitude > threshold)
		{
			for (int i = 0; i < SceneManager.sceneCount; i++)
			{
				foreach (GameObject g in SceneManager.GetSceneAt(0).GetRootGameObjects())
				{
					g.transform.position -= camPos;
				}
			}

			//Vector3 originDelta = Vector3.zero - cam.transform.position;

		}
	}
}
