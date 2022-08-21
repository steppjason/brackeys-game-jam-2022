using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

	SpriteRenderer _sprite;

	public GameObject barrel;
	public GameObject blast;

	void Start()
	{
		_sprite = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		GetInput();
		Aim();

		
		
	}

	void GetInput()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
			Fire();
	}

	void Fire()
	{
		blast.transform.position = barrel.transform.position;
		blast.transform.rotation = gameObject.transform.rotation;
	}

	void Aim()
	{

		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 mousePosV = Camera.main.ScreenToViewportPoint(Input.mousePosition);

		gameObject.transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - gameObject.transform.position + new Vector3(0, 0, 90));

		if (gameObject.transform.rotation.eulerAngles.z > 5 && gameObject.transform.rotation.eulerAngles.z < 175)
			_sprite.flipX = true;		
		else if (gameObject.transform.rotation.eulerAngles.z >= 185 && gameObject.transform.rotation.eulerAngles.z <= 355)
			_sprite.flipX = false;
		

		if (mousePosV.x > 0.5)
			gameObject.transform.localPosition = new Vector3(0.073f, 0, 0);
		else
			gameObject.transform.localPosition = new Vector3(-0.073f, 0, 0);


		if (_sprite.flipX == false)
			barrel.transform.localPosition = new Vector3(-0.044f, barrel.transform.localPosition.y, barrel.transform.localPosition.z);
		else
			barrel.transform.localPosition = new Vector3(0.044f, barrel.transform.localPosition.y, barrel.transform.localPosition.z);
	}
}
