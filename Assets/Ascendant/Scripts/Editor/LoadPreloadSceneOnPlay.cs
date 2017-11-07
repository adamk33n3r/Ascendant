using UnityEngine.SceneManagement;

using UnityEditor;
using UnityEditor.SceneManagement;

namespace Ascendant.Editor {
	[InitializeOnLoad]
	public sealed class LoadPreloadSceneOnPlay {
		private const string cEditorPrefPreviousScene = "LoadPreloadSceneOnPlay.PreviousScene";
		private static string PreviousScenePath {
			get { return EditorPrefs.GetString(cEditorPrefPreviousScene, SceneManager.GetActiveScene().path); }
			set { EditorPrefs.SetString(cEditorPrefPreviousScene, value); }
		}

		static LoadPreloadSceneOnPlay() {
			EditorApplication.playmodeStateChanged += CheckPlayMode;
		}

		private static void CheckPlayMode() {
			if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying) {
				Scene activeScene = SceneManager.GetActiveScene();
				PreviousScenePath = activeScene.path;

				// Save all open scenes since we're switching
				if (activeScene.isDirty) {
					EditorSceneManager.SaveOpenScenes();
				}
				string previousSceneName = activeScene.name;
				EditorSceneManager.OpenScene("Assets/Ascendant/Scenes/_preload.unity");
				if (previousSceneName != "_preload") {
					UnityEngine.GameObject.Find("_app").GetComponent<Logic.Initialization>().SceneToLoad = previousSceneName;
				}
			} else if (!EditorApplication.isPlaying) {
				if (PreviousScenePath == null) {
					return;
				}
				EditorSceneManager.OpenScene(PreviousScenePath);
			}
		}
	}
}
