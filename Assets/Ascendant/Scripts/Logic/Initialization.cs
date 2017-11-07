using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace Ascendant.Logic {
	public class Initialization : MonoBehaviour {
		public int RandomSeed;
		public string SceneToLoad;
		public bool SkipTransition;

		public void Start() {
			Random.InitState(RandomSeed);
            DOTween.Init();

			if (SceneToLoad == null || SceneToLoad == "") {
				Debug.LogWarning("No SceneToLoad set in _preload._app");
				return;
			}

			if (SkipTransition) {
				SceneManager.LoadScene(SceneToLoad);
			} else {
				StartCoroutine(GoToNextScene());
			}
		}

		private IEnumerator GoToNextScene() {
			yield return new WaitForSeconds(2);
			Container.Get<UI.SceneTransitionManager>().StartTransition(SceneToLoad, 2);
		}
	}
}
