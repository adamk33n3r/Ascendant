using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Ascendant.Logic;

namespace Ascendant.Editor {
    [CustomEditor(typeof(Initialization))]
    public class InitializationInspector : UnityEditor.Editor {
        private Initialization initialization;

        private void OnEnable() {
			this.initialization = (Initialization)this.target;
		}

        public override void OnInspectorGUI() {
			Undo.RecordObject(this.target, "Inspector");

			// RandomSeed
			this.initialization.RandomSeed = EditorGUILayout.IntField("Random Seed", this.initialization.RandomSeed);

			// SceneToLoad
			SceneAsset currentScene = GetSceneAsset(this.initialization.SceneToLoad);
			SceneAsset scene = EditorGUILayout.ObjectField("Scene to Load", currentScene, typeof(SceneAsset), false) as SceneAsset;

			if (scene == null) {
				this.initialization.SceneToLoad = null;
			} else if (scene.name == "_preload") {
				Debug.LogError("Setting Scene to Load to '_preload' will cause an infinite loop!!!");
				this.initialization.SceneToLoad = null;
			} else {
				// Check if the scene is in the build settings
				bool isInBuildSettings = GetSceneAsset(scene.name) != null;

				// If it's not, ask if we should add it
				if (!isInBuildSettings) {
					bool yes = EditorUtility.DisplayDialog("Scene must be in build settings!", "Do you want to add the scene to the build settings?", "Yes", "No");
					if (yes) {
						EditorBuildSettingsScene newBuildSettingsScene = new EditorBuildSettingsScene(AssetDatabase.GetAssetPath(scene), true);
						List<EditorBuildSettingsScene> currentBuildScenes = new List<EditorBuildSettingsScene>(EditorBuildSettings.scenes);
						currentBuildScenes.Add(newBuildSettingsScene);
						EditorBuildSettings.scenes = currentBuildScenes.ToArray();
						this.initialization.SceneToLoad = scene.name;
					}
				} else {
					this.initialization.SceneToLoad = scene.name;
				}
			}

			// SkipTransition
			this.initialization.SkipTransition = EditorGUILayout.Toggle("Skip Transition", this.initialization.SkipTransition);

        }

		private SceneAsset GetSceneAsset(string sceneObjectName) {
			if (string.IsNullOrEmpty(sceneObjectName)) {
				return null;
			}

			foreach (var editorScene in EditorBuildSettings.scenes) {
				if (editorScene.path.IndexOf(sceneObjectName) != -1) {
					return AssetDatabase.LoadAssetAtPath(editorScene.path, typeof(SceneAsset)) as SceneAsset;
				}
			}
			return null;
		}
    }
}
