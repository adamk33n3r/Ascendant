using System;
using System.Collections.Generic;
using UnityEngine;
using Ascendant.ScriptableObjects;

namespace Ascendant.Scripts.Logic {
	public class HandManager : MonoBehaviour {
        [Header("Hand Info")]
        public int maxCount = 7;

		private List<CardAsset> cards = new List<CardAsset>();
        private Action<CardAsset> cbOnCardDrawn;

		public void Awake() {
			Container.Register(this);
		}

		public bool AddCard(CardAsset cardAsset) {
			if (this.cards.Count >= this.maxCount) {
				return false;
			}
			this.cards.Add(cardAsset);
			this.cbOnCardDrawn(cardAsset);
			return true;
		}

		// TODO: Maybe just a global event system?
		public void RegisterCardDrawnCallback(Action<CardAsset> callback) {
			this.cbOnCardDrawn += callback;
		}
	}
}
