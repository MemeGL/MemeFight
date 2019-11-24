using System.Collections;
using UnityEngine;

/// <summary>
/// The Empty representation of a <seealso cref="MemeFight.Skills.Skill"/>.
/// </summary>
[CreateAssetMenu(fileName = "Empty", menuName = "Skill/Empty", order = 2)]
public class Empty : MemeFight.Skills.Skill {

	public override IEnumerator TriggerSkillCoroutine(GameObject caster, GameObject target, params object[] args) {
		yield return null;
	}

}
