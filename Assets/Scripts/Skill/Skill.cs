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
	/// <param name="args"></param>
	public abstract IEnumerator TriggerSkillCoroutine(params object[] args);

	protected virtual void OnEnable() {
		m_canTrigger = true;
	}

}
