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
	float _health;
	float _damageTimer;


	public BloodSplattPool bloodSplattPool;
	public GameObject weapon;
	public GameObject arrow;
	public float maxHealth = 100f;
	public float range = 2.5f;
	public float moveSpeed = 1f;
	public bool isMoving = false;
	public float damageRate = 0.1f;

	void Start()
	{
		_player = GameObject.Find("Player");
		_rb = GetComponent<Rigidbody2D>();
		_anim = GetComponent<Animator>();
		_health = maxHealth;
	}

	void Update()
	{
		if (_rescued)
			FollowPlayer();

		if (_player == null || !_player.activeInHierarchy)
			Die();

		CheckDistanceFromPlayer();
	}

	void CheckDistanceFromPlayer()
	{
		if (_player != null && _player.activeInHierarchy)
			if (Vector3.Distance(_player.transform.position, transform.position) > 35)
				gameObject.SetActive(false);
	}

	public void ResetHealth()
	{
		_health = maxHealth;
	}

	public void ResetRescue()
	{
		_rescued = false;
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
		}

	}

	void TakeDamage()
	{
		_health -= 1;
		if (_health <= 0)
			Die();
	}

	void Die()
	{
		bloodSplattPool.SetBloodSplatt(gameObject.transform.position, Quaternion.identity);
		gameObject.SetActive(false);
	}

	void Rescue()
	{
		_rescued = true;
		weapon.SetActive(true);
		GetRandomPoint();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Grabber")
			Rescue();
	}

	private void OnCollisionStay2D(Collision2D other)
	{
		if (_rescued && other.gameObject.tag == "Enemy")
		{
			_damageTimer += Time.deltaTime;
			if (_damageTimer > damageRate)
			{
				TakeDamage();
				_damageTimer = 0;
			}
		}
	}


	void GetRandomPoint()
	{
		Vector2 direction = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
		direction.Normalize();
		_target = new Vector3(direction.x * range, direction.y * range, gameObject.transform.position.z);
	}
}
