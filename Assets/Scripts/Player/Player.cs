using UnityEngine;

public class Player : MonoBehaviour {

	public new PlayerCamera camera;
	public PlayerController controller;
	public PlayerInput input;

	private void Awake() {
		camera.player = this;
		controller.player = this;
		input.player = this;
	}

}

