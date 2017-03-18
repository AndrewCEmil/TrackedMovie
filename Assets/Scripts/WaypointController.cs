using UnityEngine;
using System.Collections;

public class WaypointController : MonoBehaviour, ICardboardGazeResponder {

	private bool isGazedAt;
	private bool isSelected;
	private GameObject player;
	private PlayerController playerController;
	private float fader;
	private Material material;
	// Use this for initialization
	void Start () {
		isGazedAt = false;
		isSelected = false;
		fader = 0f;
		player = GameObject.FindGameObjectWithTag ("Player");
		playerController = player.GetComponent<PlayerController> ();
		material = gameObject.GetComponent<Renderer> ().material;
		TurnBlue ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateColor ();
	}

	void UpdateColor() {
		if (fader != 0) {
			Color curColor = material.color;
			curColor.a = curColor.a + fader;
			material.color = curColor;
			if (curColor.a < 0 || curColor.a > .5) {
				fader = 0;
			}
		}
	}

	private void SetGazedAt(bool isGazedAt) {
		this.isGazedAt = isGazedAt;
		playerController.SetWaypointGazing (isGazedAt);
		SetColor ();
	}

	private void WaypointTriggered() {
		playerController.WaypointTriggered (gameObject);
	}

	public void CurrentlySelected(bool isSelected) {
		if (this.isSelected && !isSelected) { //if unselected
			FadeIn ();
		}
		this.isSelected = isSelected;
		SetColor ();
	}

	private void SetColor() {
		if (isSelected) {
			FadeOut ();
		} else if (isGazedAt) {
			TurnWhite (); //TODO this is a dumb color
		} else {
			TurnBlue ();
		}
	}

	private void TurnBlue() {
		material.color = new Color (0f, 0f, 1f, material.color.a);
	}

	private void TurnGreen() {
		material.color = new Color (0f, 1f, 0f, material.color.a);
	}

	private void TurnWhite() {
		material.color = new Color (0f, 0f, 0f, material.color.a);
	}

	public void Dissapear() {
		gameObject.GetComponent<Renderer> ().enabled = false;
	}

	private void FadeOut() {
		fader = -.01f;
	}

	private void FadeIn() {
		fader = .01f;
		gameObject.GetComponent<Renderer> ().enabled = true;
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
