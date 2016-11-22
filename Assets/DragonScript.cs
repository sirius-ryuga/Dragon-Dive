using UnityEngine;
using System.Collections;

public class DragonScript : MonoBehaviour {

	private Bounds cameraBounds; 
	private float dirY = -1;
	// Use this for initialization
	void Start () {
		GameObject cameraBoundary = GameObject.Find ("CameraBoundary");
		cameraBounds = cameraBoundary.GetComponent<MeshCollider> ().bounds;
	}
	
	// Update is called once per frame

	void Update () {
		// Player positioning.
		Vector3 dir = Vector3.zero;
		dir.x = Input.acceleration.x * 0.1f;

		float x = transform.position.x, y = transform.position.y, z = transform.position.z;
		x += dir.x;
		y += Time.deltaTime * 3f * dirY;

		if (x < -5f) {
			x = - 5f;
		}
		else if (x > 5f) {
			x = 5f;
		}
		if (y < -22.5f) {
			y = -22.5f;
		}

		transform.position = new Vector3(x, y, z);


		// Camera positioning.
		Camera mainCamera = Camera.main;
		float camX = transform.position.x, camY = transform.position.y, camZ = mainCamera.transform.position.z;
		float camVertExtent = mainCamera.orthographicSize;
		float camHorizExtent = mainCamera.aspect * camVertExtent;

		float leftBound = cameraBounds.min.x + camHorizExtent, rightBound = cameraBounds.max.x - camHorizExtent;
		float bottomBound = cameraBounds.min.y + camVertExtent, topBound = cameraBounds.max.y - camVertExtent;

		camX = Mathf.Clamp (camX, leftBound, rightBound);
		camY = Mathf.Clamp (camY, bottomBound, topBound);

		mainCamera.transform.position = new Vector3 (camX, camY, camZ);
	}

	void OnTriggerEnter (Collider other) {

		if (other.name == "Pearl") {
			dirY = 1;
			Quaternion r = transform.rotation;
			r.z = 180f;
			transform.rotation = r;
			other.enabled = false;
			other.gameObject.transform.parent = transform;
			other.gameObject.transform.localPosition = new Vector3 (0, -1, 1);
		}
	}

}

