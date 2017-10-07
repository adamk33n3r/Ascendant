using System;
using System.Collections.Generic;
using Ascendant.ScriptableObjects;
using UnityEngine;

namespace Ascendant.Scripts.Logic {
    public class DeckManager : MonoBehaviour {
        [Header("Assets")]
        //[SerializeField]
        //protected GameObject cardPrefab;
        [SerializeField]
        protected Deck deckAsset;

        protected HandManager handManager;

        [Header("Visual References")]
        [SerializeField]
        protected Visual.DeckManager visualDeckManager;

        public Stack<CardAsset> Cards { get; protected set; }

        private Action cbOnDrawCard;

		public void Awake() {
			Container.Register(this);
		}

        public void Start () {
			Container.Dump();
			this.handManager = Container.Get<HandManager>();
            this.Cards = new Stack<CardAsset>();
            foreach (CardAmountPair cardAmountPair in this.deckAsset.cards) {
                for (int i = 0; i < cardAmountPair.amount; i++) {
                    this.Cards.Push(cardAmountPair.cardAsset);
                }
            }
            //this.visualDeckManager.RegisterDeckClickedCallback(Draw);
        }

        public GameObject CreateNextCard() {
			//GameObject card = Instantiate(this.cardPrefab, this.transform.position, Quaternion.identity);
			//CardManager cardManager = card.GetComponent<CardManager>();
			//cardManager.CardAsset = this.Cards.Pop();
			//return card;
			Debug.LogError("what is this for?");
			return null;
        }

        public bool Draw() {
            CardAsset cardAsset = this.Cards.Pop();
			bool drawn = !this.handManager.AddCard(cardAsset);
			if (!drawn) {
				this.Cards.Push(cardAsset);
				return drawn;
			}
            if (this.cbOnDrawCard != null) {
                this.cbOnDrawCard.Invoke();
            }
			return true;
        }

        public void RegisterDrawCardCallback(Action callback) {
            this.cbOnDrawCard += callback;
        }
    }
}
