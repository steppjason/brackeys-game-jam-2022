using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
	public GameObject target; 

	void Update()
	{
		var wantedPos = Camera.main.WorldToScreenPoint(target.transform.position);
		transform.position = wantedPos;
	}
}
