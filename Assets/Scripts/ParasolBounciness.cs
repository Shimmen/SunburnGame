using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParasolBounciness : MonoBehaviour {

	[Range(1.0f, 25.0f)]
	public float bouncePower;

	[Range(0.1f, 2.0f)]
	public float cooldownTime;

	public enum BounceDirection { Normal, Reflected };
	public BounceDirection bounceDirection;

	private AudioSource audioSource;
	private PlatformerCharacter2DCustom characterStuff;
	private float cooldown;

	void Start() {
		characterStuff = GameObject.Find ("Player").GetComponent<PlatformerCharacter2DCustom> ();
		audioSource = GetComponent<AudioSource>();
		cooldown = 0.0f;
	}

	void Update() {
		cooldown -= Time.deltaTime;
	}

	void OnTriggerStay2D(Collider2D other) {

		Vector2 N = new Vector2(transform.up.x, transform.up.y);
		N.Normalize ();

		Vector2 currentVelocity = other.attachedRigidbody.velocity;

		Vector3 D = new Vector2(other.attachedRigidbody.velocity.x, other.attachedRigidbody.velocity.y);
		D.Normalize ();

		// If moving with or close to parasol normal, abort
		// (we only want to bounce if there is an obvious attempt)
		if (Vector2.Dot (D, N) >= 0.5f) {
			return;
		}

		if (cooldown <= 0) {
			
			Vector2 bounceDir = (bounceDirection == BounceDirection.Reflected)
				? Vector2.Reflect (D, N)
				: N;

			// Remove any current velocity in the direction of the bounce (so we get consistent height/power for the bounce)
			Vector3 velocity = Vector3.ProjectOnPlane (currentVelocity, N);
			other.attachedRigidbody.velocity = velocity;

			// Remove air controll for the player when it bounces so it maintains a direction
			characterStuff.SetAirControlEnabled(false);

			other.attachedRigidbody.AddForce (bounceDir * bouncePower, ForceMode2D.Impulse);
			audioSource.Play();

			cooldown = cooldownTime;
		}

	}
}

