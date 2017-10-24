using System;
using System.Collections.Generic;
using UnityEngine;
using Ascendant.ScriptableObjects;

namespace Ascendant.Logic {
	public class HandManager : BaseBehaviour {
        [Header("Hand Info")]
        public int maxCount = 7;

		private List<CardAsset> cards = new List<CardAsset>();
		private Visual.HandManager visual;

		public void Awake() {
			Container.Register(this);
		}

		public void Start() {
			this.visual = Container.Get<Visual.HandManager>();
		}

		public bool AddCard(CardAsset cardAsset) {
			if (this.cards.Count >= this.maxCount) {
				return false;
			}
			this.cards.Add(cardAsset);
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
			this.cards.Remove(card.CardAsset);
			return card.CardAsset;
		}

		public bool Discard() {
			CardAsset cardAsset = TakeSelectedCard();
			if (cardAsset == null) {
				return false;
			}
			var discard = Container.Get<DiscardManager>();
			discard.AddCard(cardAsset);
			return true;
		}
	}
}
