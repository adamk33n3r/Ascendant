using System.Collections.Generic;
using UnityEngine;

namespace Ascendant {
	public sealed class DontDestroyOnLoad : BaseBehaviour {
		private void Awake () {
			DontDestroyOnLoad(this);
		}
	}
}
