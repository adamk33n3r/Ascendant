using System;
using System.Collections.Generic;
using Ascendant.ScriptableObjects;
using Ascendant.Scripts.Cards;
using UnityEngine;

namespace Ascendant.Scripts.Logic {
    public class DeckManager : MonoBehaviour {
        [Header("Assets")]
        //[SerializeField]
        //protected GameObject cardPrefab;
        [SerializeField]
        protected Deck deckAsset;

        [Header("Visual References")]
        [SerializeField]
        protected Visual.HandManager handManager;
        [SerializeField]
        protected Visual.DeckManager visualDeckManager;

        public Stack<CardAsset> Cards { get; protected set; }

        private Action cbOnDrawCard;

        public void Start () {
            this.Cards = new Stack<CardAsset>();
            foreach (CardAmountPair cardAmountPair in this.deckAsset.cards) {
                for (int i = 0; i < cardAmountPair.amount; i++) {
                    this.Cards.Push(cardAmountPair.cardAsset);
                }
            }
            this.visualDeckManager.RegisterDeckClickedCallback(Draw);
        }

        public GameObject CreateNextCard() {
			//GameObject card = Instantiate(this.cardPrefab, this.transform.position, Quaternion.identity);
			//CardManager cardManager = card.GetComponent<CardManager>();
			//cardManager.CardAsset = this.Cards.Pop();
			//return card;
			Debug.LogError("what?");
			return null;
        }

        private void Draw() {
            CardAsset cardAsset = this.Cards.Pop();
            // TODO: convert this to a command
            if (!this.handManager.AddCard(cardAsset)) {
                return;
            }
            this.Cards.Push(cardAsset);
            if (this.cbOnDrawCard != null) {
                this.cbOnDrawCard.Invoke();
            }
        }

        public void RegisterDrawCardCallback(Action callback) {
            this.cbOnDrawCard += callback;
        }
    }
}
