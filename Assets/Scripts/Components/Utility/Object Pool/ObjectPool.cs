using System.Collections.Generic;
using UnityEngine;

namespace MemeFight.Components
{
	namespace Utilities.ObjectPooling
	{
		/// <summary>
		/// The component responsible for handling the object-pooling of a specified <seealso cref="GameObject"/>.
		/// </summary>
		public class ObjectPool : MonoBehaviour {

			[SerializeField]
			protected int m_objectPoolSize;
			[SerializeField]
			protected GameObject m_objectToPool;

			protected List<PoolableObject> m_poolableObjectList = new List<PoolableObject>();

			/// <summary>
			/// Currently performs self-registering to the <seealso cref="ObjectPoolManager"/>, with the assumption that
			/// this component is a child of the <seealso cref="ObjectPoolManager"/>.
			/// <br />
			/// Inspect the code body of this method for more details.
			/// </summary>
			protected virtual void Awake() {
				GetComponentInParent<ObjectPoolManager>().RegisterObjectPool(this);
				// Alternatively, if you do not want to nest ObjectPool objects as children of an ObjectPoolManager,
				// you may register your ObjectPool to the ObjectPoolManager using this line of code.
				// This provides more freedom to where ObjectPools can be placed in the scene.
				// ObjectPoolManager.Instance.RegisterObjectPool(this);

				for (var i = 0; i < m_objectPoolSize; i++) {
					m_poolableObjectList.Add(Instantiate(m_objectToPool, transform).GetComponent<PoolableObject>());
				}
			}

			public virtual GameObject NextAvailableObject => m_poolableObjectList.Find(obj => !obj.IsActiveInScene)?.gameObject;

		}
	}
}
