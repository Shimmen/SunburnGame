using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour {

	Animator anim;
	public float restartDelay = 4.5f;
	private float restartTimer = 0.0f;
	public SunburnBehaviour sunburnBehaviour;
	public LevelWinZoneBehaviour winZone;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (sunburnBehaviour != null && sunburnBehaviour.gameOver) {
			anim.SetTrigger ("GameOver");

			restartTimer += Time.deltaTime;

			if (restartTimer >= restartDelay) {

				SceneManager.LoadScene ("Level1");
			}

		} else {
			
			if (winZone != null && winZone.levelWon) {
				anim.SetTrigger ("GameWon");
				GetComponentInChildren<Text>().text = "You won!";
				restartTimer += Time.deltaTime;
				if (restartTimer >= 8.5f) {
					SceneManager.LoadScene ("Level1");
				}
			}

		}
	}
}
