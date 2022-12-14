using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

	Rigidbody2D _rb;
	Vector3 _target = new Vector3(0, 0, 0);
	Vector2 _direction = new Vector3(0, 0, 0);
	Animator _anim;
	Shader guiText;
	Shader defaultShader;
	float moveSpeed = 0f;

	EnemySpawner spawner;


	public BloodSplattPool bloodSplattPool;
	public SpriteRenderer sprite;
	public float DEFAULT_MOVESPEED = 0.25f;
	public float health = 6f;

	public AudioClip[] hitSounds;
	public AudioClip deathSound;




	GameObject _player;

	void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
		_player = GameObject.Find("Player");
		//sprite = GetComponent<SpriteRenderer>();
		guiText = Shader.Find("GUI/Text Shader");
		defaultShader = Shader.Find("Sprites/Default");
		_anim = GetComponent<Animator>();
		_anim.SetBool("Following", true);
		moveSpeed = DEFAULT_MOVESPEED;
		spawner = gameObject.transform.parent.GetComponent<EnemySpawner>();
	}

	void Update()
	{
		//_direction.Normalize();
		Move();
		//CheckDistanceFromPlayer();

		if (gameObject == null || !gameObject.activeInHierarchy)
			StopAllCoroutines();
	}


	void Move()
	{
		if (_player != null && _player.activeInHierarchy)
		{
			_target = _player.transform.position;
			_direction = (_target - transform.position).normalized;

			_rb.position = _rb.position + _direction * moveSpeed * Time.deltaTime;
		}
		else
		{
			_rb.position = _rb.position;
		}
	}

	void CheckDistanceFromPlayer()
	{
		if (_player != null && _player.activeInHierarchy)
			if (Vector3.Distance(_player.transform.position, transform.position) > 14)
				gameObject.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (gameObject.activeInHierarchy && other.gameObject.tag == "Projectile")
			TakeDamage(other.gameObject.GetComponent<Projectile>().damage);
	}

	public void TakeDamage(float damage)
	{
		health -= damage;
		if (health <= 0)
			Die();
		else
			StartCoroutine(FlashWhite());

		if (health <= 0)
			health = 6f;
	}

	public void ResetMoveSpeed()
	{
		moveSpeed = DEFAULT_MOVESPEED;
	}

	void Die()
	{

		gameObject.SetActive(false);
		GameManager.Instance.kills++;
		bloodSplattPool.SetBloodSplatt(gameObject.transform.position, Quaternion.identity);
		GameManager.Instance.audioManager.PlaySFX(0, deathSound);
		spawner.enemyCount--;
	}

	IEnumerator FlashWhite()
	{
		sprite.material.shader = guiText;
		moveSpeed = -moveSpeed;
		GameManager.Instance.audioManager.PlaySFX(1, hitSounds[Random.Range(0, 2)]);
		yield return new WaitForSeconds(0.25f);
		sprite.material.shader = defaultShader;
		moveSpeed = -moveSpeed;
	}



}
