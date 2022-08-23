using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	Vector2 _direction;
	Rigidbody2D _rb;
	float _health = 3f;

	public GameObject cam;
	public GameObject bounds;
	public float moveSpeed = 1f;

	public GameObject pistol;
	public GameObject shotgun;
	public GameObject machineGun;

	void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		UpdateBoundsPosition();
		Move();
		//UpdateCameraPosition();
	}

	private void FixedUpdate() {
		//_rb.MovePosition(_rb.position + _direction * moveSpeed * Time.deltaTime);
	}

	void Move()
	{
		_direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		_direction.Normalize();
		_rb.position = _rb.position + _direction * moveSpeed * Time.deltaTime;
	}

	void UpdateCameraPosition()
	{
		cam.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, cam.transform.position.z);
	}

	void UpdateBoundsPosition()
	{
		bounds.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, bounds.transform.position.z);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Enemy")
			TakeDamage();
	}

	void TakeDamage()
	{
		_health -= 1;
		if (_health <= 0)
			Die();
	}

	void Die()
	{
		//Debug.Log("You are dead");
	}
}
