using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyController : MonoBehaviour {

	private int hitPoints;
	private int health;
	private Vector3 moveVector;
	private float speed;
	private GameObject currentPathTarget;
	private GameObject player;
	private PlayerController playerController;
	private PathFinder pathFinder;
	private string sceneName;
	private Vector3 tempPos;
	void Start () {
		sceneName = SceneManager.GetActiveScene ().name;
	}

	public void Initialize(int hitPoints, Vector3 startPosition, float speed, GameObject player, GameObject target) {
		this.hitPoints = hitPoints;
		this.health = this.hitPoints;
		transform.position = startPosition;
		this.speed = speed;
		this.player = player;
		this.playerController = player.GetComponent<PlayerController> ();
		this.currentPathTarget = target;
		GameObject pathFinderObj = GameObject.Find ("PathFinder");
		pathFinder = pathFinderObj.GetComponent<PathFinder> ();
	}

	// Update is called once per frame
	void Update () {
		if (sceneName == "HouseScene") {
			Move ();
		} else if (sceneName == "ParkScene") {
			MovePark ();
		}
	}

	void MovePark() {
		//TODO
		tempPos = transform.position + Random.onUnitSphere / 10;
		tempPos.y = -3.5f;
		transform.position = tempPos;
	}

	void Move ()
	{
		if(AtTarget()) {
			UpdateTarget ();
		}
		transform.position = transform.position + ((currentPathTarget.transform.position - transform.position).normalized * speed);
	}

	private bool AtTarget() {
		return Vector3.Distance (transform.position, currentPathTarget.transform.position) < 1;
	}

	void UpdateTarget() {
		if (currentPathTarget.name == playerController.GetCurrentWaypoint ()) {
			return;
		}
		currentPathTarget = pathFinder.GetNextWaypoint (currentPathTarget.name, playerController.GetCurrentWaypoint ());
	}

	void Hit(BulletController bulletController) {
		Debug.Log ("got the bullet controller");
		TakeDamage (bulletController.Damage ());
	}

	void TakeDamage (int damageAmount) {
		health = health - damageAmount;
		if (health <= 0) {
			Die ();
		}
	}

	void Die() {
		//TODO do I need to destroy the object?
		Debug.Log("I am dead");
		gameObject.SetActive (false);
		Destroy (gameObject);
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("ENEMY COLLIDED");
		if (other.CompareTag ("Bullet")) {
			Hit (other.gameObject.GetComponent<BulletController> ());
		}
	}
}
