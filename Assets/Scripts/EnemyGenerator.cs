using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class EnemyGenerator : MonoBehaviour {

	public GameObject baseEnemy;
	public GameObject player;
	private GameObject startWaypoint;
	List<GameObject> currentEnemies;
	private float lastPlacementTime;
	private string sceneName;
	private Vector3 boxPosition;
	// Use this for initialization
	void Start () {
		currentEnemies = new List<GameObject>();
		lastPlacementTime = Time.realtimeSinceStartup - 10;
		startWaypoint = GameObject.Find ("StartWaypoint");
		sceneName = SceneManager.GetActiveScene ().name;
		if (sceneName == "ParkScene") {
			boxPosition = new Vector3 (108f, -3.5f, 3f);
			InitializeParkEnemies ();
		}
	}

	private void InitializeParkEnemies() {
		for (int i = 0; i < 100; i++) {
			PlaceNewEnemy ();
		}
	}

	void Update () {
		if (ShouldPlaceNewEnemy ()) {
			PlaceNewEnemy ();
		}
	}

	private bool ShouldPlaceNewEnemy() {
		if (sceneName == "HouseScene") {
			return ShouldPlaceNewEnemyHouse ();
		} else if (sceneName == "ParkScene") {
			return ShouldPlaceNewEnemyPark ();
		}
		return false;
	}

	private bool ShouldPlaceNewEnemyHouse() {
		if (Time.realtimeSinceStartup - lastPlacementTime > 10) {
			lastPlacementTime = Time.realtimeSinceStartup;
			return true;
		}
		return false;
	}

	private bool ShouldPlaceNewEnemyPark() {
		//TODO
		return false;
	}

	public void PlaceNewEnemy() {
		if (sceneName == "HouseScene") {
			PlaceNewEnemyHouse ();
		} else if (sceneName == "ParkScene") {
			PlaceNewEnemyPark ();
		}
	}

	private void PlaceNewEnemyHouse() {
		GameObject newEnemy = Instantiate(baseEnemy);
		EnemyController ec = newEnemy.GetComponent<EnemyController> ();
		ec.Initialize (2, GetNextEnemyLocation(), .1f, player, startWaypoint);
		newEnemy.SetActive (true);
		currentEnemies.Add (newEnemy);
	}

	private void PlaceNewEnemyPark() {
		Vector3 position = Random.insideUnitSphere * 10f;
		position = position + boxPosition;
		position.y = boxPosition.y;
		GameObject newEnemy = Instantiate(baseEnemy);
		EnemyController ec = newEnemy.GetComponent<EnemyController> ();
		ec.Initialize (2, position, .1f, player, startWaypoint);
		newEnemy.SetActive (true);
		currentEnemies.Add (newEnemy);
	}

	private Vector3 GetNextEnemyLocation() {
		Vector3 startLoc = new Vector3 (Random.Range (-40, 40), -2.5f, 70);
		return startLoc;
	}
}
