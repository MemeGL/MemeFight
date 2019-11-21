using System;
using System.Collections;
using UnityEngine;
using MemeFight.Constants;

[CreateAssetMenu(fileName = "Dash", menuName = "Skill/Dash", order = 2)]
public class Dash : Skill {

	[SerializeField]
	float m_dashMagnitude;
	[SerializeField]
	GameObject m_dashEffect;
	[SerializeField]
	float m_skillInactiveTrailTime;
	[SerializeField]
	float m_skillActiveTrailTime;

	float m_timeElapsed;

	/// <summary>
	/// 
	/// </summary>
	/// <param name="args">
	/// Expects arguments in this order:
	/// 1. The mouse input's X value,
	/// 2. The mouse input's Y value.
	/// </param>
	/// <returns></returns>
	public override IEnumerator TriggerSkillCoroutine(params object[] args) {
		if (m_canTrigger) {
			m_canTrigger = false;

			// Locate the player object first
			UnitMovement playerUnitMovement = GameObject.Find(Global.NAME_OBJECT_PLAYER)?.GetComponent<UnitMovement>();
			Vector2? playerCurrentVelocity = playerUnitMovement?.m_velocity;

			if (playerCurrentVelocity == null) {
				Debug.LogError("Player object not located in present scene, or required UnitMovement component is missing on player object.", this);
				yield return null;
			} else {
				Vector2 dashVelocityNormalized = ((Vector2) playerCurrentVelocity).normalized;
				PreDashSetup(playerUnitMovement.gameObject, dashVelocityNormalized);

				// Apply the dash velocity on the player on every frame where the dash effect is active.
				while (m_timeElapsed <= m_duration) {
					m_timeElapsed += Time.deltaTime;
					playerUnitMovement.m_velocity = dashVelocityNormalized * m_dashMagnitude;
					playerUnitMovement.m_isPhasingThroughPlatforms = true;
					yield return null;
				}

				if (m_cooldown < m_duration) {
					throw new UnityException("Dash skill cooldown cannot be lower than its duration.");
				}
				playerUnitMovement.m_velocity = (Vector2) playerCurrentVelocity;
				PostDashSetup(playerUnitMovement.gameObject);
				yield return new WaitForSeconds(m_cooldown - m_duration);
				m_canTrigger = true;
			}
		} else {
			yield return null;
		}
	}

	protected override void OnEnable() {
		GameObject playerObject = GameObject.Find(Global.NAME_OBJECT_PLAYER);

		if (playerObject == null) {
			Debug.LogError("Player object not located in present scene.", this);
		} else {
			try {
				playerObject.GetComponent<TrailRenderer>().time = m_skillInactiveTrailTime;
			} catch (MissingComponentException e) {
				Debug.LogError($"Missing required component on player GameObject:\n\n{e.Message}", this);
			} catch (Exception e) {
				Debug.LogError($"Exception in Dash.OnEnable():\n\n{e.Message}", this);
			}
		}
		base.OnEnable();
	}

	/// <summary>
	/// Performs the following tasks:
	/// <br />
	/// 1. Disables the player's input so that the dash effect can be applied smoothly.
	/// <br />
	/// 2. Enables visual effects to indicate that the dash effect is active.
	/// <br />
	/// 3. Resets the time-elapsed tracking variable.
	/// </summary>
	/// <param name="playerObject">The player's <seealso cref="GameObject"/>.</param>
	/// <param name="currentVelocityNormalized">The player's velocity at the present moment of time, to create the dash particle effect.</param>
	void PreDashSetup(in GameObject playerObject, in Vector2 currentVelocityNormalized) {
		try {
			playerObject.GetComponent<PlayerInput>().enabled = false;
			playerObject.GetComponent<TrailRenderer>().time = m_skillActiveTrailTime;
			CreateEffect(playerObject.transform.position, currentVelocityNormalized);
			m_timeElapsed = 0;
		} catch (MissingComponentException e) {
			Debug.LogError($"Missing required component on player GameObject:\n\n{e.Message}", this);
		} catch (Exception e) {
			Debug.LogError($"Exception in Dash.PreDashSetup(GameObject, Vector2):\n\n{e.Message}", this);
		}
	}

	/// <summary>
	/// Performs the following tasks:
	/// <br />
	/// 1. Removes the dash skill's visual effects.
	/// <br />
	/// 2. Re-enables the player input to allow movement control after the dash effect expires.
	/// </summary>
	/// <param name="playerObject"></param>
	void PostDashSetup(in GameObject playerObject) {
		try {
			playerObject.GetComponent<TrailRenderer>().time = m_skillInactiveTrailTime;
			playerObject.GetComponent<PlayerInput>().enabled = true;
		} catch (MissingComponentException e) {
			Debug.LogError($"Missing required component on player GameObject:\n\n{e.Message}", this);
		} catch (Exception e) {
			Debug.LogError($"Exception in Dash.PostDashSetup(GameObject):\n\n{e.Message}", this);
		}
	}

	void CreateEffect(Vector2 position, Vector2 velocityNormalized) {
		Destroy(Instantiate(m_dashEffect, new Vector3(position.x, position.y, 0), Quaternion.Euler(0, 0, Mathf.Atan2(velocityNormalized.y, velocityNormalized.x) * Mathf.Rad2Deg)), m_dashEffect.GetComponent<ParticleSystem>().main.duration);
	}

}
