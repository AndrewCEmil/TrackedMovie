using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;

public class PathFinder : MonoBehaviour {

	IDictionary<string, IList<string>> waypointMap;
	IDictionary<string, string> parentMap;
	void Start () {
		InitializeTree ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameObject GetNextWaypoint(string currentWaypoint, string targetWaypoint) {
		HashSet<string> seenNodes = new HashSet<string> ();
		IList<string> fringe = new List<string> () { targetWaypoint };
		IList<string> tempFringe = new List<string> ();
		IList<string> nextNodes;
		while (fringe.Count > 0) {
			foreach (string node in fringe) {
				nextNodes = waypointMap [node];
				foreach (string nextNode in nextNodes) {
					if (!seenNodes.Contains (nextNode)) {
						if (nextNode == currentWaypoint) {
							if (node == targetWaypoint) {
								//TODO special return for case where you are already there
							}
							return GameObject.Find(node); 
						}
						tempFringe.Add (nextNode);
						seenNodes.Add (nextNode);
					}
				}
			}
			fringe.Clear ();
			foreach (string newFringeNode in tempFringe) {
				fringe.Add (newFringeNode);
			}
			tempFringe.Clear ();
		}

		return GameObject.Find (parentMap [currentWaypoint]);
	}

	//Note that the tree is the inverse path because we find our way backwards
	void InitializeTree() {
		Scenes.SceneName sceneName = Scenes.getSceneName (SceneManager.GetActiveScene ().name);
		if (Scenes.isHouseScene(sceneName)) {
			InitializeHouseTree ();
		} else if (sceneName == Scenes.SceneName.ParkScene) {
			InitializeParkTree ();
		}
	}
	void InitializeParkTree() {
		//TODO
	}

	void InitializeHouseTree() {
		waypointMap = new Dictionary<string, IList<string>> ();
		parentMap = new Dictionary<string, string> ();

		List<string> startChildren = new List<string> ();
		List<string> stairChildren = new List<string> () { "StartWaypoint" };
		List<string> porchChildren = new List<string> () { "StairWaypoint" };
		List<string> foyerChildren = new List<string> () { "PorchWaypoint" };
		List<string> diningRoomWaypoint = new List<string> () { "FoyerWaypoint" };
		List<string> kitchenChildren = new List<string> () { "DiningRoomWaypoint" };
		List<string> kitchenHallwayChildren = new List<string> () { "KitchenWaypoint" };
		List<string> mainHallwayChildren = new List<string> () { "KitchenHallwayWaypoint" };
		List<string> mainHallwayEndChildren = new List<string> () { "MainHallwayWaypoint" };
		List<string> backdoorChildren = new List<string> () { "MainHallwayEndWaypoint" };
		List<string> bedroomChildren = new List<string> () { "MainHallwayEndWaypoint" };

		waypointMap.Add("StartWaypoint", startChildren);
		waypointMap.Add("StairWaypoint", stairChildren);
		waypointMap.Add("PorchWaypoint", porchChildren);
		waypointMap.Add("FoyerWaypoint", foyerChildren);
		waypointMap.Add("DiningRoomWaypoint", diningRoomWaypoint);
		waypointMap.Add("KitchenWaypoint", kitchenChildren);
		waypointMap.Add("KitchenHallwayWaypoint", kitchenHallwayChildren);
		waypointMap.Add("MainHallwayWaypoint", mainHallwayChildren);
		waypointMap.Add("MainHallwayEndWaypoint", mainHallwayEndChildren);
		waypointMap.Add("BackdoorWaypoint", backdoorChildren);
		waypointMap.Add("BedroomWaypoint", bedroomChildren);

		parentMap.Add ("StartWaypoint", "StartWaypoint");
		parentMap.Add ("StairWaypoint", "StartWaypoint");
		parentMap.Add ("PorchWaypoint", "StairWaypoint");
		parentMap.Add ("FoyerWaypoint", "PorchWaypoint");
		parentMap.Add ("DiningRoomWaypoint", "FoyerWaypoint");
		parentMap.Add ("KitchenWaypoint", "DiningRoomWaypoint");
		parentMap.Add ("KitchenHallwayWaypoint", "KitchenWaypoint");
		parentMap.Add ("MainHallwayWaypoint", "KitchenHallwayWaypoint");
		parentMap.Add ("MainHallwayEndWaypoint", "MainHallwayWaypoint");
		parentMap.Add ("BackdoorWaypoint", "MainHallwayEndWaypoint");
		parentMap.Add ("BedroomWaypoint", "MainHallwayEndWaypoint");
	}
}
