using UnityEngine;

/// <summary>
/// A generic component script that can be attached to any GameObject
/// that should stay persistent in the game.
/// </summary>
public class PersistentObject : MonoBehaviour {

	private void Awake() {
		DontDestroyOnLoad(gameObject);
	}

}
