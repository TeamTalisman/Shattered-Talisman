using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// https://www.youtube.com/watch?v=MkbovxhwM4I&index=4&list=PL4CCSwmU04MjDqjY_gdroxHe85Ex5Q6Dy
public class CameraController : MonoBehaviour {
	public Transform target;
	public float lookSmooth = 0.09f;
	public Vector3 offsetFromTarget = new Vector3(0, 6, -8);
	public float xTilt = 10;
	Vector3 destination = Vector3.zero;
	PlayerController playerController;
	float rotateVelocity = 0;

	// Use this for initialization
	void Start () {
		SetCameraTarget(target);
	}

	public void SetCameraTarget(Transform t) {
		target = t;

		if ( target != null ) {
			if (target.GetComponent<PlayerController>()) {
				playerController = target.GetComponent<PlayerController>();
			} else {
				Debug.LogError("The camera's target needs a PlayerController script!");
			}
		} else {
			Debug.LogError("Camera needs a target!");
		}
	}

	void LateUpdate() {
		// move & rotate
		MoveToTarget();
		LookAtTarget();
	}

	void MoveToTarget() {
		destination = playerController.TargetRotation * offsetFromTarget;
		destination += target.position;
		transform.position = destination;
	}

	void LookAtTarget() {
		float eulerYAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target.eulerAngles.y, ref rotateVelocity, lookSmooth);
		transform.rotation = Quaternion.Euler(transform.eulerAngles.x, eulerYAngle, 0);
	}
}
