using System;
using System.Collections.Generic;
using Ascendant.ScriptableObjects;
using UnityEngine;

namespace Ascendant.Scripts.Logic {
    public class DeckManager : MonoBehaviour {
        [Header("Assets")]
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
			this.handManager = Container.Get<HandManager>();
            Cards = new Stack<CardAsset>();
            foreach (CardAmountPair cardAmountPair in this.deckAsset.cards) {
                for (int i = 0; i < cardAmountPair.amount; i++) {
                    Cards.Push(cardAmountPair.cardAsset);
                }
            }
			this.Shuffle();
			foreach (CardAsset card in Cards) {
				print(card.name);
			}
			//this.visualDeckManager.RegisterDeckClickedCallback(Draw);
        }

        public GameObject CreateNextCard() {
			//GameObject card = Instantiate(this.cardPrefab, this.transform.position, Quaternion.identity);
			//CardManager cardManager = card.GetComponent<CardManager>();
			//cardManager.CardAsset = Cards.Pop();
			//return card;
			Debug.LogError("what is this for?");
			return null;
        }

        public bool Draw() {
			print("drawing card");
			if (Cards.Count == 0) {
				print("no cards left");
				return false;
			}
            CardAsset cardAsset = Cards.Pop();
			print(cardAsset.name);
			if (!this.handManager.AddCard(cardAsset)) {
				print("didnt draw. hand too full. putting back on deck");
				Cards.Push(cardAsset);
				return false;
			}
			print("drew card");
            if (this.cbOnDrawCard != null) {
                this.cbOnDrawCard.Invoke();
            }
			return true;
        }

		public void Shuffle() {
			CardAsset[] cards = Cards.ToArray();
			for (int i = 0; i < cards.Length; i++) {
				int num = UnityEngine.Random.Range(0, cards.Length);
				CardAsset temp = cards[i];
				cards[i] = cards[num];
				cards[num] = temp;
			}
			Cards = new Stack<CardAsset>(cards);
		}

		// TODO: Replace with events
        public void RegisterDrawCardCallback(Action callback) {
            this.cbOnDrawCard += callback;
        }
    }
}
