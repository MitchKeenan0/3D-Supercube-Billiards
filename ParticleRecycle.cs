using UnityEngine;
using System.Collections;

public class ParticleRecycle : MonoBehaviour {

	ParticleSystem parti;

	void Start()
	{
		parti = GetComponent<ParticleSystem>();

		Destroy(this.gameObject, parti.duration);
	}

}
