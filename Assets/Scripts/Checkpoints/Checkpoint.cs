using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
	BoxCollider boxCollider;
	MeshRenderer meshRenderer;

	// Use this for initialization
	void Awake () {
		Init();
	}
	
	void Init() {
		boxCollider = GetComponent<BoxCollider>();
		meshRenderer = GetComponent<MeshRenderer>();

		// Disable meshRenderer
		meshRenderer.enabled = false;
	}

	public void Collect() {
		// User has collected it
		// We can disable collision now
		DisableCollision();
	}

	void DisableCollision() {
		// Disable collision so player can go through it
		boxCollider.enabled = false;
	}
}
