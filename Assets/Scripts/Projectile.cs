using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

	PolygonCollider2D _collider;

	public float damage = 1f;
	public float moveSpeed = 0f;
	public bool isBlast = false;
	public float duration = 1f;

	Coroutine _coBlastShot;

	void Start()
	{
		_collider = GetComponent<PolygonCollider2D>();
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
				Animator anim = GetComponent<Animator>();
				anim.SetBool("Shooting", true);
				_coBlastShot = StartCoroutine(BlastShotAnimation(2f));
			}
			else
			{
				_collider.enabled = false;
				if (_coBlastShot != null)
					StopCoroutine(_coBlastShot);
			}
		}

	}

	void EndBlastShot()
	{
		Animator anim = GetComponent<Animator>();
		anim.SetBool("Shooting", false);
		_collider.enabled = false;
	}

	void StartBlastShot()
	{
		_collider.enabled = true;
	}

	// IEnumerator BlastShotOn(float ms)
	// {
	// 	yield return new WaitForSeconds(ms);
	// 	_collider.enabled = true;
	// 	yield break;
	// }

	// IEnumerator BlastShotOff(float ms)
	// {
	// 	yield return new WaitForSeconds(ms);
	// 	_collider.enabled = false;
	// 	yield break;
	// }

	IEnumerator BlastShotAnimation(float ms)
	{
		yield return new WaitForSeconds(ms);
		gameObject.SetActive(false);
		yield break;
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (!isBlast && other.gameObject.tag == "Bounds" || other.gameObject.tag == "Enemy")
			gameObject.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (!isBlast && other.gameObject.tag == "Enemy")
			gameObject.SetActive(false);
	}
}
