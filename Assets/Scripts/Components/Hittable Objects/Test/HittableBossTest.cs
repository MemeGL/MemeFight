using System.Collections;
using UnityEngine;
using MemeFight.Components.Hittables;

public class HittableBossTest : HittableBoss {

	public float timeBeforeDeath;

	public void Test() {
		StartCoroutine(TestCoroutine());
	}

	IEnumerator TestCoroutine() {
		yield return new WaitForSeconds(timeBeforeDeath);
		base.Die();
		Destroy(gameObject);
	}

}
