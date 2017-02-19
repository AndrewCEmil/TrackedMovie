﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject bullet;
	public GameObject camera;
	public GameObject enemyGeneratorObj;
	private GameObject linkedTarget;
	private EnemyGenerator enemyGenerator;
	void Start () {
		Physics.gravity = new Vector3(0, -0.2F, 0);
		GameObject levelObject = GameObject.Find ("LevelObject");
		Physics.bounceThreshold = 0;
		enemyGenerator = enemyGeneratorObj.GetComponent<EnemyGenerator> ();
	}

	// Update is called once per frame
	void Update () {
	}

	void Shoot() {
		GameObject newBullet = Instantiate (bullet);
		newBullet.SetActive (true);
		newBullet.transform.position = transform.position + camera.transform.forward;
		Rigidbody bulletRB = newBullet.GetComponent<Rigidbody> ();
		Vector3 theForwardDirection = camera.transform.TransformDirection (Vector3.forward);
		Vector3 realForward = camera.transform.forward;
		bulletRB.AddForce (theForwardDirection * 200f);
	}

	public void BulletCollided(GameObject bulletObj) {
		Destroy (bulletObj);
		Debug.Log ("Destroyed bullet");
		enemyGenerator.PlaceNewEnemy();
	}

	public void BackToLevels() {
		Application.LoadLevel("LevelScene");
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
}