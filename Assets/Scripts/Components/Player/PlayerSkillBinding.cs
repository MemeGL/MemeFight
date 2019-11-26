using UnityEngine;
using MemeFight.Skills;

namespace MemeFight.Components
{
	namespace Player
	{
		public class PlayerSkillBinding : MonoBehaviour {

			[SerializeField]
			PlayerSkillRepository m_playerSkillRepository;

			/// <summary>
			/// The 0th index is reserved for the <seealso cref="Empty"/> <seealso cref="Skill"/>.
			/// </summary>
			const int SKILL_INDEX_EMPTY = 0;

			[SerializeField]
			int m_skillIndexPrimary = 0;

			[SerializeField]
			int m_skillIndexSecondary = 0;

			private void Awake() {
				m_playerSkillRepository.GetSkillAt(m_skillIndexPrimary)?.AssignCaster(gameObject);
				m_playerSkillRepository.GetSkillAt(m_skillIndexSecondary)?.AssignCaster(gameObject);
			}

			public virtual void TriggerPrimarySkill(params object[] args) {
				StartCoroutine(m_playerSkillRepository.GetSkillAt(m_skillIndexPrimary)?.TriggerSkillCoroutine(Stage.Stage.CurrentStageInstance.CurrentBossObject, args));
			}

			public virtual void TriggerSecondarySkill(params object[] args) {
				StartCoroutine(m_playerSkillRepository.GetSkillAt(m_skillIndexSecondary)?.TriggerSkillCoroutine(Stage.Stage.CurrentStageInstance.CurrentBossObject, args));
			}

			public virtual void AssignPrimarySkill(int skillIndex) {
				m_skillIndexPrimary = skillIndex;
			}

			public virtual void AssignSecondarySkill(int skillIndex) {
				m_skillIndexSecondary = skillIndex;
			}

			public virtual void ResetAllSkillBindings() {
				AssignPrimarySkill(SKILL_INDEX_EMPTY);
				AssignSecondarySkill(SKILL_INDEX_EMPTY);
			}

		}
	}
}
