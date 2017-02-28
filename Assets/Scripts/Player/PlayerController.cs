using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Invector.CharacterController;

public class PlayerController : MonoBehaviour {

	public Checkpoint lastCheckpoint;

	public int health = 3;
  public vThirdPersonController cc; // access the ThirdPersonController component                
	protected Timer timer;
	Rigidbody _rigidbody;

	void Awake() {
		_rigidbody = GetComponent<Rigidbody>();
	}

	void Start() {
		Init();
	}
	void Init() {
    cc = GetComponent<vThirdPersonController>();
		timer = GetComponent<Timer>();
		Spawn();
	}

	void Spawn() {
		// If we don't have a checkpoint
		if (lastCheckpoint == null) {
			// Find the spawn point and get its Checkpoint script
			lastCheckpoint = GameObject.FindGameObjectWithTag("SpawnPoint").
												GetComponent<Checkpoint>();
			// Collect the checkpoint
			lastCheckpoint.Collect();
		}

		// Set the transform position of player to that of the checkpoint
		SetPosition(lastCheckpoint.transform.position);
	}

	void SetPosition(Vector3 newPosition) {
		transform.position = newPosition;
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Void") {
			CollideVoid();
		} else if (other.tag == "CheckPoint") {
			Checkpoint checkpoint = other.gameObject.GetComponent<Checkpoint>();
			CollideCheckpoint(checkpoint);
		} else if (other.tag == "EndPoint") {
			Endpoint endpoint = other.gameObject.GetComponent<Endpoint>();
			CollideEndPoint(endpoint);
		}
	}

	void Fell() {
		// Stop the timer
		timer.isTimerRunning = false;
		// Set player movement to zero
		cc.input = Vector2.zero;
		// Lock player movement
		cc.lockMovement = true;
		// Spawn
		Spawn();
	}

	void CollideVoid() {
		Debug.Log("YOU FELL!"); 
		health --;

		if (health > 0) {
			Fell();
		} else {
			float fadeTime = gameObject.GetComponent<Fading>().BeginFade(1);
			Invoke("GameOver", fadeTime);
		}
	}

	void CollideCheckpoint(Checkpoint checkpoint) {
		Debug.Log("YOU GOT A CHECKPOINT");
		// Set out last checkpoint to checkpoint
		lastCheckpoint = checkpoint;

		// Collect checkpoint
		lastCheckpoint.Collect();
	}

	void CollideEndPoint(Endpoint endpoint) {
		Debug.Log("YOU FINISHED THE LEVEL!");
		float fadeTime = gameObject.GetComponent<Fading>().BeginFade(1);
		// Set out last checkpoint to checkpoint
		// endpoint.Collect();
		Invoke("EndLevel", fadeTime);
	}

	public void GameOver() {
		SceneManager.LoadScene("GameOverScene");
	}

	public void EndLevel() {
		SceneManager.LoadScene("EndScene");
	}
}
