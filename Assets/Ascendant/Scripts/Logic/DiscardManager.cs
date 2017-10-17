using UnityEngine;
using System.Collections.Generic;
using Ascendant.ScriptableObjects;

namespace Ascendant.Scripts.Logic {
	public class DiscardManager : BaseBehaviour {
        public Stack<CardAsset> Cards { get; protected set; }

		private void Awake() {
			Container.Register(this);
		}
		private void Start() {
			//Container.Get<>();
			Cards = new Stack<CardAsset>();
		}

		public void AddCard(CardAsset cardAsset) {
			print("discarding", cardAsset);
			Cards.Push(cardAsset);
			EventManager.Fire(Events.CARD_DISCARDED, cardAsset);
		}
	}
}
