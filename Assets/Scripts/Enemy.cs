using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

	Rigidbody2D _rb;
	Vector3 _target = new Vector3(0, 0, 0);
	Vector2 _direction = new Vector3(0, 0, 0);
	Animator _anim;

	public SpriteRenderer sprite;
	Shader guiText;
	Shader defaultShader;

	public float moveSpeed = 1f;
	public float health = 1f;



	GameObject _player;

	void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
		_player = GameObject.Find("Player");
		//sprite = GetComponent<SpriteRenderer>();
		guiText = Shader.Find("GUI/Text Shader");
		defaultShader = Shader.Find("Sprites/Default");
		_anim = GetComponent<Animator>();
	}

	void Update()
	{
		_direction.Normalize();
		Move();
	}



	void Move()
	{
		if (_player != null)
		{
			if (_player.activeInHierarchy)
			{

				_anim.SetBool("Following", true);
				_target = _player.transform.position;
				_direction = (_target - transform.position).normalized;
				_rb.MovePosition(_rb.position + _direction.normalized * moveSpeed * Time.deltaTime);
			}
			else
			{
				_anim.SetBool("Following", false);
			}
		}
		else
		{
			_anim.SetBool("Following", false);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Projectile")
			TakeDamage(other.gameObject.GetComponent<Projectile>().damage);
	}

	public void TakeDamage(float damage)
	{
		health -= damage;
		if (health <= 0)
			Die();
		else
			StartCoroutine(FlashWhite());

	}

	void Die()
	{
		gameObject.SetActive(false);
	}

	IEnumerator FlashWhite()
	{
		sprite.material.shader = guiText;
		yield return new WaitForSeconds(0.1f);
		sprite.material.shader = defaultShader;
	}

}
