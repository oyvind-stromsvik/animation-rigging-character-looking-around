using UnityEngine;

public class PlayerCamera : MonoBehaviour {

	[System.NonSerialized]
	public Player player;

	public Transform pivot;

	public Transform hipBone;
	public Transform spineBone;
	public Transform chestBone;
	public Transform upperChestBone;
	public Transform neckBone;
	public Transform headBone;

	private const float DebugRayLength = 5;

	private void Update() {
		// Just debug some things.
		Debug.DrawRay(hipBone.position, hipBone.forward * DebugRayLength, new Color(0, 1, 1));
		Debug.DrawRay(spineBone.position, spineBone.forward * DebugRayLength, new Color(0, 5/6f, 1));
		Debug.DrawRay(chestBone.position, chestBone.forward * DebugRayLength, new Color(0, 4/6f, 1));
		Debug.DrawRay(upperChestBone.position, upperChestBone.forward * DebugRayLength, new Color(0, 3/6f, 1));
		Debug.DrawRay(neckBone.position, neckBone.forward * DebugRayLength, new Color(0, 2/6f, 1));
		Debug.DrawRay(headBone.position, headBone.forward * DebugRayLength, new Color(0, 1/6f, 1));
		Debug.DrawRay(transform.position, transform.forward * DebugRayLength, new Color(0, 0, 1));
	}
}
