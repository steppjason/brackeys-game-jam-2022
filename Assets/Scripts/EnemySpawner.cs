using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour
{
	public Enemy enemy;
	public Player player;
	public int numberOfEnemies = 500;
	public float rate = 0.01f;
	public float range = 5f;
	public float waitTime;

	int _numberOfEnemiesToSpawn = 1;
	float _time;
	Shader defaultShader;

	Enemy[] _enemies;
	int _nextEnemy = 0;

	void Awake()
	{
		defaultShader = Shader.Find("Sprites/Default");
		InstantiateEnemies();
	}

	void Start()
	{
		StartCoroutine(coSpawnEnemies());
	}

	void Update()
	{
		_time += Time.deltaTime;
		_numberOfEnemiesToSpawn = GetNumberOfEnemies(rate, _time);
	}

	IEnumerator coSpawnEnemies()
	{
		while (true)
		{
			SpawnEnemies(_numberOfEnemiesToSpawn);
			yield return new WaitForSeconds(waitTime);
		}
	}


	int GetNumberOfEnemies(float rate, float time)
	{
		return (int)Math.Floor(Math.Pow((1 + rate), time)) * 1;
	}


	void InstantiateEnemies()
	{
		_enemies = new Enemy[numberOfEnemies];
		for (int i = 0; i < numberOfEnemies; i++)
		{
			_enemies[i] = Instantiate(enemy, new Vector3(0, 0, 0), Quaternion.identity);
			_enemies[i].transform.parent = gameObject.transform;
			_enemies[i].gameObject.SetActive(false);
		}
	}


	void SpawnEnemies(int numberOfEnemiesToSpawn)
	{
		for (int i = 0; i < numberOfEnemiesToSpawn; i++)
		{
			SetActiveEnemy(GetRandomPoint(range) + player.transform.position, Quaternion.identity);
		}
	}


	Vector3 GetRandomPoint(float range)
	{
		Vector3 randomPoint = new Vector3(UnityEngine.Random.Range(-2f, 2f), UnityEngine.Random.Range(-2f, 2f), 0);
		randomPoint.Normalize();

		return randomPoint * range;
	}


	void SetActiveEnemy(Vector3 position, Quaternion rotation)
	{
		GetAvailable();
		if (_nextEnemy == -1) return;
		Enemy newEnemy = _enemies[_nextEnemy];
		newEnemy.sprite.material.shader = defaultShader;
		newEnemy.transform.position = position;
		newEnemy.transform.rotation = rotation;
		newEnemy.gameObject.SetActive(true);

	}


	void GetAvailable()
	{
		if(_nextEnemy == -1) _nextEnemy = 0;
		for (int i = _nextEnemy; i < _enemies.Length; i++)
		{
			if (!_enemies[i].gameObject.activeInHierarchy)
			{
				_nextEnemy = i;
				return;
			}
		}

		_nextEnemy = -1;
		return;

	}

}
