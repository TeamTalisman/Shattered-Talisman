using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public Transform target;

	[System.SerializableAttribute]
	public class PositionSettings {
		public Vector3 targetPositionOffset = new Vector3(0, 3.4f, 0);
		public float lookSmooth = 100f;
		public float distanceFromTarget = -8f;
		public float zoomSmooth = 2; //2
		public float zoomStep = 2;
		public float maxZoom = -2;
		public float minZoom = -15;

		public bool smoothFollow = true;
		public float smooth = 0.05f;

		[HideInInspector]
		public float newDistance = -8;
		[HideInInspector]
		public float adjustmentDistance = -8;
	}

	[System.SerializableAttribute]
	public class OrbitSettings {
		public float xRotation = -20f;
		public float yRotation = -180f;
		public float maxXRotation = 25;
		public float minXRotation = -50;
		public float vOrbitSmooth = 0.5f;
		public float hOrbitSmooth = 0.5f;
	}

	[System.SerializableAttribute]
	public class InputSettings {
		public string MOUSE_ORBIT = "MouseOrbit";
		public string MOUSE_ORBIT_VERTICAL = "MouseOrbitVertical";
		public string ORBIT_HORIZONTAL_SNAP = "OrbitHorizontalSnap";
		public string ORBIT_HORIZONTAL = "OrbitHorizontal";
		public string ORBIT_VERTICAL = "OrbitVertical";
		public string ZOOM = "Mouse ScrollWheel";
	}

	[System.SerializableAttribute]
	public class DebugSettings {
		public bool drawDesiredCollisionLines = true;
		public bool drawAdjustedCollisionLines = true;
	}

	public PositionSettings position = new PositionSettings();
	public OrbitSettings orbit = new OrbitSettings();
	public InputSettings input = new InputSettings();
	public DebugSettings debug = new DebugSettings();
	public CollisionHandler collision = new CollisionHandler();

	Vector3 targetPosition = Vector3.zero;
	Vector3 destination = Vector3.zero;
	Vector3 adjustedDestination = Vector3.zero;
	Vector3 cameraVelocity = Vector3.zero;
	Vector3 previousMousePosition = Vector3.zero;
	Vector3 currentMousePosition = Vector3.zero;
	PlayerController playerController;
	float vOrbitInput, hOrbitInput, zoomInput, hOrbitSnapInput, mouseOrbitInput, vMouseOrbitInput;
	


	// Use this for initialization
	void Start () {
		SetCameraTarget(target);
		vOrbitInput = hOrbitInput = zoomInput = hOrbitSnapInput = mouseOrbitInput = vMouseOrbitInput = 0;
		MoveToTarget();
		collision.Initialize(Camera.main);
		collision.UpdateCameraClipPoints(transform.position, transform.rotation, ref collision.adjustedCameraClipPoints);
		collision.UpdateCameraClipPoints(transform.position, transform.rotation, ref collision.desiredCameraClipPoints);
		previousMousePosition = currentMousePosition = Input.mousePosition;
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

	public void GetInput() {
		// vOrbitInput = Input.GetAxisRaw(input.ORBIT_VERTICAL);
		// hOrbitInput = Input.GetAxisRaw(input.ORBIT_HORIZONTAL);
		// hOrbitSnapInput = Input.GetAxisRaw(input.ORBIT_HORIZONTAL_SNAP);
		zoomInput = Input.GetAxisRaw(input.ZOOM);
		// mouseOrbitInput = Input.GetAxisRaw(input.MOUSE_ORBIT);
		// vMouseOrbitInput = Input.GetAxisRaw(input.MOUSE_ORBIT_VERTICAL);
	}

	void Update() {
		GetInput();
		ZoomInOnTarget();
	}

	void FixedUpdate() {
		// move & rotate
		MoveToTarget();
		LookAtTarget();
		OrbitTarget();
		// MouseOrbitTarget();
		collision.UpdateCameraClipPoints(transform.position, transform.rotation, ref collision.adjustedCameraClipPoints);
		collision.UpdateCameraClipPoints(transform.position, transform.rotation, ref collision.desiredCameraClipPoints);

		if (debug.drawAdjustedCollisionLines || debug.drawDesiredCollisionLines) {
			// draw debug lines
			for (int i = 0; i < 5; i++) {
				if (debug.drawDesiredCollisionLines) {
					Debug.DrawLine(targetPosition, collision.desiredCameraClipPoints[i], Color.white);
				}

				if (debug.drawAdjustedCollisionLines) {
					Debug.DrawLine(targetPosition, collision.adjustedCameraClipPoints[i], Color.green);
				}
			}
		}

		collision.CheckColliding(targetPosition); // Using raycasts here
		position.adjustmentDistance = collision.GetAdjustedDistanceWithRay(targetPosition);
	}

	void MoveToTarget() {
		targetPosition = target.position + 
											Vector3.up * position.targetPositionOffset.y + 
											Vector3.forward * position.targetPositionOffset.z + 
											transform.TransformDirection(Vector3.right * position.targetPositionOffset.x);
		//
		destination = Quaternion.Euler(orbit.xRotation, orbit.yRotation + target.eulerAngles.y, 0) * 
									-Vector3.forward * position.distanceFromTarget;

		destination += target.position;
		
		if (collision.colliding) {
			adjustedDestination = Quaternion.Euler(orbit.xRotation, orbit.yRotation + target.eulerAngles.y, 0) * 
										Vector3.forward * position.adjustmentDistance;
			adjustedDestination += targetPosition;

			if (position.smoothFollow) {
				// use smooth damp follow
				transform.position = Vector3.SmoothDamp(transform.position, adjustedDestination, ref cameraVelocity, position.smooth);
			} else {
				transform.position = adjustedDestination;
			}
		} else {
				if (position.smoothFollow) {
					// use smooth damp follow
					transform.position = Vector3.SmoothDamp(transform.position, destination, ref cameraVelocity, position.smooth);
				} else {
					transform.position = destination;
				}
		}
	}

	void LookAtTarget() {
		Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, position.lookSmooth * Time.deltaTime);
	}

	void OrbitTarget() {
		if (hOrbitSnapInput > 0) {
			orbit.yRotation = -180f;
		}

		orbit.xRotation += -vOrbitInput * orbit.vOrbitSmooth * Time.deltaTime;
		orbit.yRotation += -hOrbitInput * orbit.hOrbitSmooth * Time.deltaTime;

		if (orbit.xRotation > orbit.maxXRotation) {
			orbit.xRotation = orbit.maxXRotation;
		}

		if (orbit.xRotation < orbit.minXRotation) {
			orbit.xRotation = orbit.minXRotation;
		}
	}

	void MouseOrbitTarget() {

	}

	void ZoomInOnTarget() {
		position.distanceFromTarget += zoomInput * position.zoomSmooth * Time.deltaTime;

		if (position.distanceFromTarget > position.maxZoom) {
			position.distanceFromTarget = position.maxZoom;
		}

		if (position.distanceFromTarget < position.minZoom) {
			position.distanceFromTarget = position.minZoom;
		}
	}

	[System.SerializableAttribute]
	public class CollisionHandler {
		public LayerMask collisionLayer;

		[HideInInspector]
		public bool colliding = false;

		[HideInInspector]
		public Vector3[] adjustedCameraClipPoints;

		[HideInInspector]
		public Vector3[] desiredCameraClipPoints;

		public float CAMERA_COLLISION_SPACE = 3.41f;
		Camera camera;

		public void Initialize(Camera cam) {
			camera = cam;
			adjustedCameraClipPoints = new Vector3[5];
			desiredCameraClipPoints = new Vector3[5];
		}
		public void UpdateCameraClipPoints(Vector3 cameraPosition, Quaternion atRotation, ref Vector3[] intoArray) {
			if (!camera)
				return;

			// Clear contents of intoArray
			intoArray = new Vector3[5];

			float z = camera.nearClipPlane;
			float x = Mathf.Tan(camera.fieldOfView / CAMERA_COLLISION_SPACE) * z;
			float y = x / camera.aspect;

			// top left
			intoArray[0] = (atRotation * new Vector3(-x, y, z)) + cameraPosition; // added and rotate the point relative to camera

			// top right
			intoArray[1] = (atRotation * new Vector3(x, y, z)) + cameraPosition; // added and rotate the point relative to camera

			// bottom left
			intoArray[2] = (atRotation * new Vector3(-x, -y, z)) + cameraPosition; // added and rotate the point relative to camera

			// bottom right
			intoArray[3] = (atRotation * new Vector3(x, -y, z)) + cameraPosition; // added and rotate the point relative to camera

			// camera's position
			intoArray[4] = cameraPosition - camera.transform.forward;
		}

		bool CollisionDetectedAtClipPoints(Vector3[] clipPoints, Vector3 fromPosition) {
			for (int i = 0; i < clipPoints.Length; i++) {
				Ray ray = new Ray(fromPosition, clipPoints[i] - fromPosition);
				float distance = Vector3.Distance(clipPoints[i], fromPosition);

				if (Physics.Raycast(ray, distance, collisionLayer)) {
					return true;
				}
			}

			return false;
		}
		
		public float GetAdjustedDistanceWithRay(Vector3 from) {
			float distance = -1f;

			for (int i = 0; i< desiredCameraClipPoints.Length; i++) {
				Ray ray = new Ray(from, desiredCameraClipPoints[i] - from);
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit)) {
					if (distance == -1) {
						distance = hit.distance;
					} else {
						if (hit.distance < distance) {
							distance = hit.distance;
						}
					}
				}
			}

			if (distance == -1) {
				return 0;
			} else {
				return distance;
			}
		}

		public void CheckColliding(Vector3 targetPosition) {
			if (CollisionDetectedAtClipPoints(desiredCameraClipPoints, targetPosition)) {
				 colliding = true;
			} else {
				colliding = false;
			}
		}
	}
}
