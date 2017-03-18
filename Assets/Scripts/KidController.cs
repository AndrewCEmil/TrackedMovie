using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class KidController : MonoBehaviour {

	private IList<string> path;
	private GameObject currentWaypoint;
	private float speed;
	private string sceneName;
	// Use this for initialization
	void Start () {
		CreatePath ();
		currentWaypoint = GameObject.Find ("StairWaypoint");
		speed = 0.03f;
		sceneName = SceneManager.GetActiveScene ().name;
	}

	void CreatePath() {
		if (sceneName == "HouseScene") {
			CreateHousePath ();
		} else if (sceneName == "ParkScene") {
			CreateParkPath ();
		}
	}

	void CreateHousePath() {
		path = new List<string> () {
			"StairWaypoint", 
			"PorchWaypoint", 
			"FoyerWaypoint", 
			"DiningRoomWaypoint", 
			"KitchenWaypoint",
			"KitchenHallwayWaypoint",
			"MainHallwayWaypoint",
			"MainHallwayEndWaypoint",
			"BedroomWaypoint"
		};
	}

	void CreateParkPath() {
		//TODO
	}

	// Update is called once per frame
	void Update () {
		Move ();
	}

	void Move ()
	{
		if (AtTarget ()) {
			MaybeUpdateTarget ();
		} else {
			transform.position = transform.position + ((currentWaypoint.transform.position - transform.position).normalized * speed);
		}
	}

	private bool AtTarget() {
		return Vector3.Distance (transform.position, currentWaypoint.transform.position) < 1;
	}

	private void MaybeUpdateTarget() {
		//TODO delay on moving to next target
		if(ShouldUpdateTarget()) {
			currentWaypoint = GameObject.Find (path [path.IndexOf (currentWaypoint.name) + 1]);
		}
	}

	private bool ShouldUpdateTarget() {
		if (InTerminalPosition ()) {
			return false;
		}
		return true;
	}

	private bool InTerminalPosition() {
		if (sceneName == "HouseScene") {
			return currentWaypoint.name == "BedroomWaypoint";
		} else if (sceneName == "ParkScene") {
			//TODO
		}
		return false;
	}
}
