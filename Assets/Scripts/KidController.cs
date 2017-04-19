using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class KidController : MonoBehaviour {

	public GameObject player;
	private PlayerController playerController;
	private IList<string> path;
	private GameObject currentWaypoint;
	private float speed;
	private Scenes.SceneName sceneName;
	private float startTime;
	// Use this for initialization
	void Start () {
		sceneName = Scenes.getSceneName (SceneManager.GetActiveScene ().name);
		CreatePath ();
		startTime = -1.0f;
		if (sceneName == Scenes.SceneName.InnerHouseScene) {
			currentWaypoint = GameObject.Find ("StairWaypoint");
			playerController = player.GetComponent<PlayerController> ();
		} else if (sceneName == Scenes.SceneName.ParkScene) {
			currentWaypoint = GameObject.Find ("W0");
		} else if (sceneName == Scenes.SceneName.OuterHouseScene) {
			currentWaypoint = GameObject.Find ("MiddleWaypoint");
			playerController = player.GetComponent<PlayerController> ();
			startTime = Time.fixedTime;
		}
		speed = 0.03f;
	}

	void CreatePath() {
		if (sceneName == Scenes.SceneName.InnerHouseScene) {
			CreateInnerHousePath ();
		} else if (sceneName == Scenes.SceneName.OuterHouseScene) {
			CreateOuterHousePath ();
		} else if (sceneName == Scenes.SceneName.ParkScene) {
			CreateParkPath ();
		}
	}

	void CreateInnerHousePath() {
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

	void CreateOuterHousePath() {
		path = new List<string> () {
			"LeftWaypoint",
			"RightWaypoint"
		};
	}

	void CreateParkPath() {
		path = new List<string> () {
			"W0", 
			"W1", 
			"W2", 
			"W3", 
			"W4",
			"W5",
			"W6"
		};
	}

	// Update is called once per frame
	void Update () {
		Move ();
		HandleSceneChange ();
	}

	void HandleSceneChange() {
		if (InTerminalPosition ()) {
			if (startTime < 0) {
				startTime = Time.fixedTime;
			} else {
				MaybeChangeScenes ();
			}
		}
	}

	void MaybeChangeScenes() {
		if (sceneName == Scenes.SceneName.ParkScene) {
			if (Time.fixedTime - startTime > 20) {
				SceneController.LoadNextScene(sceneName);
			}
		} else if (sceneName == Scenes.SceneName.OuterHouseScene) {
			if (Time.fixedTime - startTime > 120) {
				SceneController.LoadNextScene(sceneName);
			}
		}
	}

	void Move ()
	{
		if (AtTarget ()) {
			MaybeUpdateTarget ();
		} else if (sceneName == Scenes.SceneName.OuterHouseScene) {
			Vector3 currentPos = currentWaypoint.transform.position;
			currentPos.z = currentPos.z - 2;
			transform.position = transform.position + ((currentPos - transform.position).normalized * speed);
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
			if (sceneName == Scenes.SceneName.OuterHouseScene) {
				UpdateOuterWaypoint ();
			} else {
				currentWaypoint = GameObject.Find (path [path.IndexOf (currentWaypoint.name) + 1]);
			}
		}
	}

	private void UpdateOuterWaypoint() {
		if (currentWaypoint.name == "MiddleWaypoint") {
			currentWaypoint = GameObject.Find (path [Random.Range (0, 1)]);
		} else {
			currentWaypoint = GameObject.Find ("MiddleWaypoint");
		}
	}


	private bool ShouldUpdateTarget () {
		if (InTerminalPosition ()) {
			return false;
		}
		return true;
	}

	private bool InTerminalPosition() {
		if (Scenes.isHouseScene(sceneName)) {
			return currentWaypoint.name == "BedroomWaypoint";
		} else if (sceneName == Scenes.SceneName.ParkScene) {
			return currentWaypoint.name == "W6";
		}
		return false;
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("KID COLLIDED");
		if (other.CompareTag ("Bullet")) {
			BulletHit (other.gameObject.GetComponent<BulletController> ());
		} else if(other.CompareTag("Enemy")) {
			EnemyHit();
		}
	}

	void BulletHit(BulletController bulletController) {
		playerController.ChildShot ();
	}

	void EnemyHit() {
		playerController.ChildEaten ();
	}
}
