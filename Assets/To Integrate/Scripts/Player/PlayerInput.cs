using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour {

    private PlayerMovement m_playerMovement;

    private bool m_isPressingUp = false;
    private bool m_isPressingDown = false;

    private void Awake() {
        m_playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update() {
        if (Input.GetAxisRaw(InputAxes.VERTICAL) > 0.5f) {
            if (!m_isPressingUp) {
                m_isPressingUp = true;
            }
            m_playerMovement.Jump();
        } else if (m_isPressingUp) {
            m_isPressingUp = false;
            m_playerMovement.StopChargingJump();
            m_playerMovement.SetCanJump();
        }
        if (Input.GetAxisRaw(InputAxes.VERTICAL) < -0.5f && !m_isPressingDown) {
            m_isPressingDown = true;
            m_playerMovement.PhaseThroughPlatforms();
        } else if (Input.GetAxisRaw(InputAxes.VERTICAL) > -0.5f) {
            m_isPressingDown = false;
        }
        if (Input.GetAxisRaw(InputAxes.HORIZONTAL) < -0.5f) {
            m_playerMovement.MoveLeft();
        } else if (Input.GetAxisRaw(InputAxes.HORIZONTAL) > 0.5f) {
            m_playerMovement.MoveRight();
        } else {
            m_playerMovement.StopMoving();
        }
    }

}