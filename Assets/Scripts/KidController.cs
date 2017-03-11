using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KidController : MonoBehaviour {

	IList<string> path;
	GameObject currentWaypoint;
	private float speed;
	// Use this for initialization
	void Start () {
		CreatePath ();
		currentWaypoint = GameObject.Find ("StairWaypoint");
		speed = 0.03f;
	}

	void CreatePath() {
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
		return currentWaypoint.name == "BedroomWaypoint";
	}
}
