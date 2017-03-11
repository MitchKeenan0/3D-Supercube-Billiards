using UnityEngine;
using System.Collections;
public class RecycleBullet : MonoBehaviour {

	public float lifeTime;
	Collider col;
	

	void OnEnable()
	{
		col = GetComponent<Collider>();
		col.enabled = true;

		Invoke("Recycle", lifeTime);
	}

	void OnDisable()
	{
		CancelInvoke();
	}


	void Recycle()
	{
		gameObject.SetActive(false);
	}


	void OnCollisionExit(Collision c)
	{
		col.enabled = false;
	}

}
