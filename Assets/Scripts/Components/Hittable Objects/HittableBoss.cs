using UnityEngine;
using MemeFight.Stage;

namespace MemeFight.Components
{
	namespace Hittables
	{
		/// <summary>
		/// The <seealso cref="Component"/> to be attached to a boss <seealso cref="GameObject"/> to make it a <seealso cref="HittableObject"/>.
		/// </summary>
		public class HittableBoss : HittableObject {

			public override void Hit() {
				throw new System.NotImplementedException();
			}

			protected override void Die() {
				GetComponent<StageEntity>().InvokeOnDeathCallbacks();
			}

		}
	}
}
