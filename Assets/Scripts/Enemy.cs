using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

	Rigidbody2D _rb;
	Vector3 _target = new Vector3(0, 0, 0);
	Vector2 _direction = new Vector3(0, 0, 0);

	public float moveSpeed = 1f;

	public GameObject player;

	void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		_direction.Normalize();
		Move();
	}

	void Move()
	{
		if (player != null)
		{
			if (player.activeInHierarchy)
			{
				_target = player.transform.position;
				_direction = (_target - transform.position).normalized;
				_rb.MovePosition(_rb.position + _direction.normalized * moveSpeed * Time.deltaTime);
			}
		}

	}
}
