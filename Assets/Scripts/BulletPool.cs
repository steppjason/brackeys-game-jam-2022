using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
	public Projectile bullet;
	public int numberOfProjectiles = 100;

	Projectile[] _projectiles;
	int _nextProjectile = 0;

	void Awake() {
		InstantiateBullets();
	}

	void InstantiateBullets()
	{
		_projectiles = new Projectile[numberOfProjectiles];
		for (int i = 0; i < numberOfProjectiles; i++)
		{
			_projectiles[i] = Instantiate(bullet, new Vector3(0, 0, 0), Quaternion.identity);
			_projectiles[i].transform.parent = gameObject.transform;
			_projectiles[i].gameObject.SetActive(false);
		}
	}

	public void SetBulletActive(Vector3 position, Quaternion rotation)
	{
		GetAvailable();
		Projectile newProjectile = _projectiles[_nextProjectile];
		//newProjectile.transform.position = position;
		//newProjectile.SetDirection(direction);
		newProjectile.transform.position = position;
		newProjectile.transform.rotation = rotation;

		newProjectile.gameObject.SetActive(true);
	}

	void GetAvailable()
	{
		for (int i = 0; i < _projectiles.Length; i++)
		{
			if (!_projectiles[i].gameObject.activeInHierarchy)
			{
				_nextProjectile = i;
				return;
			}
		}
	}
	
}
