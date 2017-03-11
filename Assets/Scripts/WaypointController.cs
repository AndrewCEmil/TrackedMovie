using UnityEngine;
using System.Collections;

public class WaypointController : MonoBehaviour, ICardboardGazeResponder {

	private bool isGazedAt;
	private bool isSelected;
	private GameObject player;
	private PlayerController playerController;
	// Use this for initialization
	void Start () {
		isGazedAt = false;
		isSelected = false;
		player = GameObject.FindGameObjectWithTag ("Player");
		playerController = player.GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void SetGazedAt(bool isGazedAt) {
		this.isGazedAt = isGazedAt;
	}

	private void WaypointTriggered() {
		playerController.WaypointTriggered (gameObject);
	}

	public void CurrentlySelected(bool isSelected) {
		this.isSelected = isSelected;
		SetColor ();
	}

	private void SetColor() {
		if (isSelected) {
			TurnBlue ();
		} else if (isGazedAt) {
			TurnGreen ();
		} else {
			TurnRed ();
		}
	}

	private void TurnBlue() {
		GetComponent<Renderer> ().material.color = Color.blue;
	}

	private void TurnGreen() {
		GetComponent<Renderer> ().material.color = Color.green;
	}

	private void TurnRed() {
		GetComponent<Renderer> ().material.color = Color.red;
	}


	#region ICardboardGazeResponder implementation

	/// Called when the user is looking on a GameObject with this script,
	/// as long as it is set to an appropriate layer (see CardboardGaze).
	public void OnGazeEnter() {
		SetGazedAt(true);
	}

	/// Called when the user stops looking on the GameObject, after OnGazeEnter
	/// was already called.
	public void OnGazeExit() {
		SetGazedAt(false);
	}

	// Called when the Cardboard trigger is used, between OnGazeEnter
	/// and OnGazeExit.
	public void OnGazeTrigger() {
		WaypointTriggered ();
	}

	#endregion
}
