using System.Collections;
using UnityEngine;

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
