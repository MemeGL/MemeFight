using UnityEngine;

/// <summary>
/// The <seealso cref="Component"/> to be attached to a player <seealso cref="GameObject"/> to make it a <seealso cref="HittableObject"/>.
/// </summary>
public class HittablePlayer : HittableObject {

	public override void Hit() {
		throw new System.NotImplementedException();
	}

	protected override void Die() {
		GetComponent<StageEntity>().InvokeOnDeathCallbacks();
	}

}
