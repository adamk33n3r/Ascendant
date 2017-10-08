using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ascendant.Scripts.Logic {
	public class Initialization : MonoBehaviour {
		public int Seed = 0;

		public void Start() {
			Random.InitState(Seed);
		}
	}
}
