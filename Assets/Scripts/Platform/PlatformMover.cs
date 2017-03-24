using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour {
	public static string PLATFORM_DESTINATION_TAG = "PlatformDestination";
	int MAX_INDEX = 0;

	public List<Transform> Destinatons;

	public Vector3 nextDestination;

	Vector3 destinationPosition;

	int incrementor;

	int currentDestinationIndex;

	public float smoothTime = 0.7f;

	public float changeDestinationTime = 7.0f;

	ParticleSystem particles;

	void Awake() {
		foreach (Transform child in transform) {
    	if (child.tag == PLATFORM_DESTINATION_TAG) {
      	Destinatons.Add(child.transform);
      }
    }
		particles = GetComponentInChildren<ParticleSystem>();
	}

	// Use this for initialization
	void Start () {
		MAX_INDEX = Destinatons.Count - 1;
		currentDestinationIndex = 0;
		incrementor = 1;
		ChangeDestination();
		startParticles();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		destinationPosition = Vector3.Lerp(transform.position, nextDestination, smoothTime * Time.deltaTime);
		transform.position = destinationPosition;
	}

	void ChangeDestination() {		
		// If currentDestinationIndex is the first in the List
		// Make sure the incrementor is positive
		if (currentDestinationIndex == 0) {
			// Make incrementor positive
			incrementor = Mathf.Abs(incrementor);
		} else if (currentDestinationIndex >= MAX_INDEX) {
			// Make incrementor negative
			// So we go back next time
			incrementor *= -1;
		}

		currentDestinationIndex += incrementor;

		// Set next destination
		nextDestination = Destinatons[currentDestinationIndex].position;

		// Call this again at changeDestinationTime
		Invoke("ChangeDestination", changeDestinationTime);
		Invoke("startParticles", 5f);
	}

	void startParticles() {
		particles.Play();
		Invoke("stopParticles", 0.5f);
	}
	void stopParticles() {
		particles.Stop();
	}
}
