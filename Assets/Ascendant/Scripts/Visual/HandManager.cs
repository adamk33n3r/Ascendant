using System.Collections.Generic;
using Ascendant.ScriptableObjects;
using Ascendant.Scripts.Visual.Cards;
using UnityEngine;

namespace Ascendant.Scripts.Visual {
    public class HandManager : MonoBehaviour {
		public Logic.HandManager logicHandManager;

        [Header("Markers")]
        public Transform first;
        public Transform last;

        [Header("Hand Info")]
        public GameObject cardPrefab;

        private readonly List<Transform> cards = new List<Transform>();

		private float width;
		private CardManager selectedCard;

		public void Awake() {
			Container.Register(this);
		}

		public void Start() {
			this.width = this.last.position.x - this.first.position.x;
			this.logicHandManager.RegisterCardDrawnCallback(OnCardDrawn);
		}

		public CardManager GetSelectedCard() {
			return this.selectedCard;
		}

		public CardManager TakeSelectedCard() {
			CardManager selectedCard = this.selectedCard;
			RemoveCard(this.selectedCard);
			return selectedCard;
		}

		private bool RemoveCard(CardManager card) {
			// If the card you want to remove is selected, unselect it.
			if (this.selectedCard == card) {
				this.selectedCard = null;
			}

			// Remove callback
			card.UnregisterDeckClickedCallback(OnCardClicked);
			bool removed = this.cards.Remove(card.transform);

			// Reset positions
			SetCardPositions();
			return removed;
		}

        private void OnCardDrawn(CardAsset cardAsset) {
            GameObject card = Instantiate(this.cardPrefab);
			CardManager manager = card.GetComponent<CardManager>();
			manager.CardAsset = cardAsset;
			manager.RegisterDeckClickedCallback(OnCardClicked);
            card.transform.SetParent(this.transform);
            card.GetComponentInChildren<Canvas>().sortingOrder = this.cards.Count;
            this.cards.Add(card.transform);
            SetCardPositions();
        }

		private void OnCardClicked(CardManager clickedCard) {
			Debug.Log(string.Format("clicked card: {0}", clickedCard.nameText.text));
			if (this.selectedCard == null) {
				// No selected card
				this.selectedCard = clickedCard;
				this.selectedCard.transform.Translate(0, 1, 0);
			} else {
				if (this.selectedCard == clickedCard) {
					// Clicking selected card
					this.selectedCard.transform.Translate(0, -1, 0);
					this.selectedCard = null;
				} else {
					// Clicking different than selected card
					this.selectedCard.transform.Translate(0, -1, 0);
					this.selectedCard = clickedCard;
					this.selectedCard.transform.Translate(0, 1, 0);
				}
			}
		}

        private void SetCardPositions() {
			this.selectedCard = null;
			float spacing = (1f / this.cards.Count) * this.width;
			for (int i = 0; i < this.cards.Count; i++) {
				this.cards[i].position = new Vector3(
					(this.first.position.x + spacing / 2 + (spacing * i)),
					this.first.position.y,
					i * -0.01f
				);
			}
		}
    }
}
