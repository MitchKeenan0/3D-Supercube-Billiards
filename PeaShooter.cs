using UnityEngine;
using System.Collections.Generic;

public class PeaShooter : MonoBehaviour {

	public float shotForce;
	public float refireTime;
	public float curveScalar;
	public int bulletPool = 2;
	public GameObject prefab;
	public Transform emitPoint;

	float timeSinceLastShot = 0;
	float curveX;
	float curveY;
	float totalMove;
	bool check = true;

	List<GameObject> bullets;
	Rigidbody shotRb;
	Vector3 currentPosition;
	Vector3 deltaPosition;
	Vector3 lastPosition;
	Vector3 originPosition;


	void Start()
	{
		bullets = new List<GameObject>();

		for (int i = 0; i < bulletPool; i++)
		{
			GameObject obj = (GameObject)Instantiate(prefab);
			obj.SetActive(false);
			bullets.Add(obj);
		}
	}

	void Update()
	{
		if (Input.GetButton("Fire1"))
		{
			if (check)
			{
				originPosition = Input.mousePosition;
				check = false;
			}
			
			currentPosition = Input.mousePosition;
			deltaPosition = currentPosition - lastPosition;
			lastPosition = currentPosition;
		}

		if (Input.GetButtonUp("Fire1"))
		{
			FireGun(shotForce);

			totalMove = Vector3.Distance(originPosition, lastPosition);
		}

		if (shotRb)
		{
			if (totalMove > 150f)
			{
				shotRb.AddForce(-deltaPosition.x * Vector3.right * curveScalar
								- deltaPosition.y * Vector3.up * curveScalar);
			}
		}

	}


	void FireGun(float shotPower)
	{
		if (Time.time > refireTime + timeSinceLastShot)
		{
			for (int i = 0; i < bullets.Count; i++)
			{
				if (!bullets[i].activeInHierarchy)
				{
					bullets[i].transform.position = emitPoint.position;
					bullets[i].transform.rotation = emitPoint.rotation;
					bullets[i].SetActive(true);

					shotRb = bullets[i].GetComponent<Rigidbody>();
					shotRb.velocity = Vector3.zero;

					Vector3 trajectory = transform.forward;
					shotRb.AddForce(trajectory * shotPower);

					check = true;
					break;
				}
			}

			timeSinceLastShot = Time.time;
		}
	}
	
}
