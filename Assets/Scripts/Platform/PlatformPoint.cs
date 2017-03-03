using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPoint : MonoBehaviour {

	Vector3 fixedPosition;
	MeshRenderer meshRenderer;

	void Awake () {
		// Get the meshRenderer so we can hide it in play mode
		meshRenderer = GetComponent<MeshRenderer>();

		// Get a reference to the intiial position
		fixedPosition = transform.position;
	}

	// Use this for initialization
	void Start() {
		// Disable the renderer
		meshRenderer.enabled = false;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		// Make sure we keep this position
		transform.position = fixedPosition;
	}
}
