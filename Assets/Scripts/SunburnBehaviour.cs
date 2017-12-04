using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SunburnBehaviour : MonoBehaviour {

	public bool gameOver = false;

	public SunSensorLogic[] sunSensors;
	private float totalExposureTime = 0.0f;

	public Gradient gradient;
	public Material playerMaterial;

	public Text burnProgressLabel;
	public float lethalExposure = 100.0f;

	// TODO: Maybe make this part of the pickup instead so that it can very between pickups!
	public float pickupEffect = 25.0f;

	public float deadnessRequiredForEmission = 0.5f;
	public int minParticleCap = 0;
	public int maxParticleCap = 1000;

	public ParticleSystem particleSystem;

	private AudioSource butterAudioSource;
	private AudioSource lotionAudioSource;
	private GameObject splashObject;

	public float deadnessRequiredForSound = 0.5f;

	private Animator m_Anim;

	private void setSmokeParticleCap(int cap) {
		//var em = particleSystem.emission.rate;
		//em.mode = ParticleSystemCurveMode.Constant;
		//em.constantMax = emission;
		//em.constantMin = emission;
		//em.constant = emission;
		//particleSystem.main.maxParticles = cap;
		ParticleSystem.MainModule main = particleSystem.main;
		main.maxParticles = cap;
		//particleSystem.main = main;
	}

	private float targetVolume = 0.0f;
	private float volumeVelocity = 0.0f;

	public float volumeTransitionTime = 0.1f;

	public Image burnednessFill;

	void Start () {
		butterAudioSource = GetComponents<AudioSource>()[0];
		lotionAudioSource = GetComponents<AudioSource>()[1];
		splashObject = (GameObject)Resources.Load("SunLotionPickupSplash");
		m_Anim = GetComponentInChildren<Animator>();
	}

	void Update () {

		// Calculate current total exposure
		int numInSun = 0;
		foreach (SunSensorLogic sensor in sunSensors) {
			if (sensor.IsExposedToSun ()) {
				numInSun += 1;
			}
		}
		float fractionInSun = (float)numInSun / (float)sunSensors.Length;
		totalExposureTime += fractionInSun * Time.deltaTime;

		if (numInSun == 0) {
			totalExposureTime = Mathf.Max (0.0f, totalExposureTime - Time.deltaTime);
			burnProgressLabel.color = Color.green;
		} else {
			burnProgressLabel.color = Color.red;
		}

		float deadness = totalExposureTime / lethalExposure;
		if (deadness >= 1) {
			if (!gameOver) {
				burnProgressLabel.text = "You are dead!";
				gameOver = true;
				m_Anim.SetTrigger ("Die");
			}
		} else {
			//int percent = Mathf.RoundToInt (deadness * 100.0f);
			//burnProgressLabel.text = percent + " %";
			burnednessFill.fillAmount = deadness;
			burnednessFill.color = gradient.Evaluate (deadness);
		}

		// Animate player color depending on exposure
		playerMaterial.color = gradient.Evaluate (deadness);

		bool inSun = numInSun > 0;

		if (inSun && deadness >= deadnessRequiredForEmission) {
			float emissionFactor = (deadness - deadnessRequiredForEmission) / (1.0f - deadnessRequiredForEmission);
			int emission = Mathf.RoundToInt(minParticleCap + emissionFactor * (maxParticleCap - minParticleCap));
			setSmokeParticleCap (emission);
		} else {
			setSmokeParticleCap (0);
		}


		if (inSun && deadness >= deadnessRequiredForSound) {
			float soundFactor = (deadness - deadnessRequiredForSound) / (1.0f - deadnessRequiredForSound);
			targetVolume = Mathf.Clamp01(soundFactor) * 0.65f;
		} else {
			targetVolume = 0.0f;
		}

		butterAudioSource.volume = Mathf.SmoothDamp (butterAudioSource.volume, targetVolume, ref volumeVelocity, volumeTransitionTime);

	}

	void OnTriggerEnter2D(Collider2D other) {

		// On pickup
		if (other.gameObject.layer == LayerMask.NameToLayer("Pickup")) {
			totalExposureTime = Mathf.Max (0.0f, totalExposureTime - pickupEffect);
			Destroy (other.gameObject);
			lotionAudioSource.Play();
			Instantiate (splashObject, transform.position, Quaternion.identity);
		}
	}
}
