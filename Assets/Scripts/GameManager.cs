using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{

	public static GameManager Instance { get; private set; }

	public AudioManager audioManager;
	public CameraShake cameraShake;
	public Camera cam;
	public float threshold = 100.0f;
	public int kills;
	public TMP_Text score;
	public bool gameOver = false;

	public GameObject player;

	void Awake()
	{
		GetGameInstance();
		audioManager = GetComponent<AudioManager>();
		gameOver = false;
	}

	void Start()
	{
		GetManagers();
		kills = 0;
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

	void Update()
	{
		score.text = String.Format("{0:n0}", kills);

		if (gameOver)
			if (Input.GetKey(KeyCode.Escape))
			{
				SceneManager.LoadScene("TitleScreen");
				Destroy(gameObject);
			}
	}

	void GetManagers()
	{

	}

	void ResetOrigin()
	{
		Vector3 camPos = player.transform.position;
		camPos.z = 0f;

		if (Math.Abs(camPos.x) > threshold)
		{
			foreach (GameObject g in SceneManager.GetSceneAt(0).GetRootGameObjects())
			{
				g.transform.position -= new Vector3((float)Math.Round(camPos.x), 0, 0);
			}
		}

		if (Math.Abs(camPos.y) > threshold)
		{
			foreach (GameObject g in SceneManager.GetSceneAt(0).GetRootGameObjects())
			{
				g.transform.position -= new Vector3(0, (float)Math.Round(camPos.y), 0);
			}
		}

	}
}
