using UnityEngine;
using System.Collections;

public class LookControl : MonoBehaviour {

	public Transform shooter;
	Vector3 lookVector;
	Camera cam;
	

	void Start()
	{
		cam = FindObjectOfType<Camera>();
	}

	void Update()
	{
		Ray rayToMouse = cam.ScreenPointToRay(Input.mousePosition);
		
		Vector3 lookVector = transform.position + rayToMouse.direction;

		transform.LookAt(lookVector);

	}
}
