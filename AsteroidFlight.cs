using UnityEngine;
using System.Collections;

public class AsteroidFlight : MonoBehaviour {

	public float flightSpeed;
	public float growthMax;
	public float growthSpeed;
	public float despawnDepth;
	public float despawnXScreen;
	public float despawnYScreen;
	public float threatToHumanLife;
	public Vector3 baseScale;

	float despawnRangeX;
	float despawnRangeY;
	bool grow = false;

	Rigidbody rb;
	GameObject player;
	Camera cam;
	AsteroidHandle handle;
	Vector3 upScale;


	void Start ()
	{
		if (!handle)
			handle = GameObject.Find("Asteroider").GetComponent<AsteroidHandle>();

		despawnRangeX = Screen.width * despawnXScreen;
		despawnRangeY = Screen.height * despawnYScreen;
	}


	void OnEnable()
	{
		ReAquire();

		transform.localScale = baseScale;
		upScale = transform.localScale * (growthMax * Random.Range(0.3f, 3f));
		grow = true;

		Vector3 dangerVector = transform.position - player.transform.position;
		rb.velocity = -dangerVector * flightSpeed;
	}

	void OnDisable()
	{
		grow = false;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
	}
	

	void Update ()
	{
		if (grow)
		{
			transform.localScale = Vector3.Lerp(transform.localScale, 
												upScale, 
												Time.deltaTime * Time.deltaTime * growthSpeed);
		}

		/// Recycle
		Vector3 screenPos = cam.WorldToScreenPoint(transform.position);

		if (DetectScreenExit(screenPos))
		{

			float lives = Random.Range(0, threatToHumanLife * (Random.value * 10));
			handle.livesSaved += lives;

			gameObject.SetActive(false);
		}
	}



	void ReAquire()
	{
		if (!rb)
			rb = GetComponent<Rigidbody>();

		if (!player)
			player = FindObjectOfType<PlayerHandle>().gameObject;

		if (!cam)
			cam = player.GetComponentInChildren<Camera>();

		despawnRangeX = Screen.width * despawnXScreen;
		despawnRangeY = Screen.height * despawnYScreen;
	}


	bool DetectScreenExit(Vector3 pos)
	{
		if (Mathf.Abs(pos.x) > despawnRangeX
			||
			Mathf.Abs(pos.y) > despawnRangeY
			||
			transform.position.z > despawnDepth)
			return true;

		else
			return false;
	}
	

}
