using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinOSinemating : MonoBehaviour {

	[Range(0.01f, 2.0f)]
	public float speed;

	private Vector3 initialPosition;

	void Start () {
		initialPosition = transform.position;
	}

	void Update () {

		float amp = 17.5f;
		float fa = Mathf.Sin(Time.time * Mathf.PI * 2.0f * speed);
		float fb = Mathf.Sin(fa * 13.0f);
		transform.position = initialPosition + new Vector3 (fa * amp + fb * (amp / 8.0f), 0.0f, 0.0f);

	}

}
