using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Director_GameHandle : MonoBehaviour {

	public float timeAtWin;
	public float downTime;

	bool done = false;

	BeamEmitter beam;
	Fader fader;
	Rigidbody cube;


	void Awake()
	{
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}


	void Start()
	{
		beam = FindObjectOfType<BeamEmitter>();
		cube = beam.GetComponent<Rigidbody>();

		fader = GetComponent<Fader>();
		fader.enabled = false;
	}

	void OnLevelWasLoaded()
	{
		fader.enabled = false;
	}


	void Update()
	{
		if (beam.success)
		{
			if (!done)
			{
				timeAtWin = Time.time;
				fader.enabled = true;

				done = true;
			}
			
			/// wait for fade
			if (Time.time > downTime + timeAtWin)
			{
				beam.success = false;

				DontDestroyOnLoad(gameObject);
				SceneManager.LoadScene("world2");
			}
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene("world1");
			fader.enabled = false;
		}

	}
	
}
