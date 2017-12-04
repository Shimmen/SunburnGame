using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


[RequireComponent(typeof (PlatformerCharacter2DCustom))]
public class Platformer2DUserControlCustom : MonoBehaviour
{
	private PlatformerCharacter2DCustom m_Character;
	private bool m_Jump;

	private Animator m_Anim; 

	private SunburnBehaviour sunburnBehaviour;
	public LevelWinZoneBehaviour winZone;

	public PhysicsMaterial2D pm;

	private void Awake()
	{
		m_Character = GetComponent<PlatformerCharacter2DCustom>();
		m_Anim = GetComponentInChildren<Animator>();
		sunburnBehaviour = GetComponent<SunburnBehaviour>();
	}


	private void Update()
	{
		if (!m_Jump)
		{
			// Read the jump input in Update so button presses aren't missed.
			m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
		}
	}


	private void FixedUpdate()
	{
		if (!sunburnBehaviour.gameOver && !winZone.levelWon) {
			// Read the inputs.
			bool crouch = Input.GetKey(KeyCode.LeftControl);
			float h = CrossPlatformInputManager.GetAxis("Horizontal");

			/*if (h == 0f) {
				pm.friction = 0.4f;
			} else {
				pm.friction = 0.0f;
			}*/

			// Pass all parameters to the character control script.
			m_Character.Move(h, crouch, m_Jump);
			m_Jump = false;

			// If we are on the ground and air control happens to be disabled, enable it again
			// (air control is disabled when bouncing on a parasol!)
			if (m_Character.IsGrounded ()) {
				m_Character.SetAirControlEnabled (true);
			}
		} else {
			// Stop moving & disable air control so it doesn't stop in the air
			m_Character.SetAirControlEnabled(false);
			m_Character.Move(0.0f, false, false);
		}
	}
}
