using UnityEngine;
using System.Collections;

public class BeamEmitter : MonoBehaviour {

	public float rayLength;
	public float forceStr;
	public float platePull;
	public bool success = false;
	public GameObject powerPlate;

	RaycastHit hit;
	Rigidbody rb;
	MeshRenderer plateRender;


	void Start ()
	{
		success = false;

		rb = GetComponent<Rigidbody>();
		rb.AddTorque(-Vector3.up * rb.mass);

	}


	void FixedUpdate()
	{
		// Beam
		if (Physics.Raycast(transform.position + transform.forward, // avoids the emit pad collider
							transform.forward * rayLength, 
							out hit))
		{
			/// light powerplate
			if (hit.collider.gameObject == powerPlate)
			{
				plateRender = powerPlate.GetComponent<MeshRenderer>();
				plateRender.material.SetColor("_EmissionColor", Color.grey);

				success = true;
			}

			/// push asteroids
			else if (hit.rigidbody)
			{
				Rigidbody hitRb = hit.rigidbody;
				Vector3 beamPush = hit.transform.position - transform.position;

				hitRb.AddForceAtPosition(beamPush.normalized * (forceStr / hit.distance),
										 hit.point);
			}
		}


		if (success)
		{
			/// stick to plate
			rb.angularVelocity = Vector3.Lerp(rb.angularVelocity,
												Vector3.zero,
												Time.deltaTime * platePull * 2.2f);

			transform.rotation = 
				Quaternion.Slerp
					(transform.rotation, 
					 Quaternion.LookRotation(powerPlate.transform.position - transform.position, transform.up),
					 Time.deltaTime * platePull);
		}

	}//Update
	
}
