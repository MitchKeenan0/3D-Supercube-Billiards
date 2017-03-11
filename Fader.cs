using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour {

	public float fadeSpeed = 0.8f;
	public Texture2D fadeTex;

	float alpha = 0.0f;
	int drawDepth = -1000;
	int fadeDir = 1; //-1 in, 1 out

	void OnGUI()
	{
		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		alpha = Mathf.Clamp01(alpha);

		GUI.color = new Color ( GUI.color.r, GUI.color.g, GUI.color.b, alpha );
		GUI.depth = drawDepth;
		GUI.DrawTexture ( new Rect (0,0, Screen.width, Screen.height), fadeTex );
	}

	

}
