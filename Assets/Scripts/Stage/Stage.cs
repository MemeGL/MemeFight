using UnityEngine;
using MemeFight.Constants;

/// <summary>
/// Acts as a manager for the current active boss fight.
/// </summary>
public class Stage : MonoBehaviour {

	protected bool m_hasStageEnded = false;
	
	public virtual void Awake() {
		// If the player dies, he loses the stage.
		StageEntity player = GameObject.Find(Global.NAME_OBJECT_PLAYER)?.GetComponent<StageEntity>();
		if (player == null) {
			throw new UnityException($"No player of name '{Global.NAME_OBJECT_PLAYER}' or its corresponding StageEntity component detected.");
		} else {
			player.AddOnDeathCallback(Lose);
		}

		// If the boss dies, player wins the stage.
		StageEntity boss = GameObject.Find(Global.NAME_OBJECT_BOSS)?.GetComponent<StageEntity>();
		if (boss == null) {
			throw new UnityException($"No boss of name '{Global.NAME_OBJECT_BOSS}' or its corresponding StageEntity component detected.");
		} else {
			boss.AddOnDeathCallback(Win);
		}
	}

	public virtual void Begin() {
		m_hasStageEnded = false;
		print("Boss-fight stage has now begun.");
		//SetParticipantsEnabledState(true);
	}

	// Reserved for boss-battles that use a timer
	// and reached timed-out state.
	public virtual void End() {
		if (!m_hasStageEnded) {
			m_hasStageEnded = true;
			print("Boss-fight stage has ended with no victor/loser.");
			//SetParticipantsEnabledState(false);
		}
	}

	public virtual void Win() {
		if (!m_hasStageEnded) {
			m_hasStageEnded = true;
			print("Player has won.");
			//SetParticipantsEnabledState(false);
		}
	}

	public virtual void Lose() {
		if (!m_hasStageEnded) {
			m_hasStageEnded = true;
			print("Player has lost.");
			//SetParticipantsEnabledState(false);
		}
	}

	// Placeholder mechanism for when game breakpoints are reached.
	// TODO: Remove if not required.
	public virtual void SetParticipantsEnabledState(bool isActive) {
		GameObject.Find(Global.NAME_OBJECT_PLAYER)?.SetActive(isActive);
		GameObject.Find(Global.NAME_OBJECT_BOSS)?.SetActive(isActive);
	}
}
