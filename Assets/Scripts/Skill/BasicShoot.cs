using System;
using System.Collections;
using UnityEngine;
using MemeFight.Components;
using MemeFight.Components.Utilities.ObjectPooling;

[CreateAssetMenu(fileName = "BasicShoot", menuName = "Skill/BasicShoot", order = 3)]
public class BasicShoot : MemeFight.Skills.Skill {

	[SerializeField]
	int m_objectPoolIndex;
	[SerializeField]
	float m_bulletSpeed;
	[SerializeField]
	float m_bulletLifetime;

	/// <summary>
	/// Fires a basic projectile towards the mouse position.
	/// </summary>
	/// <param name="casterGameObject">See <seealso cref="Skill.TriggerSkillCoroutine(GameObject, GameObject, object[])"/>.</param>
	/// <param name="targetGameObject">See <seealso cref="Skill.TriggerSkillCoroutine(GameObject, GameObject, object[])"/>.</param>
	/// <param name="args">Expects only a Vector3 representing the input mouse position.</param>
	/// <returns></returns>
	public override IEnumerator TriggerSkillCoroutine(GameObject casterGameObject, GameObject targetGameObject, params object[] args) {
		if (m_canTrigger) {
			m_canTrigger = false;

			try {
				Vector3 inputMousePosition = (Vector3) args[0];
				inputMousePosition.z = casterGameObject.transform.position.z - Camera.main.transform.position.z;
				Vector3 projectileDirection = Camera.main.ScreenToWorldPoint(inputMousePosition) - casterGameObject.transform.position;
				ObjectPoolManager.Instance.GetObjectPoolAt(m_objectPoolIndex).NextAvailableObject?.GetComponent<Projectile>()?.Fire(casterGameObject.transform.position, projectileDirection * m_bulletSpeed, m_bulletLifetime);
			} catch (InvalidCastException e) {
				Debug.LogError($"Expected Vector3 but encountered {args[0]}:\n\n{e.Message}.", this);
			}

			yield return new WaitForSeconds(m_cooldown);
			m_canTrigger = true;
		} else {
			yield return null;
		}
	}

}
