using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edgetext : MonoBehaviour
{
	Camera m_Camera = Camera.main;
	void Update()
	{
		
		transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
			m_Camera.transform.rotation * Vector3.up);

		// float xDist = m_Camera.transform.position.x - transform.position.x;
		// float yDist = m_Camera.transform.position.y - transform.position.y; 
		// float zDist = m_Camera.transform.position.z - transform.position.z;  
		// float dist = (float)Mathf.Sqrt(xDist * xDist + yDist * yDist + zDist * zDist);

		// if(dist > 30)
		// {

		// }else{

		// }
	}
}