using UnityEngine;

public class PlayerController : MonoBehaviour {

	[System.NonSerialized]
	public Player player;

	public float speed;
	public CharacterController controller;
	public Transform characterForwardHelper;
	public Transform aimTarget;
	public Vector3 moveDirection;

	// The maximum angular offset of the aiming direction from the character forward. Character will be rotated to comply.
	[Range(0f, 180f)]
	public float maxAngle;

	public float mouseSensitivity;

	// Limit these to 89 degrees because if we look straight up or down the IK
	// goes crazy. There may be ways to avoid this, but this seems like an easy
	// fix.
	// TODO: Using the current animation rigging setup the IK still goes crazy.
	[Range(-89f, 0f)]
	public float yMinLimit;
	[Range(0f, 89f)]
	public float yMaxLimit;

	private float _x, _y;

	private void Start() {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	private void Update() {
		if (controller.isGrounded) {
			// We are grounded, so recalculate move direction directly from axes
			moveDirection = characterForwardHelper.TransformDirection(moveDirection);
			moveDirection = moveDirection.normalized * speed;
		}

		// Apply gravity
		moveDirection.y += Physics.gravity.y * Time.deltaTime;

		// Move the controller
		controller.Move(moveDirection * Time.deltaTime);

		// Rotating the root of the character if it is past maxAngle from the camera forward
		RotateCharacter();
	}

	private void LateUpdate() {
		_x += Input.GetAxis("Mouse X") * mouseSensitivity;
		_y = ClampAngle(_y - Input.GetAxis("Mouse Y") * mouseSensitivity, yMinLimit, yMaxLimit);

		player.camera.transform.position = player.camera.pivot.position;
		player.camera.transform.rotation = Quaternion.AngleAxis(_x, Vector3.up) * Quaternion.AngleAxis(_y, Vector3.right);
		aimTarget.position = player.camera.transform.position + player.camera.transform.forward * 5;

		// Rotate our character forward helper which we use to base our input
		// on so that we WASD move us relative to the camera rotation without
		// the entire character following it.
		// TODO: Determine if this is convoluted or not and if there's a better way to solve it?
		characterForwardHelper.rotation = Quaternion.LookRotation(new Vector3(player.camera.transform.forward.x, 0f, player.camera.transform.forward.z));
	}

	/// <summary>
	/// Rotating the root of the character if it is past maxAngle from the camera forward.
	/// </summary>
	private void RotateCharacter() {
		if (maxAngle >= 180f) {
			return;
		}

		// If no angular difference is allowed, just rotate the character to the flattened camera forward.
		if (maxAngle <= 0f) {
			transform.rotation = Quaternion.LookRotation(new Vector3(player.camera.transform.forward.x, 0f, player.camera.transform.forward.z));
			return;
		}

		// Get camera forward in the character's rotation space.
		Vector3 camRelative = transform.InverseTransformDirection(player.camera.transform.forward);

		// Get the angle of the camera forward relative to the character forward.
		float angle = Mathf.Atan2(camRelative.x, camRelative.z) * Mathf.Rad2Deg;

		// Making sure the angle does not exceed maxAngle.
		if (Mathf.Abs(angle) > Mathf.Abs(maxAngle)) {
			float a = angle - maxAngle;
			if (angle < 0f) a = angle + maxAngle;
			transform.rotation = Quaternion.AngleAxis(a, transform.up) * transform.rotation;
		}
	}

	private static float ClampAngle (float angle, float min, float max) {
		if (angle < -360F) {
			angle += 360F;
		}
		if (angle > 360F) {
			angle -= 360F;
		}
		return Mathf.Clamp(angle, min, max);
	}
}

