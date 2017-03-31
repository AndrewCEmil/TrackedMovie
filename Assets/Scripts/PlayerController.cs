using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	public GameObject bullet;
	public GameObject camera;
	public GameObject enemyGeneratorObj;
	private GameObject linkedTarget;
	private EnemyGenerator enemyGenerator;
	private AudioSource audioSource;
	private GameObject currentWaypoint;
	private float speed;
	private bool waypointGazing;
	private string sceneName;
	private IList<string> path;
	void Start () {
		Physics.gravity = new Vector3(0, -0.2F, 0);
		Physics.bounceThreshold = 0;
		audioSource = GetComponentInChildren<AudioSource> ();
		waypointGazing = false;
		sceneName = SceneManager.GetActiveScene ().name;
		if (sceneName == "ParkScene") {
			GetPath ();
			currentWaypoint = GameObject.Find ("W0");
			speed = 0.029f;
		} else if (sceneName == "HouseScene") {
			enemyGenerator = enemyGeneratorObj.GetComponent<EnemyGenerator> ();
			speed = 0.1f;
		}
	}

	private void GetPath() {
		path = new List<string> () {
			"W0", 
			"W1", 
			"W2", 
			"W3", 
			"W4",
			"W5"
		};
	}

	// Update is called once per frame
	void Update () {
		HandleMovement ();
	}

	void Shoot() {
		if (!ShouldShoot()) {
			return;
		}
		GameObject newBullet = Instantiate (bullet);
		newBullet.SetActive (true);
		newBullet.transform.position = transform.position + camera.transform.forward;
		Rigidbody bulletRB = newBullet.GetComponent<Rigidbody> ();
		Vector3 theForwardDirection = camera.transform.TransformDirection (Vector3.forward);
		Vector3 realForward = camera.transform.forward;
		bulletRB.AddForce (theForwardDirection * 2000f);
		PlayShootNoise ();
	}

	bool ShouldShoot() {
		return !waypointGazing && sceneName == "HouseScene";
	}

	public void SetWaypointGazing(bool isGazing) {
		waypointGazing = isGazing;
	}

	public string GetCurrentWaypoint() {
		if (currentWaypoint == null) {
			return "StartWaypoint";
		}
		return currentWaypoint.name;
	}

	private void HandleMovement() {
		if (sceneName == "HouseScene") {
			HandleHouseMovement ();
		} else if (sceneName == "ParkScene") {
			HandleParkMovement ();
		}

	}

	private void HandleHouseMovement() {
		if (InRangeOfWaypoint ()) {
			if (currentWaypoint != null) {
				currentWaypoint.GetComponent<WaypointController> ().Dissapear ();
			}
		} else {
			DoMovement ();
		}
	}

	private void HandleParkMovement() {
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
		return currentWaypoint.name == "W7";
	}

	private void DoMovement() {
		transform.position = transform.position + ((currentWaypoint.transform.position - transform.position).normalized * speed);
	}

	private bool InRangeOfWaypoint() {
		if (currentWaypoint == null) {
			return true;
		}

		float dist = Vector3.Distance (gameObject.transform.position, currentWaypoint.transform.position);
		return dist < 1.0;
	}

	public void BulletCollided(GameObject bulletObj) {
		Destroy (bulletObj);
		Debug.Log ("Destroyed bullet");
		enemyGenerator.PlaceNewEnemy();
	}

	private void PlayShootNoise() {
		audioSource.Play();
	}

	void OnEnable(){
		Cardboard.SDK.OnTrigger += TriggerPulled;
	}

	void OnDisable(){
		Cardboard.SDK.OnTrigger -= TriggerPulled;
	}

	void TriggerPulled() {
		Debug.Log("The trigger was pulled!");
		Shoot ();
	}

	public void WaypointTriggered(GameObject waypoint) {
		if (currentWaypoint == null || waypoint.GetInstanceID () != currentWaypoint.GetInstanceID()) {
			HandleNewWaypoint (waypoint);
		}
	}

	void HandleNewWaypoint(GameObject waypoint) {
		if (currentWaypoint != null) {
			currentWaypoint.GetComponent<WaypointController> ().CurrentlySelected (false);
		}
		currentWaypoint = waypoint;
		currentWaypoint.GetComponent<WaypointController> ().CurrentlySelected (true);
	}
}
