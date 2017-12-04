using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelWinZoneBehaviour : MonoBehaviour {

	public bool levelWon = false;

	void OnTriggerEnter2D(Collider2D other) {
		levelWon = true;
	}

}
