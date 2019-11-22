using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Empty", menuName = "Skill/Empty", order = 2)]
public class Empty : Skill {
	public override IEnumerator TriggerSkillCoroutine(GameObject caster, GameObject target, params object[] args) {
		yield return null;
	}
}
