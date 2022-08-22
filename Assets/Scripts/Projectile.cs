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

	Coroutine _coBlastShot;

	void Start()
	{
		_collider = GetComponent<BoxCollider2D>();
		if (isBlast == true)
			_collider.enabled = false;
	}

	void Update()
	{
		Move();
	}

	void Move()
	{
		transform.position += transform.up * moveSpeed * Time.deltaTime;

		if (isBlast)
		{
			if (gameObject.activeInHierarchy)
			{
				_collider.enabled = true;
				_coBlastShot = StartCoroutine(BlastShot(duration));
			}
			else
			{
				if (_coBlastShot != null)
					StopCoroutine(_coBlastShot);
			}
		}

	}

	IEnumerator BlastShot(float ms)
	{
		yield return new WaitForSeconds(ms);
		_collider.enabled = false;
		gameObject.SetActive(false);
		yield break;
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (!isBlast && other.gameObject.tag == "Bounds" || other.gameObject.tag == "Enemy" )
			gameObject.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (!isBlast && other.gameObject.tag == "Enemy")
			gameObject.SetActive(false);
	}
}
