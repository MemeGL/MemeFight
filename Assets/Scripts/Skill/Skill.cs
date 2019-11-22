using System.Collections;
using UnityEngine;

public abstract class Skill : ScriptableObject {

	protected bool m_canTrigger = true;

	[SerializeField]
	protected float m_duration;
	[SerializeField]
	protected float m_cooldown;

	/// <summary>
	/// A skill may consist of a sequence of events.
	/// This coroutine holds the sequence of events to be performed on this <see cref="Skill"/>.
	/// </summary>
	/// <param name="casterGameObject">
	/// The <seealso cref="GameObject"/> of the entity that casted this <see cref="Skill"/>.
	/// Positive buff <see cref="Skill"/>s should use this reference <seealso cref="GameObject"/> for skill effect application.
	/// </param>
	/// <param name="targetGameObject">
	/// The <seealso cref="GameObject"/> of the target entity (NOT the caster itself) that this <see cref="Skill"/> should act on.
	/// <br />
	/// Typical use cases involve Player --> Boss and Boss --> Player.
	/// <br />
	/// Additional targets information may be passed through the <paramref name="args"/> parameter.
	/// </param>
	/// <param name="args">Any additional information to be supplied to this <see cref="Skill"/> method.</param>
	/// <returns></returns>
	public abstract IEnumerator TriggerSkillCoroutine(GameObject casterGameObject, GameObject targetGameObject, params object[] args);

	protected virtual void Awake() {
		m_canTrigger = true;
	}

}
