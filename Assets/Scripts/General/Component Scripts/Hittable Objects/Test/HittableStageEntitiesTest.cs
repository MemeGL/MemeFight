using System;
using UnityEngine;

/// <summary>
/// Used for testing the <seealso cref="Stage"/> handler logic.
/// </summary>
public class HittableStageEntitiesTest : MonoBehaviour {

	[Header("Test Case Setup")]
	public Stage stage;

	public HittableBossTest testBossStageEntity;
	public float timeBeforeBossDies;

	public HittablePlayerTest testPlayerEntity;
	public float timeBeforePlayerDies;

	enum EntityDeathOrder {
		Random, Player_First, Boss_First
	}

	[Header("Corner Case Handling")]
	[Tooltip("Determines how the test should execute for the case where player and boss dies at same time.")]
	[SerializeField]
	EntityDeathOrder entityDeathOrder;	

    // Start is called before the first frame update
    void Start() {
		if (testBossStageEntity == null || testPlayerEntity == null) {
			throw new UnityException("Required test entities are absent.");
		}

		stage.Begin();

		testBossStageEntity.timeBeforeDeath = timeBeforeBossDies;
		testPlayerEntity.timeBeforeDeath = timeBeforePlayerDies;

		if (timeBeforeBossDies == timeBeforePlayerDies) {
			switch (entityDeathOrder) {
				case EntityDeathOrder.Random:
					var option = UnityEngine.Random.Range(0, 2);
					if (option == 1) {
						print("Testing for case where boss dies first...");
						testBossStageEntity.Test();
						testPlayerEntity.Test();
					} else {
						print("Testing for case where player dies first...");
						testPlayerEntity.Test();
						testBossStageEntity.Test();
					}
					break;
				case EntityDeathOrder.Player_First:
					print("Testing for current test case...");
					testPlayerEntity.Test();
					testBossStageEntity.Test();
					break;
				case EntityDeathOrder.Boss_First:
					print("Testing for current test case...");
					testBossStageEntity.Test();
					testPlayerEntity.Test();
					break;
				default:
					throw new NotSupportedException("Invalid HittableStageEntitiesTest test case.");
			}		
		} else {
			print("Testing for current test case...");
			testPlayerEntity.Test();
			testBossStageEntity.Test();
		}		
    }

	private void Update() {
		if (Input.GetKey(KeyCode.R)) {
			UnityEngine.SceneManagement.SceneManager.LoadScene(
				UnityEngine.SceneManagement.SceneManager.GetActiveScene().name,
				UnityEngine.SceneManagement.LoadSceneMode.Single);
		}
	}
}
