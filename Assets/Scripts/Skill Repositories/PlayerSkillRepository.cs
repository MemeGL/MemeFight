using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillRepository", menuName = "Skill Repository/PlayerSkillRepository", order = 1)]
public sealed class PlayerSkillRepository : ScriptableObject {

	public static PlayerSkillRepository Instance {
		get;
		private set;
	}

	/// <summary>
	/// The 0th <seealso cref="Skill"/> is reserved for "empty skill".
	/// </summary>
	[SerializeField]
	[Tooltip("The 0th Skill is reserved for the Empty Skill.")]
	Skill[] playerSkills;

	private void OnEnable() {
		if (Instance == null) {
			Instance = this;
		} else {
			Destroy(this);
		}
	}

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
