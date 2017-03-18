using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject bullet;
	public GameObject camera;
	public GameObject enemyGeneratorObj;
	private GameObject linkedTarget;
	private EnemyGenerator enemyGenerator;
	private AudioSource audioSource;
	private GameObject currentWaypoint;
	private bool inTransit;
	private float speed;
	private bool waypointGazing;
	void Start () {
		Physics.gravity = new Vector3(0, -0.2F, 0);
		GameObject levelObject = GameObject.Find ("LevelObject");
		Physics.bounceThreshold = 0;
		enemyGenerator = enemyGeneratorObj.GetComponent<EnemyGenerator> ();
		audioSource = GetComponentInChildren<AudioSource> ();
		inTransit = false;
		speed = 0.1f;
		waypointGazing = false;
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
		return !waypointGazing;
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
		if (InRangeOfWaypoint ()) {
			inTransit = false;
			if (currentWaypoint != null) {
				currentWaypoint.GetComponent<WaypointController> ().Dissapear ();
			}
		} else {
			DoMovement ();
		}
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

	public void BackToLevels() {
		Application.LoadLevel("LevelScene");
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
