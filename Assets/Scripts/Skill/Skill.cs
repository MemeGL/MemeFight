using System.Collections;
using UnityEngine;

namespace MemeFight.Skills
{
	/// <summary>
	/// This class lays the base framework for all subclasses of <see cref="Skill"/>s.
	/// </summary>
	public abstract class Skill : ScriptableObject {

		/// <summary>
		/// The <seealso cref="GameObject"/> of this <see cref="Skill"/>'s caster.
		/// </summary>
		[Header("Caster GameObject")]
		[SerializeField]
		protected GameObject m_casterGameObject;

		[Header("Default Skill attributes")]
		[SerializeField]
		protected float m_duration;
		[SerializeField]
		protected float m_cooldown;

		protected bool m_canTrigger = true;

		/// <summary>
		/// A skill may consist of a sequence of events.
		/// This coroutine holds the sequence of events to be performed on this <see cref="Skill"/>.
		/// <br />
		/// This method is made `abstract` for freedom of custom <see cref="Skill"/> implementation.
		/// </summary>
		/// <param name="targetGameObject">
		/// The <seealso cref="GameObject"/> of the target entity (NOT the <see cref="Skill.m_casterGameObject"/> itself) that this <see cref="Skill"/> should act on.
		/// <br />
		/// Typical use cases involve Player --> Boss and Boss --> Player.
		/// <br />
		/// Additional targets information may be passed through the <paramref name="args"/> parameter.
		/// </param>
		/// <param name="args">Any additional information to be supplied to this <see cref="Skill"/> method.</param>
		/// <returns></returns>
		public abstract IEnumerator TriggerSkillCoroutine(GameObject targetGameObject, params object[] args);

		protected virtual void OnEnable() {
			m_canTrigger = true;
		}

		public virtual void AssignCaster(GameObject caster) {
			m_casterGameObject = caster;
		}

	}
}
