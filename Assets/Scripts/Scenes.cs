
public static class Scenes
{
	public enum SceneName {
		TextIntro,
		ParkScene,
		TextChanges,
		OuterHouseScene,
		TextClimax,
		InnerHouseScene
	}
	public static bool isHouseScene(SceneName sceneName) {
		return sceneName == SceneName.InnerHouseScene || sceneName == SceneName.OuterHouseScene;
	}

	public static SceneName getSceneName(string sceneName) {
		switch (sceneName) {
		case "TextIntro":
			return SceneName.TextIntro;
		case "ParkScene":
			return SceneName.ParkScene;
		case "TextChanges":
			return SceneName.TextChanges;
		case "OuterHouseScene":
			return SceneName.OuterHouseScene;
		case "TextClimax":
			return SceneName.TextClimax;
		case "InnerHouseScene":
			return SceneName.InnerHouseScene;
		default:
			return SceneName.TextIntro;
		}
	}
}