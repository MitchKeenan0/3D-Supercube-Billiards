using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AsteroidHandle : MonoBehaviour {

	public int maxPopulation = 5;
	public float baseSpawnTime = 2f;
	public float spawnOffsetScaler = 2f;
	public float spawnRange = 15f;
	public float circleRange = 10f;
	public float circleElevation = 2f;
	public float livesSaved;
	public GameObject asteroidPrefab;
	
	float timeSinceAsteroid = 0;

	List<GameObject> AsteroidsList;


	void Start ()
	{
		AsteroidsList = new List<GameObject>();

		for (int i = 0; i < maxPopulation; i++)
		{
			GameObject asteroid = (GameObject)Instantiate(asteroidPrefab);
			asteroid.SetActive(false);
			AsteroidsList.Add( asteroid );
		}

		SceneManager.UnloadScene("world1");
	}


	void Update ()
	{
		SpawnAsteroids();
	}


	Vector3 CreateAsteroidPosition()
	{
		Vector3 spawnPosition = Vector3.zero;

		spawnPosition.z = spawnRange;
		spawnPosition.x = Random.insideUnitCircle.x * circleRange;
		spawnPosition.y = Random.insideUnitCircle.y * circleRange + circleElevation;

		return spawnPosition;
	}


	void SpawnAsteroids()
	{
		float spawnTimeOffset = (baseSpawnTime * Random.value) * spawnOffsetScaler;
		float actualSpawnTime = baseSpawnTime + spawnTimeOffset;

		if (Time.time > actualSpawnTime + timeSinceAsteroid)
		{
			for (int i = 0; i < AsteroidsList.Count; i++)
			{
				if (!AsteroidsList[i].activeInHierarchy)
				{
					AsteroidsList[i].transform.position = CreateAsteroidPosition();

					Rigidbody asteroidRb = AsteroidsList[i].GetComponent<Rigidbody>();
					asteroidRb.velocity = Vector3.zero;
					asteroidRb.angularVelocity = Vector3.zero;

					AsteroidsList[i].SetActive(true);
					break;
				}
			}

			timeSinceAsteroid = Time.time;
		}
	}


	/// Lives Counter
	void OnGUI()
	{
		int w = Screen.width, h = Screen.height;

		GUIStyle style = new GUIStyle();

		Rect rect = new Rect(0, 0, w, h * 2 / 50);
		style.alignment = TextAnchor.UpperRight;
		style.fontSize = h * 2 / 50;

		style.normal.textColor = Color.white;

		string text = string.Format("Lives Saved: {00000000000000000000000000000000}", 
									Mathf.Ceil(livesSaved));

		GUI.Label(rect, text, style);
	}


}
