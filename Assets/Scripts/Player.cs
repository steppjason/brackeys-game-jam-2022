using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	Vector2 _direction;
	Rigidbody2D _rb;

	public GameObject cam;
	public float moveSpeed = 1f;

	void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		UpdateCameraPosition();
	}

	void FixedUpdate()
	{
		Move();
	}

	void Move()
	{
		_direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		_direction.Normalize();
		_rb.MovePosition(_rb.position + _direction * moveSpeed * Time.deltaTime);
	}

	void UpdateCameraPosition()
	{
		cam.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, cam.transform.position.z);
	}

}
