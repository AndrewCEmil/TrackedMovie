using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyGenerator : MonoBehaviour {

	public GameObject baseEnemy;
	public GameObject player;
	List<GameObject> currentEnemies;
	// Use this for initialization
	void Start () {
		currentEnemies = new List<GameObject>();
	}

	public void PlaceNewEnemy() {
		//TODO
		GameObject newEnemy = Instantiate(baseEnemy);
		EnemyController ec = newEnemy.GetComponent<EnemyController> ();
		ec.Initialize (10, new Vector3 (10, 1, 10), .01f, player);
		newEnemy.SetActive (true);
		currentEnemies.Add (newEnemy);
	}
}
