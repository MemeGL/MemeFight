using System.Collections;
using UnityEngine;
using MemeFight.Components.Utilities.ObjectPooling;

namespace MemeFight.Components
{
	[RequireComponent(typeof(PoolableObject))]
	public class Projectile : MonoBehaviour {

		protected float m_speed;
		protected float m_lifetime;
		protected bool m_isEnabled = false;
		protected bool m_isExpired = false;
		/// <summary>
		/// The velocity of this projectile, expressed as a <seealso cref="Vector3"/>.
		/// <br />
		/// However, the z-value of this velocity value will be disregarded.
		/// </summary>
		protected Vector3 m_velocity;
		protected PoolableObject m_poolableObjectComponent;

		protected virtual void Awake() {
			m_poolableObjectComponent = GetComponent<PoolableObject>();
		}

		protected virtual void Update() {
			if (m_poolableObjectComponent.IsActiveInScene) {
				if (m_isExpired) {
					RemoveSelfFromScene();
				} else if (m_isEnabled) {
					transform.position += m_velocity * m_speed * Time.deltaTime;
				}

				if (m_lifetime <= 0) {
					m_isExpired = true;
				} else {
					m_lifetime -= Time.deltaTime;
				}
			}
		}

		public virtual void Fire(Vector3 startingPosition, float lifetime, float speed, ref Vector3 velocity) {
			ResetToInitialState();
			transform.position = startingPosition;
			m_lifetime = lifetime;
			m_speed = speed;
			velocity.z = 0;
			m_velocity = velocity.normalized;
			SpawnSelfInScene();
		}

		protected virtual void SpawnSelfInScene() {
			m_isEnabled = true;
			m_poolableObjectComponent.IsActiveInScene = true;
		}

		protected virtual void RemoveSelfFromScene() {
			StartCoroutine(ReturnToObjectPoolCoroutine());
		}

		protected virtual void ResetToInitialState() {
			m_isEnabled = false;
			m_isExpired = false;
		}

		protected IEnumerator ReturnToObjectPoolCoroutine() {
			yield return new WaitForEndOfFrame();
			m_poolableObjectComponent.IsActiveInScene = false;
		}

		protected virtual void OnCollisionEnter2D(Collision2D collision) {
		}
		protected virtual void OnCollisionStay2D(Collision2D collision) {
		}
		protected virtual void OnCollisionExit2D(Collision2D collision) {
		}
		protected virtual void OnTriggerEnter2D(Collider2D collision) {
		}
		protected virtual void OnTriggerStay2D(Collider2D collision) {
		}
		protected virtual void OnTriggerExit2D(Collider2D collision) {
		}

	}
}
