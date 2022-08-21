using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

	SpriteRenderer _sprite;
	Coroutine _coFire;
	Coroutine _coFireAI;

	float _elapsedTime = 0f;

	public GameObject barrel;
	public BulletPool _bulletPool;
	public float fireRate = 0.5f;

	public bool aiWeapon = false;
	public bool hasTarget = false;
	bool firstRun = true;
	Vector3 enemyTarget = new Vector3(-1, 0, 0);


	void Start()
	{
		_sprite = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		if (!aiWeapon)
			GetInput();

		CheckAITarget();
	}

	void CheckAITarget()
	{
		_elapsedTime += Time.deltaTime;
		if (hasTarget)
			_elapsedTime = 0;
		else if (_elapsedTime > 0.5f)
			enemyTarget = transform.position + new Vector3(-1, 0, 0);
	}

	void FixedUpdate()
	{
		if (aiWeapon)
		{
			AimAI();
			AI();
		}
		else
			Aim();
	}

	void GetInput()
	{

		if (Input.GetKeyDown(KeyCode.Mouse0))
			_coFire = StartCoroutine(Fire());

		if (!Input.GetKey(KeyCode.Mouse0))
		{
			if (_coFire != null)
				StopCoroutine(_coFire);
		}

	}

	void AI()
	{
		if (hasTarget && firstRun)
		{
			_coFireAI = StartCoroutine(FireAI());
			firstRun = false;
		}
	}

	// void Fire()
	// {
	// 	_bulletPool.SetBulletActive(barrel.transform.position, gameObject.transform.rotation);
	// 	// projectile.transform.position = barrel.transform.position;
	// 	// projectile.transform.rotation = gameObject.transform.rotation;
	// }

	void AimAI()
	{
		transform.rotation = Quaternion.LookRotation(Vector3.forward, enemyTarget - transform.position + new Vector3(0, 0, 90));

		if (transform.rotation.eulerAngles.z > 5 && transform.rotation.eulerAngles.z < 175)
			_sprite.flipX = true;
		else if (transform.rotation.eulerAngles.z >= 185 && transform.rotation.eulerAngles.z <= 355)
			_sprite.flipX = false;

		if (enemyTarget.x > transform.position.x)
			transform.localPosition = new Vector3(0.073f, 0, 0);
		else
			transform.localPosition = new Vector3(-0.073f, 0, 0);


		if (_sprite.flipX == false)
			barrel.transform.localPosition = new Vector3(-0.044f, barrel.transform.localPosition.y, barrel.transform.localPosition.z);
		else
			barrel.transform.localPosition = new Vector3(0.044f, barrel.transform.localPosition.y, barrel.transform.localPosition.z);
	}

	void Aim()
	{

		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 mousePosV = Camera.main.ScreenToViewportPoint(Input.mousePosition);

		transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position + new Vector3(0, 0, 90));

		if (transform.rotation.eulerAngles.z > 5 && transform.rotation.eulerAngles.z < 175)
			_sprite.flipX = true;
		else if (transform.rotation.eulerAngles.z >= 185 && transform.rotation.eulerAngles.z <= 355)
			_sprite.flipX = false;


		if (mousePosV.x > 0.5)
			transform.localPosition = new Vector3(0.073f, 0, 0);
		else
			transform.localPosition = new Vector3(-0.073f, 0, 0);


		if (_sprite.flipX == false)
			barrel.transform.localPosition = new Vector3(-0.044f, barrel.transform.localPosition.y, barrel.transform.localPosition.z);
		else
			barrel.transform.localPosition = new Vector3(0.044f, barrel.transform.localPosition.y, barrel.transform.localPosition.z);
	}

	IEnumerator Fire()
	{
		while (true)
		{
			_bulletPool.SetBulletActive(barrel.transform.position, gameObject.transform.rotation);
			yield return new WaitForSeconds(fireRate);
		}
	}

	IEnumerator FireAI()
	{
		while (true)
		{
			_bulletPool.SetBulletActive(barrel.transform.position, gameObject.transform.rotation);
			yield return new WaitForSeconds(fireRate);
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			enemyTarget = other.gameObject.transform.position;
			hasTarget = true;
		}
		else
		{
			hasTarget = false;
			if (_coFireAI != null)
				StopCoroutine(_coFireAI);
		}

	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			if (_coFireAI != null)
				StopCoroutine(_coFireAI);

			hasTarget = false;
			firstRun = true;
		}
	}

}
