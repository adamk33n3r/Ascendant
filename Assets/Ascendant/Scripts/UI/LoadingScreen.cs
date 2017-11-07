using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ascendant.UI {
	public sealed class LoadingScreen : BaseBehaviour {
		public GameObject loadingTextGO;
		private void Start() {
			this.loadingTextGO.SetActive(true);
		}
	}
}
