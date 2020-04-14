using UnityEngine;

public class PlayerInput : MonoBehaviour {

    [System.NonSerialized]
    public Player player;

    private void Update() {
        player.controller.moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
    }
}
