using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyGenerator : MonoBehaviour {

	public GameObject baseEnemy;
	public GameObject player;
	List<GameObject> currentEnemies;
	private float lastPlacementTime;
	// Use this for initialization
	void Start () {
		currentEnemies = new List<GameObject>();
		lastPlacementTime = Time.realtimeSinceStartup;
	}

	void Update () {
		if (ShouldPlaceNewEnemy ()) {
			PlaceNewEnemy ();
		}
	}

	private bool ShouldPlaceNewEnemy() {
		if (Time.realtimeSinceStartup - lastPlacementTime > 10) {
			lastPlacementTime = Time.realtimeSinceStartup;
			return true;
		}
		return false;
	}

	public void PlaceNewEnemy() {
		GameObject newEnemy = Instantiate(baseEnemy);
		EnemyController ec = newEnemy.GetComponent<EnemyController> ();
		ec.Initialize (2, new Vector3 (10, 1, 10), .01f, player);
		newEnemy.SetActive (true);
		currentEnemies.Add (newEnemy);
	}
}
