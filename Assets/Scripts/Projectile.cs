using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

	BoxCollider2D _collider;

	public float damage = 1f;
	public float moveSpeed = 0f;
	public bool isBlast = false;
	public float duration = 1f;

	void Start()
	{
		_collider = GetComponent<BoxCollider2D>();
		if (isBlast == true)
			_collider.enabled = false;
	}

	void Update()
	{
		GetInput();
		Move();
	}

	void GetInput()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0) && isBlast)
		{
			_collider.enabled = true;
			StartCoroutine(BlastShot(duration));
		}
	}

	void Move()
	{
		transform.position += transform.up * moveSpeed * Time.deltaTime;
	}

	IEnumerator BlastShot(float ms)
	{
		yield return new WaitForSeconds(ms);
		_collider.enabled = false;
	}
}
