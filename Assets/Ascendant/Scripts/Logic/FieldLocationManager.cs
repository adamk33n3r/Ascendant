using Ascendant.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ascendant.Scripts.Logic {
	public class FieldLocationManager : MonoBehaviour {
		private Visual.FieldLocationManager visual;
		private CardAsset cardAsset;
		public void Start() {
			this.visual = Container.Get<Visual.FieldManager>().GetLocation();
		}
		public bool PlayCard(CardAsset card) {
			this.cardAsset = card;
			// TODO: tell visual to create a new instance of card manager
			return true;
		}
	}
}
