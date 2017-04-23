using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public static class SceneController {

	public static void LoadNextScene(Scenes.SceneName currentSceneName) {
		switch(currentSceneName) {
		case Scenes.SceneName.ParkScene: 
			LoadChapterOne ();
			break;
		case Scenes.SceneName.OuterHouseScene: 
			LoadChapterTwo ();
			break;
		}
	}

	public static void LoadGame() {
		SceneManager.LoadScene("DemoScene");
	}

	public static void LoadSettings() {
		SceneManager.LoadScene ("SettingsScene");
	}

	public static void LoadStart() {
		SceneManager.LoadScene ("StartScene");
	}

	public static void LoadLevelSelection() {
		SceneManager.LoadScene ("LevelScene");
	}

	public static void LoadChapterZero() {
		SceneManager.LoadScene ("ChapterZeroScene");
	}

	public static void LoadChapterOne() {
		SceneManager.LoadScene ("ChapterOneScene");
	}

	public static void LoadChapterTwo() {
		SceneManager.LoadScene ("ChapterTwoScene");
	}

	public static void LoadChapterThree(bool eaten) {
		if (eaten) {
			SceneManager.LoadScene ("ChapterThreeEatenScene");
		} else {
			SceneManager.LoadScene ("ChapterThreeShotScene");
		}
	}

	public static void LoadAboutScene() {
		SceneManager.LoadScene ("AboutScene");
	}
}
