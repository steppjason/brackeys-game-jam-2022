using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyPool : MonoBehaviour
{
	public Friendly[] _friendlies;
	public GameObject player;

	void Start()
	{
		StartCoroutine(coSpawnFriendlies());
	}	

	IEnumerator coSpawnFriendlies()
	{
		while (true)
		{
			GetAvailable();
			yield return new WaitForSeconds(10);
		}
	}

	public void SpawnFriendly(Vector3 position, Quaternion rotation, Friendly friendly)
	{
		friendly.transform.position = position;
		friendly.transform.rotation = rotation;
		friendly.ResetHealth();
		friendly.ResetRescue();
		friendly.gameObject.SetActive(true);
	}

	void GetAvailable()
	{
		int randomIndex = Random.Range(0, _friendlies.Length);
		
		if (!_friendlies[randomIndex].gameObject.activeInHierarchy)
			SpawnFriendly(GetRandomPoint(20) + player.transform.position , Quaternion.identity, _friendlies[randomIndex]);
	}

	Vector3 GetRandomPoint(float range)
	{
		Vector3 randomPoint = new Vector3(UnityEngine.Random.Range(-2f, 2f), UnityEngine.Random.Range(-2f, 2f), 0);
		randomPoint.Normalize();
		return randomPoint * range;
	}

}
