using UnityEngine;

namespace MemeFight.Skills
{	
	/// <summary>
	/// The asset data responsible for storing all implemented player <seealso cref="Skill"/>s.
	/// </summary>
	[CreateAssetMenu(fileName = "PlayerSkillRepository", menuName = "Skill Repository/PlayerSkillRepository", order = 1)]
	public sealed class PlayerSkillRepository : ScriptableObject {

		/// <summary>
		/// The 0th <seealso cref="Skill"/> is reserved for "empty skill".
		/// </summary>
		[SerializeField]
		[Tooltip("The 0th Skill is reserved for the Empty Skill.")]
		Skill[] playerSkills;

		public bool HasSkillAt(int index) {
			return -1 < index && index < playerSkills.Length;
		}

		public Skill GetSkillAt(int index) {
			if (HasSkillAt(index)) {
				return playerSkills[index];
			} else {
				Debug.LogError("Attempted to access a skill at invalid index.", this);
				return null;
			}
		}

	}
}