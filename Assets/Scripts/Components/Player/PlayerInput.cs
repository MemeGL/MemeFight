using UnityEngine;
using MemeFight.Constants;

namespace MemeFight.Components
{
	namespace Player
	{
		[RequireComponent(typeof(PlayerMovement))]
		public class PlayerInput : MonoBehaviour {

			private PlayerMovement m_playerMovement;

			private bool m_isPressingUp = false;
			private bool m_isPressingDown = false;

			private void Awake() {
				m_playerMovement = GetComponent<PlayerMovement>();
			}

			private void Update() {
				if (Input.GetAxisRaw(InputAxes.VERTICAL) > InputAxes.INPUT_AXIS_VALUE_AFFIRMATION_POSITIVE) {
					if (!m_isPressingUp) {
						m_isPressingUp = true;
					}

					m_playerMovement.Jump();
				} else if (m_isPressingUp) {
					m_isPressingUp = false;
					m_playerMovement.StopChargingJump();
					m_playerMovement.EnableJump();
				}

				if (Input.GetAxisRaw(InputAxes.VERTICAL) < InputAxes.INPUT_AXIS_VALUE_AFFIRMATION_NEGATIVE && !m_isPressingDown) {
					m_isPressingDown = true;
					m_playerMovement.PhaseThroughPlatforms();
				} else if (Input.GetAxisRaw(InputAxes.VERTICAL) > -0.5f) {
					m_isPressingDown = false;
				}

				if (Input.GetAxisRaw(InputAxes.HORIZONTAL) < InputAxes.INPUT_AXIS_VALUE_AFFIRMATION_NEGATIVE) {
					m_playerMovement.MoveLeft();
				} else if (Input.GetAxisRaw(InputAxes.HORIZONTAL) > InputAxes.INPUT_AXIS_VALUE_AFFIRMATION_POSITIVE) {
					m_playerMovement.MoveRight();
				} else {
					m_playerMovement.StopMoving();
				}

				if (Input.GetButtonDown(InputAxes.MOUSE_BUTTON_LEFT)) {
					GetComponent<PlayerSkillBinding>().TriggerPrimarySkill(Input.mousePosition);
				}

				if (Input.GetButtonDown(InputAxes.MOUSE_BUTTON_RIGHT)) {
					GetComponent<PlayerSkillBinding>().TriggerSecondarySkill(Input.mousePosition);
				}
			}

		}
	}
}