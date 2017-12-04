using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (SphereCollider))]
public class SunSensorLogic : MonoBehaviour {

	private bool inSun = false;

	void OnTriggerEnter(Collider other) {
		inSun = false;
	}

	void OnTriggerExit(Collider other) {
		inSun = true;
	}

	public bool IsExposedToSun() {
		return inSun;
	}

}
