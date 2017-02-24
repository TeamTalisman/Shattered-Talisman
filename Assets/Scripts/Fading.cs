using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fading : MonoBehaviour {

	// Texture to overlap the screen
	public Texture2D fadeOutTexture;

	// The speed of the fading Transition
	public float fadeSpeed = 0.8f;

	// Texture order in the Draw Hierarchy
	private int drawDepth = -1000;

	// The fadeOutTexture's alpha (from 0 to 1)
	private float alpha = 1f;

	// The direction to fade: in = -1, out = 1
	private int fadeDir = -1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI ()
	{
		// Fade in/out the alpha value using a direction, a speed and deltaTime to convert the operation to seconds
		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		// Force clamping alpha between 0 and 1
		alpha = Mathf.Clamp01(alpha); 

		// Set color of GUI, just changing the alpha value
		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		// Make the texture render on top
		GUI.depth = drawDepth;
		// Draw the texture to fit the entire screen
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeOutTexture);
	}

	// Sets fadeDir to the direction parameter making the scene fade in if -1 and out if 1
	public float BeginFade (int direction) {
		fadeDir = direction;
		return fadeSpeed;
	}

	// When OnLevelFinishedLoading is called
	void OnLevelFinishedLoading () {
		BeginFade (-1);
	}
}
