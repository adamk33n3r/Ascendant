using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Ascendant.UI {
	public sealed class SceneTransition : BaseBehaviour {
		public string sceneName;
		public int seconds;
		public Color startColor;
		public Color endColor;
		public System.Action onFinished;

		private Image image;
		private float secondsSoFar = 0;

		private void Start() {
			DontDestroyOnLoad(this.gameObject);
			this.image = GetComponentInChildren<Image>();
			StartCoroutine(Transition(this.sceneName, this.seconds));
		}

		private IEnumerator Transition(string sceneName, int seconds) {
			float timeToTransition = seconds / 2f;

			this.secondsSoFar = 0;
			while (this.secondsSoFar < timeToTransition) {
				this.secondsSoFar += Time.deltaTime;
				this.image.color = Color.Lerp(this.startColor, this.endColor, this.secondsSoFar / timeToTransition);
				yield return null;
			}

			SceneManager.LoadScene(sceneName);

			this.secondsSoFar = 0;
			while (this.secondsSoFar < timeToTransition) {
				this.secondsSoFar += Time.deltaTime;
				this.image.color = Color.Lerp(this.endColor, this.startColor, this.secondsSoFar / timeToTransition);
				yield return null;
			}
			
			if (this.onFinished != null) {
				this.onFinished();
			}
			Destroy(this.gameObject);
		}
	}
}
