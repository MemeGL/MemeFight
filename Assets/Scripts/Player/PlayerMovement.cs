using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitMovement))]
public class PlayerMovement : MonoBehaviour {

    private const float PHASE_THROUGH_PLATFORMS_DURATION = 0.05f;

    [SerializeField]
    private float m_movementSpeed;
    [SerializeField]
    private float m_acceleration;
    [SerializeField]
    private float m_jumpSpeed;
    [SerializeField]
    private float m_maxJumpHoldDuration;

    private UnitMovement m_unitMovement;

    private bool m_canJump = false;
    private bool m_isChargingJump = false;
    private float m_jumpChargeDuration = 0;
    private float m_phaseThroughPlatformsDuration = 0;

    private void Awake() {
        m_unitMovement = GetComponent<UnitMovement>();
    }

    private void Update() {
        m_unitMovement.m_velocity += Vector2.up * GlobalConstants.gravity * Time.deltaTime;
        if (m_unitMovement.m_isGrounded) {
            if (m_jumpChargeDuration > 0) {
                ResetJumpCharge();
            }
        }
        

        m_unitMovement.m_isPhasingThroughPlatforms = m_phaseThroughPlatformsDuration > 0;
        m_phaseThroughPlatformsDuration -= Time.deltaTime;
    }

    public void MoveLeft() {
        if (m_unitMovement.m_isGrounded && m_unitMovement.m_velocity.x > 0) {
            m_unitMovement.m_velocity.x = 0;
        }
        m_unitMovement.m_velocity.x = Mathf.MoveTowards(m_unitMovement.m_velocity.x, -m_movementSpeed, m_acceleration * Time.deltaTime);
    }

    public void MoveRight() {
        if (m_unitMovement.m_isGrounded && m_unitMovement.m_velocity.x < 0) {
            m_unitMovement.m_velocity.x = 0;
        }
        m_unitMovement.m_velocity.x = Mathf.MoveTowards(m_unitMovement.m_velocity.x, m_movementSpeed, m_acceleration * Time.deltaTime);
    }
    public void StopMoving() {
        m_unitMovement.m_velocity.x = Mathf.MoveTowards(m_unitMovement.m_velocity.x, 0, m_acceleration * Time.deltaTime);
    }

    public void Jump() {
        if (m_unitMovement.m_isGrounded || (m_jumpChargeDuration < m_maxJumpHoldDuration && m_isChargingJump)) {
            if (m_unitMovement.m_isGrounded == true && m_canJump) {
                m_unitMovement.m_isGrounded = false;
                m_isChargingJump = true;
                m_canJump = false;
            }
            m_unitMovement.m_velocity.y = m_jumpSpeed;
            m_jumpChargeDuration += Time.deltaTime;
        }
    }

    public void SetCanJump() {
        m_canJump = true;
    }

    public void StopChargingJump() {
        m_isChargingJump = false;
    }

    public void ResetJumpCharge() {
        StopChargingJump();
        m_jumpChargeDuration = 0;
    }

    public void PhaseThroughPlatforms() {
        if (m_unitMovement.m_isGrounded) {
            m_phaseThroughPlatformsDuration = PHASE_THROUGH_PLATFORMS_DURATION;
        }
    }
}