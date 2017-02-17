using UnityEngine;

public class Endpoint : MonoBehaviour {
	BoxCollider boxCollider;
	MeshRenderer meshRenderer;

	// Use this for initialization
	void Awake () {
		Init();
	}
	
	void Init() {
		boxCollider = GetComponent<BoxCollider>();
		meshRenderer = GetComponent<MeshRenderer>();
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
