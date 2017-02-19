using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	private int hitPoints;
	private int health;
	private Vector3 moveVector;
	private float speed;
	private GameObject target;
	void Start () {
	}

	public void Initialize(int hitPoints, Vector3 startPosition, float speed, GameObject target) {
		this.hitPoints = hitPoints;
		this.health = this.hitPoints;
		transform.position = startPosition;
		this.speed = speed;
		this.target = target;
	}

	// Update is called once per frame
	void Update () {
		Move ();
	}

	void Move ()
	{
		transform.position = transform.position + ((target.transform.position - transform.position).normalized * speed);
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
