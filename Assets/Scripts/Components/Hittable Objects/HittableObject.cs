using UnityEngine;
using MemeFight.Stage;

namespace MemeFight.Components
{
	namespace Hittables
	{
		/// <summary>
		/// A base definition of an object that can trigger an interaction when struck by something in the game.
		/// </summary>
		[RequireComponent(typeof(StageEntity))]
		public abstract class HittableObject : MonoBehaviour {

			public abstract void Hit();

			protected abstract void Die();

		}
	}
}