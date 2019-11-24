using UnityEngine;

namespace MemeFight.Stage
{
	/// <summary>
	/// The component responsible for communication between the
	/// <seealso cref="Stage"/> and this <seealso cref="GameObject"/>.
	/// <br /><br />
	/// This component is named <see cref="StageEntity"/> because
	/// it is perceived to be the representation of the game entity by
	/// the <seealso cref="Stage"/>. 
	/// </summary>
	public class StageEntity : MonoBehaviour {

		public delegate void OnDeathCallback();

		protected OnDeathCallback m_onDeathCallbacks;

		public virtual void AddOnDeathCallback(OnDeathCallback callback) {
			m_onDeathCallbacks += callback;
		}

		public virtual void RemoveOnDeathCallback(OnDeathCallback callback) {
			m_onDeathCallbacks -= callback;
		}

		public virtual void InvokeOnDeathCallbacks() {
			m_onDeathCallbacks();
		}

	}
}