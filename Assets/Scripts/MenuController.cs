using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public Slider musicSlider;
	public Toggle particleToggle;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadGame() {
		SceneManager.LoadScene("DemoScene");
	}

	public void LoadSettings() {
		SceneManager.LoadScene ("SettingsScene");
	}

	public void LoadStart() {
		SceneManager.LoadScene ("StartScene");
	}

	public void LoadLevelSelection() {
		SceneManager.LoadScene ("LevelScene");
	}

	public void LoadChapterZero() {
		SceneManager.LoadScene ("ChapterZeroScene");
	}

	public void LoadParkScene() {
		SceneManager.LoadScene ("ParkScene");
	}

	public void LoadAboutScene() {
		SceneManager.LoadScene ("AboutScene");
	}

	public void LoadOuterHouseScene() {
		SceneManager.LoadScene ("OuterHouseScene");
	}

	public void LoadInnerHouseScene() {
		SceneManager.LoadScene ("InnerHouseScene");
	}

	public void SetMusicVolume() {
		AudioSource musicPlayer = GameObject.Find ("MusicPlayer").GetComponent<AudioSource> ();
		musicPlayer.volume = musicSlider.value;
	}

	//Reversed so we default to particles on
	public void SetParticleToggle() {
		if (particleToggle.isOn) {
			PlayerPrefs.SetInt ("ParticlesOff", 0);
		} else {
			PlayerPrefs.SetInt ("ParticlesOff", 1);
		}
		PlayerPrefs.Save ();
	}
}
