using System.Collections;
using UnityEngine;
using MemeFight.Components.Utilities.ObjectPooling;

namespace MemeFight.Components
{
	public class Projectile : MonoBehaviour {

        [SerializeField]
        private LayerMask m_layerMask;
        [SerializeField]
        private float m_gravityMultiplier;

        private Vector2 m_velocity;
        protected float m_lifetime;

		protected virtual void Awake() {
            Fire(transform.position, Vector2.down, 100);
		}

		protected virtual void Update() {
			if (m_lifetime <= 0) {
                OnExpiry();
            } else {
				m_lifetime -= Time.deltaTime;
            }
            m_velocity += Physics2D.gravity * m_gravityMultiplier * Time.deltaTime;
        }

        private void FixedUpdate() {
            transform.position += (Vector3)m_velocity * Time.fixedDeltaTime;
        }

		public virtual void Fire(Vector3 startPosition, Vector2 velocity, float lifetime) {
			transform.position = startPosition;
			m_lifetime = lifetime;
            m_velocity = velocity;
		}

		protected virtual void OnExpiry() {
            Destroy(gameObject);
		}

		protected virtual void OnCollisionEnter2D(Collision2D collision) { }
		protected virtual void OnCollisionStay2D(Collision2D collision) { }
		protected virtual void OnCollisionExit2D(Collision2D collision) { }
		protected virtual void OnTriggerEnter2D(Collider2D collision) {
            if (m_layerMask == (m_layerMask | (1 << collision.gameObject.layer))) {
                OnExpiry();
            }
        }
		protected virtual void OnTriggerStay2D(Collider2D collision) { }
		protected virtual void OnTriggerExit2D(Collider2D collision) { }

	}
}