using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapWrapper : MonoBehaviour 
{
	public float mapWidth;
	public float mapHeight;

	public float chunkWidth;
	public float chunkHeight;

	void Start() 
	{
		
	}

	void LateUpdate() 
	{
		//transform.position += new Vector3(1 * Time.deltaTime, 1 * Time.deltaTime, 0);

		if(transform.position.x > mapWidth)
			transform.position = new Vector3(-chunkWidth, transform.position.y, transform.position.z);
			

		if(transform.position.x < -mapWidth)
			transform.position = new Vector3(chunkWidth, transform.position.y, transform.position.z);

		if(transform.position.y > mapHeight)
			transform.position = new Vector3(transform.position.x, -chunkHeight, transform.position.z);

		if(transform.position.y < -mapHeight)
			transform.position = new Vector3(transform.position.x, chunkHeight, transform.position.z);

	}
}
