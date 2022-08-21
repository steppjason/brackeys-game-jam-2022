using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

	Rigidbody2D _rb;
	Vector3 _target = new Vector3(0, 0, 0);
	Vector2 _direction = new Vector3(0, 0, 0);


	public float moveSpeed = 1f;
	public float health = 1f;

	GameObject _player;

	void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
		_player = GameObject.Find("Player");
	}

	void Update()
	{
		_direction.Normalize();
		Move();
	}



	void Move()
	{
		if (_player != null)
		{
			if (_player.activeInHierarchy)
			{
				_target = _player.transform.position;
				_direction = (_target - transform.position).normalized;
				_rb.MovePosition(_rb.position + _direction.normalized * moveSpeed * Time.deltaTime);
			}
		}
	}


	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.tag == "Projectile")
			TakeDamage(other.gameObject.GetComponent<Projectile>().damage);
	}

	public void TakeDamage(float damage)
	{
		health -= damage;
		if (health <= 0)
			Die();
	}

	void Die()
	{
		gameObject.SetActive(false);
	}

}
