using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friendly : MonoBehaviour
{

	Rigidbody2D _rb;
	GameObject _player;
	Vector2 _direction = new Vector3(0, 0, 0);
	Vector3 _target;
	bool _rescued = false;

	public float health = 3f;
	public float moveSpeed = 1f;

	void Start()
	{
		_player = GameObject.Find("Player");
		_rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (_rescued)
			FollowPlayer();
		else
			DisplayHelp();

		if (_rescued)
			Shoot();
	}

	void Shoot()
	{
		// Shoot at enemies
	}

	void DisplayHelp()
	{
		// Shout for help
	}

	void FollowPlayer()
	{
		if (_player != null)
		{
			if (_player.activeInHierarchy && Vector2.Distance(transform.position, (_player.transform.position + _target)) >= 0.05f)
			{
				//_target = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), gameObject.transform.position.z);
				_direction = (_player.transform.position - transform.position + _target).normalized;
				_rb.MovePosition(_rb.position + _direction.normalized * moveSpeed * Time.deltaTime);
			}
		}
	}

	void TakeDamage()
	{
		health -= 1;
		if (health <= 0)
			Die();
	}

	void Die()
	{
		Debug.Log("Friendly died");
	}

	void Rescue()
	{
		_rescued = true;
		_target = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), gameObject.transform.position.z);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (_rescued && other.gameObject.tag == "Enemy" && _rescued)
			TakeDamage();

		if (other.gameObject.tag == "Grabber")
			Rescue();
	}
}
