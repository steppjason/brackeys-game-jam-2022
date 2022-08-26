using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

	//SpriteRenderer _sprite;
	public SpriteRenderer _sprite;
	Animator _anim;
	Coroutine _coFire;
	Coroutine _coFireAI;

	float _elapsedTime = 0f;
	float _weaponFireElapsedTime = 0f;

	public GameObject barrel;
	public BulletPool _bulletPool;
	public float fireRate = 0.5f;

	public bool aiWeapon = false;
	public bool hasTarget = false;

	public AudioClip gunSound;

	bool firstRun = true;
	Vector3 enemyTarget = new Vector3(-1, 0, 0);
	GameObject _target;

	Vector3 mousePos = new Vector3(0, 0, 0);
	Vector3 mousePosV = new Vector3(0, 0, 0);

	Vector3 defaultRotation = new Vector3(0, 0, 90);

	Vector3 localPosRight = new Vector3(0.276f, -0.1f, 0);
	Vector3 localPosLeft = new Vector3(-0.276f, -0.1f, 0);

	Vector3 barrelPosRight = new Vector3(-0.132f, 0.727f, 0);
	Vector3 barrelPosLeft = new Vector3(0.132f, 0.727f, 0);

	public float shake;
	public float shakeAI;

	void Start()
	{
		//		_sprite = GetComponent<SpriteRenderer>();
		_anim = GetComponent<Animator>();
	}

	void Update()
	{
		if (!aiWeapon)
			GetInput();

		if (aiWeapon)
		{
			AimAI();
			AI();
		}
		else
			Aim();

		CheckAITarget();
	}

	void CheckAITarget()
	{
		_elapsedTime += Time.deltaTime;
		_weaponFireElapsedTime += Time.deltaTime;

		if (hasTarget)
			_elapsedTime = 0;
		else if (_elapsedTime > 0.5f)
			enemyTarget = transform.position + new Vector3(-1, 0, 0);
	}


	void GetInput()
	{
		if (_weaponFireElapsedTime > fireRate)
		{

			if (Input.GetKey(KeyCode.Mouse0))
			{
				_weaponFireElapsedTime = 0;
				_coFire = StartCoroutine(Fire());
			}

			if (!Input.GetKey(KeyCode.Mouse0))
			{
				if (_coFire != null)
					StopCoroutine(_coFire);

				_anim.SetBool("Fire", false);
			}
		}

	}

	void AI()
	{
		if (hasTarget && firstRun && _weaponFireElapsedTime > fireRate)
		{
			_weaponFireElapsedTime = 0;
			_coFireAI = StartCoroutine(FireAI());
			firstRun = false;
		} else {
			_anim.SetBool("Fire", false);
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
		if (_target != null && hasTarget)
			enemyTarget = _target.transform.position;

		transform.rotation = Quaternion.LookRotation(Vector3.forward, enemyTarget - transform.position + defaultRotation);

		if (transform.rotation.eulerAngles.z > 5 && transform.rotation.eulerAngles.z < 175)
			_sprite.flipX = true;
		else if (transform.rotation.eulerAngles.z >= 185 && transform.rotation.eulerAngles.z <= 355)
			_sprite.flipX = false;

		if (enemyTarget.x > transform.position.x)
			transform.localPosition = localPosRight;
		else
			transform.localPosition = localPosLeft;


		if (_sprite.flipX == false)
			barrel.transform.localPosition = new Vector3(-0.141f, barrel.transform.localPosition.y, barrel.transform.localPosition.z);
		else
			barrel.transform.localPosition = new Vector3(0.141f, barrel.transform.localPosition.y, barrel.transform.localPosition.z);
	}

	void Aim()
	{

		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePosV = Camera.main.ScreenToViewportPoint(Input.mousePosition);

		transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position + defaultRotation);

		if (transform.rotation.eulerAngles.z > 5 && transform.rotation.eulerAngles.z < 175)
			_sprite.flipX = true;
		else if (transform.rotation.eulerAngles.z >= 185 && transform.rotation.eulerAngles.z <= 355)
			_sprite.flipX = false;


		if (mousePosV.x > 0.5f)
			transform.localPosition = localPosRight;
		else
			transform.localPosition = localPosLeft;


		if (_sprite.flipX == false)
			barrel.transform.localPosition = new Vector3(-0.141f, barrel.transform.localPosition.y, barrel.transform.localPosition.z);
		else
			barrel.transform.localPosition = new Vector3(0.141f, barrel.transform.localPosition.y, barrel.transform.localPosition.z);
	}

	IEnumerator Fire()
	{
		while (true)
		{
			_bulletPool.SetBulletActive(barrel.transform.position, gameObject.transform.rotation);
			_anim.SetBool("Fire", true);
			GameManager.Instance.audioManager.PlaySFX(2, gunSound);
			GameManager.Instance.cameraShake.ShakeCamera(shake, 0.1f);
			yield break;
		}
	}

	IEnumerator FireAI()
	{
		while (true)
		{
			_bulletPool.SetBulletActive(barrel.transform.position, gameObject.transform.rotation);
			_anim.SetBool("Fire", true);
			GameManager.Instance.audioManager.PlaySFX(3, gunSound);
			//GameManager.Instance.cameraShake.ShakeCamera(shakeAI, 0.1f);
			yield return new WaitForSeconds(fireRate);
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			if (!hasTarget)
			{
				_target = other.gameObject;
				hasTarget = true;
			}
		}

		if (_target != null && !_target.activeInHierarchy)
		{
			hasTarget = false;
			firstRun = true;

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
