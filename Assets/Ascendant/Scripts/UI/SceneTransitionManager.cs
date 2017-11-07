using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ascendant.UI {
	public sealed class SceneTransitionManager : BaseBehaviour {
		public GameObject SceneTransitionPrefab;

		private void Awake() {
			Container.Register(this);
		}

		public void StartTransition(string sceneName, int seconds) {
			SceneTransition sceneTransition = Instantiate(SceneTransitionPrefab).GetComponent<SceneTransition>();
			sceneTransition.sceneName = sceneName;
			sceneTransition.seconds = seconds;
			sceneTransition.onFinished = () => {
				Debug.Log("done");
			};
		}
	}
}
