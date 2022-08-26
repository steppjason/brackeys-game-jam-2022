using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	Vector2 _direction;
	Rigidbody2D _rb;
	Animator _anim;
	float _health;
	float _damageTimer;

	public GameObject gameOver;

	public BloodSplattPool bloodSplattPool;
	public GameObject cam;
	public GameObject bounds;
	public float moveSpeed = 1f;
	public float maxHealth = 100f;
	public Image healthBar;

	public GameObject pistol;
	public GameObject shotgun;
	public GameObject machineGun;
	public float damageRate;



	void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
		_anim = GetComponent<Animator>();
		_health = maxHealth;
	}

	void Update()
	{
		Move();
		UpdateCameraPosition();
		healthBar.fillAmount = _health / 100;
	}

	void Move()
	{
		_direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		_direction.Normalize();

		_rb.position = _rb.position + _direction * moveSpeed * Time.deltaTime;

		if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
			_anim.SetBool("Walking", true);
		else
			_anim.SetBool("Walking", false);

	}

	void UpdateCameraPosition()
	{
		cam.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, cam.transform.position.z);
		bounds.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, cam.transform.position.z);
	}

	void OnCollisionStay2D(Collision2D other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			_damageTimer += Time.deltaTime;
			if (_damageTimer > damageRate)
			{
				TakeDamage();
				_damageTimer = 0;
			}
		}
	}

	void TakeDamage()
	{
		GameManager.Instance.cameraShake.ShakeCamera(5f, 0.05f);
		_health -= 1;
		if (_health <= 0)
			Die();
	}

	void Die()
	{
		bloodSplattPool.SetBloodSplatt(gameObject.transform.position, Quaternion.identity);
		gameOver.SetActive(true);
		GameManager.Instance.gameOver = true;
		gameObject.SetActive(false);
	}
}
