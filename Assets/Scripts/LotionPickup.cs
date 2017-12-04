using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotionPickup : MonoBehaviour {

	[Range(0.5f, 40.0f)]
	public float bobSpeed = 2.0f;
	[Range(0.0f, 1.0f)]
	public float bobAmplitude = 0.35f;
	[Range(0.0f, 1.0f)]
	public float rotateSpeed;

	private float initialY;

	void Start () {
		initialY = transform.position.y;
	}

	void Update () {

		float bobAmount = Mathf.Sin (Time.time  / (2.0f * Mathf.PI) * bobSpeed) * bobAmplitude;
		Vector3 pos = transform.position;
		transform.position = new Vector3 (pos.x, initialY + bobAmount, pos.z);

		transform.Rotate (Vector3.up * Time.deltaTime * 360.0f * rotateSpeed);
	}

}
