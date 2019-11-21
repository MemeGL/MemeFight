using UnityEngine;

public class PlayerSkillBinding : MonoBehaviour {

	/// <summary>
	/// The 0th index is reserved for the <seealso cref="Empty"/> <seealso cref="Skill"/>.
	/// </summary>
	const int SKILL_INDEX_EMPTY = 0;

	[SerializeField]
	int m_skillIndexPrimary = 0;

	[SerializeField]
	int m_skillIndexSecondary = 0;

	public void TriggerPrimarySkill() {
		StartCoroutine(PlayerSkillRepository.Instance.GetSkillAt(m_skillIndexPrimary)?.TriggerSkillCoroutine(Input.mousePosition.x, Input.mousePosition.y));
	}

	public void TriggerSecondarySkill() {
		StartCoroutine(PlayerSkillRepository.Instance.GetSkillAt(m_skillIndexSecondary)?.TriggerSkillCoroutine(Input.mousePosition.x, Input.mousePosition.y));
	}

	public void AssignPrimarySkill(int skillIndex) {
		m_skillIndexPrimary = skillIndex;
	}

	public void AssignSecondarySkill(int skillIndex) {
		m_skillIndexSecondary = skillIndex;
	}

	public void ResetAllSkillBindings() {
		AssignPrimarySkill(SKILL_INDEX_EMPTY);
		AssignSecondarySkill(SKILL_INDEX_EMPTY);
	}

}
