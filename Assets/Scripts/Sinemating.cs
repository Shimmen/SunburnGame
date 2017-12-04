using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sinemating : MonoBehaviour {

	public Vector2 amplitude;
	public Vector2 frequency;
	public Vector2 offset;

	private Vector3 initialPosition;

	void Start () {
		initialPosition = transform.position;
	}

	void Update () {

		Vector2 f = frequency * Mathf.PI * 2.0f * Time.time;
		Vector2 o = offset * Mathf.PI * 2.0f;
		Vector2 posOffset = new Vector2(Mathf.Sin (f.x + o.x), Mathf.Sin(f.y + o.y));
		transform.position = initialPosition + new Vector3(posOffset.x * amplitude.x, 0.0f, posOffset.y * amplitude.y);

	}
}
