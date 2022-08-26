using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
	GameObject _player;

	public Vector3 cappedTargetScreenPOS;
	public Vector3 screenPOS;
	void Start()
	{
		_player = GameObject.Find("Player");
	}

	void FixedUpdate()
	{
		transform.localRotation = Quaternion.LookRotation(Vector3.forward, gameObject.transform.parent.position - transform.position);

		screenPOS = Camera.main.WorldToScreenPoint(gameObject.transform.parent.position);
		if (
				screenPOS.x <= 100f ||
				screenPOS.x >= Screen.width - 100f ||
				screenPOS.y <= 100f ||
				screenPOS.y >= Screen.height - 100f
		)
		{
			cappedTargetScreenPOS = screenPOS;
			if (cappedTargetScreenPOS.x <= 100f) cappedTargetScreenPOS.x = 100f;
			if (cappedTargetScreenPOS.y <= 100f) cappedTargetScreenPOS.y = 100f;
			if (cappedTargetScreenPOS.x >= Screen.width - 100f) cappedTargetScreenPOS.x = Screen.width - 100f;
			if (cappedTargetScreenPOS.y >= Screen.height - 100f) cappedTargetScreenPOS.y = Screen.height - 100f;
			gameObject.GetComponent<Renderer>().enabled = true;
		}
		else
		{
			gameObject.GetComponent<Renderer>().enabled = false;
		}

		transform.position = Camera.main.ScreenToWorldPoint(cappedTargetScreenPOS);

	}
}
