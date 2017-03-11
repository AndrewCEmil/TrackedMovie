using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyGenerator : MonoBehaviour {

	public GameObject baseEnemy;
	public GameObject player;
	private GameObject startWaypoint;
	List<GameObject> currentEnemies;
	private float lastPlacementTime;
	// Use this for initialization
	void Start () {
		currentEnemies = new List<GameObject>();
		lastPlacementTime = Time.realtimeSinceStartup - 10;
		startWaypoint = GameObject.Find ("StartWaypoint");
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
		ec.Initialize (2, GetNextEnemyLocation(), .1f, player, startWaypoint);
		newEnemy.SetActive (true);
		currentEnemies.Add (newEnemy);
	}

	private Vector3 GetNextEnemyLocation() {
		Vector3 startLoc = new Vector3 (Random.Range (-40, 40), -2.5f, 70);
		return startLoc;
	}
}
