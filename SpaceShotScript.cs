using UnityEngine;
using System.Collections;

public class SpaceShotScript : MonoBehaviour {

	public float lifeTime;
	public float minSize;
	public float scaleSpeed;
	public Vector3 baseScale;
	public GameObject impact;

	bool shrink = false;

	Collider col;
	GameObject impactBoom;


	void OnEnable()
	{
		col = GetComponent<Collider>();
		col.enabled = true;

		Invoke("Recycle", lifeTime);

		shrink = true;
	}

	void OnDisable()
	{
		CancelInvoke();
		shrink = false;
		transform.localScale = baseScale;
	}


	void Update()
	{
		if (shrink)
		{
			transform.localScale = Vector3.Lerp(transform.localScale, 
												baseScale * minSize, 
												scaleSpeed * Time.deltaTime);
		}
	}


	void OnCollisionEnter(Collision c)
	{
		ContactPoint contact = c.contacts[0];
		Quaternion rotator = Quaternion.FromToRotation(Vector3.forward, contact.normal);

		impactBoom = (GameObject)Instantiate(impact,
												transform.TransformPoint(contact.point),
												rotator);
	}

	void OnCollisionExit(Collision c)
	{
		shrink = false;
		col.enabled = false;
	}


	void Recycle()
	{
		gameObject.SetActive(false);
	}

}
