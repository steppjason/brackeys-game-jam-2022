using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplattPool : MonoBehaviour
{
	public GameObject blood;
	public int numberOfBlood = 100;

	GameObject[] _bloods;
	int _nextBlood = 0;

	void Awake()
	{
		InstantiateBullets();
	}

	void InstantiateBullets()
	{
		_bloods = new GameObject[numberOfBlood];
		for (int i = 0; i < numberOfBlood; i++)
		{
			_bloods[i] = Instantiate(blood, new Vector3(0, 0, 0), Quaternion.identity);
			_bloods[i].transform.parent = gameObject.transform;
			_bloods[i].gameObject.SetActive(false);
		}
	}

	public void SetBloodSplatt(Vector3 position, Quaternion rotation)
	{
		GetAvailable();
		GameObject newBlood = _bloods[_nextBlood];
		newBlood.transform.position = position;
		newBlood.transform.rotation = rotation;
		newBlood.gameObject.SetActive(true);
	}

	void GetAvailable()
	{
		for (int i = 0; i < _bloods.Length; i++)
		{
			if (!_bloods[i].gameObject.activeInHierarchy)
			{
				_nextBlood = i;
				return;
			}
		}
	}


}
