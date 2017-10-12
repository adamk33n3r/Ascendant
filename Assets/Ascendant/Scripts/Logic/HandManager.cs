using System;
using System.Collections.Generic;
using UnityEngine;
using Ascendant.ScriptableObjects;

namespace Ascendant.Scripts.Logic {
	public class HandManager : BaseBehaviour {
        [Header("Hand Info")]
        public int maxCount = 7;

		private List<CardAsset> cards = new List<CardAsset>();
        private Action<CardAsset> cbOnCardDrawn;

		private Visual.HandManager visual;

		public void Awake() {
			Container.Register(this);
			Events.Listen("test", (data) => {
				print("test event was fired", data);
			});
		}

		public void Start() {
			this.visual = Container.Get<Visual.HandManager>();
		}

		public bool AddCard(CardAsset cardAsset) {
			if (this.cards.Count >= this.maxCount) {
				return false;
			}
			this.cards.Add(cardAsset);
			this.cbOnCardDrawn(cardAsset);
			return true;
		}

		public CardAsset GetSelectedCard() {
			Visual.Cards.CardManager card = this.visual.GetSelectedCard();
			if (card == null) {
				return null;
			}
			return card.CardAsset;
		}

		public CardAsset TakeSelectedCard() {
			Visual.Cards.CardManager card = this.visual.TakeSelectedCard();
			if (card == null) {
				return null;
			}
			return card.CardAsset;
		}

		// TODO: Maybe just a global event system?
		public void RegisterCardDrawnCallback(Action<CardAsset> callback) {
			this.cbOnCardDrawn += callback;
		}
	}
}
