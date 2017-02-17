using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[System.SerializableAttribute]
	public class MoveSettings {
		public float forwardVelocity = 12;
		public float rotateVelocity = 100;
		public float jumpVelocity = 25;
		public float distanceToGrounded = 0.1f;
		public LayerMask ground;
	}

	[System.SerializableAttribute]
	public class PhysicSettings {
		public float downAcceleration = 0.75f;
	}

	[System.SerializableAttribute]
	public class InputSettings {
		public float inputDelay = 0.1f;
		public string FORWARD_AXIS = "Vertical";
		public string TURN_AXIS = "Horizontal";
		public string JUMP_AXIS = "Jump";
	}

	[System.SerializableAttribute]
	public class AnimatorSettings {
		public Animator animator;
		public float animationSpeedMultiplier = 1f;
	}

	Vector3 velocity = Vector3.zero;
	Quaternion targetRotation;
	Rigidbody rBody;
	float forwardInput, turnInput, jumpInput;

	public Quaternion TargetRotation {
		get { return targetRotation; }
	}
	
	public AnimatorSettings animatorSettings = new AnimatorSettings();
	public MoveSettings moveSettings = new MoveSettings();
	public PhysicSettings physicsSettings = new PhysicSettings();
	public InputSettings inputSettings = new InputSettings();
	public Checkpoint lastCheckpoint;

	bool Grounded() {
		return Physics.Raycast(transform.position, Vector3.down, moveSettings.distanceToGrounded, moveSettings.ground);
	}

	void Awake() {
		if (GetComponent<Rigidbody>()) {
			rBody = GetComponent<Rigidbody>();
		}
		else {
			Debug.LogError("The character needs a rigidbody");
		}

		forwardInput = turnInput = jumpInput = 0;
		rBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

		animatorSettings.animator = GetComponent<Animator>();
	}

	void Start() {
		Init();
	}

	void GetInput() {
		forwardInput = Input.GetAxis(inputSettings.FORWARD_AXIS);
		turnInput = Input.GetAxis(inputSettings.TURN_AXIS);
		jumpInput = Input.GetAxisRaw(inputSettings.JUMP_AXIS);
	}

	void Update() {
		GetInput();
		Turn();
	}

	void FixedUpdate() {
		Run();
		Jump();

		rBody.velocity = transform.TransformDirection(velocity);
	}

	void Run() {
		if (Mathf.Abs(forwardInput) > inputSettings.inputDelay) {
			// move
			velocity.z = moveSettings.forwardVelocity * forwardInput;
			animatorSettings.animator.SetFloat("Forward", forwardInput, 0.1f, Time.deltaTime);
		} else {
			// zero velocity
			velocity.z = 0;
			// animatorSettings.animator.ApplyBuiltinRootMotion();
		}
	}

	void Turn() {
		if (Mathf.Abs(turnInput) > inputSettings.inputDelay) {
			targetRotation *= Quaternion.AngleAxis(moveSettings.rotateVelocity * turnInput * Time.deltaTime, Vector3.up);
			transform.rotation = targetRotation;
			animatorSettings.animator.SetFloat("Turn", turnInput, 0.1f, Time.deltaTime);

		}
	}

	void Jump() {
		if (jumpInput > 0 && Grounded()) {
			// jump!
			velocity.y = moveSettings.jumpVelocity;
		} else if (jumpInput == 0 && Grounded()) {
			// zero out velocity.y
			velocity.y =0;
		} else {
			// decrease velocity.y
			velocity.y -= physicsSettings.downAcceleration;
		}
	}

	void Init() {
		targetRotation = transform.rotation;
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
			Debug.Log("YOU DIE!");
			Spawn();
		} else if (other.tag == "CheckPoint") {
			Debug.Log("YOU GOT A CHECKPOINT");
			Checkpoint checkpoint = other.gameObject.GetComponent<Checkpoint>();
			CollideCheckpoint(checkpoint);
		} else if (other.tag == "EndPoint") {
			Checkpoint checkpoint = other.gameObject.GetComponent<Checkpoint>();
			Debug.Log("YOU FINISHED THE LEVEL!");
			CollideCheckpoint(checkpoint);
		}
	}

	void CollideCheckpoint(Checkpoint checkpoint) {
		// Set out last checkpoint to checkpoint
		lastCheckpoint = checkpoint;

		// Collect checkpoint
		lastCheckpoint.Collect();
	}
}
