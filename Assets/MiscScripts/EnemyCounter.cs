using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCounter : MonoBehaviour {
	public static EnemyCounter eC;

	public List<Enemy> allEnemies = new List<Enemy> ();
	void Awake () {
		eC = GetComponent<EnemyCounter> ();
	}

	public void AlertNearbyEnemies (Enemy initiator) {
		foreach (Enemy e in allEnemies) {
			if (Vector3.Distance(initiator.transform.position, e.transform.position) < 5) {
				e.ExternalDetection ();
			}
		}
	}

}
