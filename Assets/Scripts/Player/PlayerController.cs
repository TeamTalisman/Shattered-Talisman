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
	// Rigidbody _rigidbody;
	ParticleSystem particles;

	void Awake() {
		// _rigidbody = GetComponent<Rigidbody>();
		particles = GetComponentInChildren<ParticleSystem>();
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
		} else if (other.tag == "LevelPoint") {
			Invoke("CollideLevelPoint", 0.2f);
		} else if (other.tag == "EndPoint") {
			Invoke("CollideEndPoint", 0.2f);
		}
	}

	void Fell() {
	
		// Set player movement to zero
		cc.input = Vector2.zero;
		// Lock player movement
		cc.lockMovement = true;
		// Spawn
		Spawn();
	}

	void CollideVoid() {
		health --;

		if (health > 0) {
			Fell();
		} else {
			float fadeTime = gameObject.GetComponent<Fading>().BeginFade(1);
			Invoke("GameOver", fadeTime);
		}
	}

	void CollideCheckpoint(Checkpoint checkpoint) {
		// Set out last checkpoint to checkpoint
		lastCheckpoint = checkpoint;
		particles.Play();
		
		// Collect checkpoint
		lastCheckpoint.Collect();
		Invoke("stopParticles", 0.5f);
	}

	void CollideLevelPoint() {
		float fadeTime = gameObject.GetComponent<Fading>().BeginFade(1);

		Invoke("NextLevel", fadeTime);
	}

	void CollideEndPoint() {
		float fadeTime = gameObject.GetComponent<Fading>().BeginFade(1);

		Invoke("EndLevel", fadeTime);
	}

	public void GameOver() {
		SceneManager.LoadScene("GameOverScene");
	}

	public void NextLevel() {
		SceneManager.LoadScene("Level 2");
	}

	public void EndLevel() {
		SceneManager.LoadScene("EndScene");
	}

	void stopParticles() {
		particles.Stop();
	}
}
