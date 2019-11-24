using UnityEngine;

namespace MemeFight.Components
{
	namespace Utilities.ObjectPooling
	{
		/// <summary>
		/// Defines an object that can be object-pooled by the <seealso cref="ObjectPool"/> component.
		/// </summary>
		[RequireComponent(typeof(SpriteRenderer))]
		public class PoolableObject : MonoBehaviour {

			/// <summary>
			/// Indicates to its <see cref="ObjectPool"/> whether this <see cref="PoolableObject"/> is active in the scene.
			/// </summary>
			public bool IsActiveInScene {
				get;
				set;
			}

			protected virtual void Awake() {
				IsActiveInScene = false;
			}

			protected virtual void Update() {
				if (IsActiveInScene && !GetComponent<SpriteRenderer>().enabled) {
					GetComponent<SpriteRenderer>().enabled = true;
				} else if (!IsActiveInScene && GetComponent<SpriteRenderer>().enabled) {
					GetComponent<SpriteRenderer>().enabled = false;
				}
			}

		}
	}
}
