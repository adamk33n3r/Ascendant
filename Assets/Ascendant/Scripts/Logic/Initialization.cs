using UnityEngine;
using DG.Tweening;

namespace Ascendant.Logic {
	public class Initialization : MonoBehaviour {
		public int Seed = 0;

		public void Start() {
			Random.InitState(Seed);
            DOTween.Init();
		}
	}
}
