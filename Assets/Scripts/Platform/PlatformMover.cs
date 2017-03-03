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

	void Awake() {
		foreach (Transform child in transform) {
    	if (child.tag == PLATFORM_DESTINATION_TAG) {
      	Destinatons.Add(child.transform);
      }
    }
	}

	// Use this for initialization
	void Start () {
		MAX_INDEX = Destinatons.Count - 1;
		currentDestinationIndex = 0;
		incrementor = 1;
		ChangeDestination();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		destinationPosition = Vector3.Lerp(transform.position, nextDestination, smoothTime * Time.deltaTime);
		transform.position = destinationPosition;

		// TODO: When platform has reach destination => Invoke ChangeDestination
		// if (transform.position == destinationPosition) {
		// 	// Invoke("ChangeDestination", changeDestinationTime);
		// 	Debug.Log("Reached Destinaton");
		// 	ChangeDestination();
		// }
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
	}
}
