using UnityEngine;

namespace MemeFight.Components
{
	namespace Utilities
	{
		/// <summary>
		/// A generic component script that can be attached to any GameObject
		/// that should stay persistent in the game.
		/// </summary>
		public class PersistentObject : MonoBehaviour {

			private void Awake() {
				DontDestroyOnLoad(gameObject);
			}

		}
	}
}