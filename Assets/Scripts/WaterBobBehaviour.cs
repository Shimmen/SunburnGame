using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBobBehaviour : MonoBehaviour {

	[Range(0.0f, 0.5f)]
	public float bobFrequency;

	[Range(0.0f, 0.5f)]
	public float bobAmplitude;

	private float initialY;

	void Start () {
		initialY = transform.position.y;
	}

	void Update () {

		float bob = bobAmplitude * Mathf.Sin(Time.time * Mathf.PI * 2.0f * bobFrequency);
		transform.position = new Vector3(transform.position.x, initialY + bob, transform.position.z);
		
	}
}
