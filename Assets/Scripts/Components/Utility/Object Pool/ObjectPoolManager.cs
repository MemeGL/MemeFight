using System.Collections.Generic;
using UnityEngine;

namespace MemeFight.Components
{
	namespace Utilities.ObjectPooling
	{
		/// <summary>
		/// A component that manages multiple <seealso cref="ObjectPool"/>s, if they are needed in the scene.
		/// <br />
		/// Ideally, all <seealso cref="ObjectPool"/>s should be a child of the <see cref="ObjectPoolManager"/> for the scene setup.
		/// (See <seealso cref="ObjectPool.Awake"/>)
		/// </summary>
		public class ObjectPoolManager : MonoBehaviour {

			public static ObjectPoolManager Instance;
			protected static List<ObjectPool> m_objectPools = new List<Utilities.ObjectPooling.ObjectPool>();

			protected virtual void Awake() {
				Instance = this;
			}

			public virtual ObjectPool GetObjectPoolAt(int index) {
				if (index < 0) {
					Debug.LogError("Invalid index for accessing target ObjectPool.", this);
					return null;
				} else if (index >= m_objectPools.Count) {
					Debug.LogError("Index exceeds number of ObjectPools present.", this);
					return null;
				} else {
					return m_objectPools[index];
				}
			}

			public virtual void RegisterObjectPool(ObjectPool objectPool) {
				if (m_objectPools.Contains(objectPool)) {
					Debug.LogWarning($"ObjectPool from {objectPool.gameObject.name} has already been registered.", this);
				} else {
					m_objectPools.Add(objectPool);
				}
			}

			public virtual void DeregisterObjectPool(ObjectPool objectPool) {
				if (m_objectPools.Contains(objectPool)) {
					m_objectPools.Remove(objectPool);
				} else {
					Debug.LogWarning($"ObjectPool from {objectPool.gameObject.name} has already been deregistered.", this);
				}
			}

		}
	}
}