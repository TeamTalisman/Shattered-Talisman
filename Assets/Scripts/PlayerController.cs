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

	Vector3 velocity = Vector3.zero;
	Quaternion targetRotation;
	Rigidbody rBody;
	float forwardInput, turnInput, jumpInput;

	public Quaternion TargetRotation {
		get { return targetRotation; }
	}

	public MoveSettings moveSettings = new MoveSettings();
	public PhysicSettings physicsSettings = new PhysicSettings();

	public InputSettings inputSettings = new InputSettings();

	bool Grounded() {
		return Physics.Raycast(transform.position, Vector3.down, moveSettings.distanceToGrounded, moveSettings.ground);
	}

	void Start() {
		targetRotation = transform.rotation;

		if (GetComponent<Rigidbody>()) {
			rBody = GetComponent<Rigidbody>();
		}
		else {
			Debug.LogError("The character needs a rigidbody");
		}

		forwardInput = turnInput = jumpInput = 0;
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
		} else {
			// zero velocity
			velocity.z = 0;
		}
	}

	void Turn() {
		if (Mathf.Abs(turnInput) > inputSettings.inputDelay) {
			targetRotation *= Quaternion.AngleAxis(moveSettings.rotateVelocity * turnInput * Time.deltaTime, Vector3.up);
			transform.rotation = targetRotation;
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
}
