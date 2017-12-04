using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeInOverlayBehaviour : MonoBehaviour {

	public float cameraFadeInTime;

	private Image overlayImage;
	private float currentFadeTime;

	void Start () {
		
		overlayImage = GetComponent<Image>();
		overlayImage.color = new Color(0, 0, 0, 1);

		currentFadeTime = 0.0f;
	}

	void Update () {
		
		// Exit if escape is pressed
		if (Input.GetKey ("escape")) {
			currentFadeTime -= Time.deltaTime;
			if (currentFadeTime < 0.0f) {
				Debug.Log ("Quit game!");
				Application.Quit ();
				if (currentFadeTime < -0.2f) {
					SceneManager.LoadScene ("Level1"); // If the application cannot quit (e.g. in webgl), restart the game instead
				}
			}
		} else {
			
			currentFadeTime += Time.deltaTime;
			currentFadeTime = Mathf.Clamp(currentFadeTime, 0.0f, cameraFadeInTime);
		}

		float alpha = 1.0f - Mathf.InverseLerp (0.0f, cameraFadeInTime, currentFadeTime);

		Color curr = overlayImage.color;
		overlayImage.color = new Color (curr.r, curr.g, curr.b, alpha);
		
	}
}
