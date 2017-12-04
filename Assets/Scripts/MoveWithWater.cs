using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithWater : MonoBehaviour {

	private float yOffset;
	private GameObject water;
	private float t;
	public float internalSineAmplitude = 0.03f;
	public float internalSineMultiplier = 6.86f;

	// Use this for initialization
	void Start () {
		water = GameObject.Find ("Water");
		yOffset = transform.position.y - water.transform.position.y;
		t = Random.Range (-Mathf.PI, Mathf.PI);
	}
	
	// Update is called once per frame
	void Update () {
		t += Time.deltaTime;
		float internalSine = internalSineAmplitude * Mathf.Sin (t * internalSineMultiplier);
		float y = water.transform.position.y + yOffset + internalSine;
		transform.position = new Vector3 (transform.position.x, y, transform.position.z);
	}
}
