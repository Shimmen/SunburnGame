using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class FollowXAxis : MonoBehaviour {

	public Transform target;
	public bool keepCurrentOffset = true;

	private float xOffset;

	void Start () {
		if (keepCurrentOffset) {
			xOffset = transform.position.x - target.position.x;
		} else {
			xOffset = 0.0f;
		}

		MoveToPosition ();
	}
	
	void Update () {
		MoveToPosition ();
	}

	private void MoveToPosition() {
		Vector3 pos = this.transform.position;
		float targetX = target.position.x;
		transform.position = new Vector3 (targetX + xOffset, pos.y, pos.z);
	}

}
