using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friendly : MonoBehaviour
{

	Rigidbody2D _rb;
	Animator _anim;
	GameObject _player;
	Vector2 _direction = new Vector3(0, 0, 0);
	Vector3 _target;
	bool _rescued = false;

	public float range = 2.5f;
	public float health = 3f;
	public float moveSpeed = 1f;

	public bool isMoving = false;

	void Start()
	{
		_player = GameObject.Find("Player");
		_rb = GetComponent<Rigidbody2D>();
		_anim = GetComponent<Animator>();
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
				if (!isMoving)
					GetRandomPoint();

				isMoving = true;
				_anim.SetBool("Following", true);
				_direction = (_player.transform.position - transform.position + _target).normalized;
				_rb.position = _rb.position + _direction.normalized * moveSpeed * Time.deltaTime;
			}
			else
			{
				isMoving = false;
				_anim.SetBool("Following", false);
			}
			// else
			// {
			// 	
			// }
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
		//Debug.Log("Friendly died");
	}

	void Rescue()
	{
		_rescued = true;
		GetRandomPoint();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (_rescued && other.gameObject.tag == "Enemy" && _rescued)
			TakeDamage();

		if (other.gameObject.tag == "Grabber")
			Rescue();
	}

	void GetRandomPoint()
	{
		Vector2 direction = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
		direction.Normalize();
		_target = new Vector3(direction.x * range, direction.y * range, gameObject.transform.position.z);
	}
}
