using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public GameObject messageBoxObject;
	public string enemyId;
	public int hitPoints;

	private int health;
	private Vector3 moveVector;
	void Start () {
		health = hitPoints;
		moveVector = new Vector3 (0, 0, .1f);
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
	}

	void Move() {
		transform.position = transform.position + moveVector;
	}

	void Hit(BulletController bulletController) {
		Debug.Log ("got the bullet controller");
		TakeDamage (bulletController.Damage ());
	}

	void TakeDamage (int damageAmount) {
		health = health - damageAmount;
		if (health < 0) {
			Die ();
		}
	}

	void Die() {
		//TODO
		Debug.Log("I am dead");
		gameObject.SetActive (false);
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("ENEMY COLLIDED");
		if (other.CompareTag ("Bullet")) {
			Hit (other.gameObject.GetComponent<BulletController> ());
		}
	}
}
