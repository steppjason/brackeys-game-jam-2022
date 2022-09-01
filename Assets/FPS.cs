using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class FPS : MonoBehaviour 
{
	float fps;
	public TMP_Text counter;
	void Start() 
	{
		
	}

	void Update() 
	{
		fps = 1f / Time.deltaTime;
		counter.text = String.Format("{0:0}", fps.ToString());
	}
}
