using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PathFinder : MonoBehaviour {

	IDictionary<string, IList<string>> waypointMap;
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

		return GameObject.Find ("StartWaypoint");
	}

	//Note that the tree is the inverse path because we find our way backwards
	void InitializeTree() {
		waypointMap = new Dictionary<string, IList<string>> ();
		GameObject startWaypoint = GameObject.Find ("StartWaypoint");
		GameObject stairWaypoint = GameObject.Find ("StairWaypoint");
		GameObject porchWaypoint = GameObject.Find ("PorchWaypoint");
		GameObject foyerWaypoint = GameObject.Find ("FoyerWaypoint");
		GameObject KitchenWaypoint = GameObject.Find ("KitchenWaypoint");

		List<string> startChildren = new List<string> ();
		List<string> stairChildren = new List<string> () { "StartWaypoint" };
		List<string> porchChildren = new List<string> () { "StairWaypoint" };
		List<string> foyerChildren = new List<string> () { "PorchWaypoint" };
		List<string> kitchenChildren = new List<string> () { "FoyerWaypoint" };

		waypointMap.Add("StartWaypoint", startChildren);
		waypointMap.Add("StairWaypoint", stairChildren);
		waypointMap.Add("PorchWaypoint", porchChildren);
		waypointMap.Add("FoyerWaypoint", foyerChildren);
		waypointMap.Add("KitchenWaypoint", kitchenChildren);
	}
}
