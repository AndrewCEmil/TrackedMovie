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
		lastPlacementTime = Time.realtimeSinceStartup - 10;
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
		ec.Initialize (2, GetNextEnemyLocation(), .01f, player);
		newEnemy.SetActive (true);
		currentEnemies.Add (newEnemy);
	}

	private Vector3 GetNextEnemyLocation() {
		Vector3 startLoc = Random.onUnitSphere * Random.Range(5, 20);
		startLoc.y = 0;
		startLoc.z = Mathf.Abs (startLoc.z) + 10;
		startLoc.x = Mathf.Clamp (startLoc.x, -5, 5);
		return startLoc;
	}
}
