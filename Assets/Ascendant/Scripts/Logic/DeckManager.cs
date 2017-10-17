using System;
using System.Collections.Generic;
using Ascendant.ScriptableObjects;
using UnityEngine;

namespace Ascendant.Scripts.Logic {
    public class DeckManager : MonoBehaviour {
        [Header("Assets")]
        [SerializeField]
        protected Deck deckAsset;
		// TODO: Look into loading cards and decks from google docs

        protected HandManager handManager;

        [Header("Visual References")]
        [SerializeField]
        protected Visual.DeckManager visualDeckManager;

		protected Stack<CardAsset> Cards = new Stack<CardAsset>();
		public int CardCount { get { return Cards.Count; } }

		public void Awake() {
			Container.Register(this);
		}

        public void Start () {
			this.handManager = Container.Get<HandManager>();
            foreach (CardAmountPair cardAmountPair in this.deckAsset.cards) {
                for (int i = 0; i < cardAmountPair.amount; i++) {
                    Cards.Push(cardAmountPair.cardAsset);
                }
            }
			Shuffle();
			print(Cards.Count);
			//foreach (CardAsset card in Cards) {
			//	print(card.name);
			//}
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
			print(Cards.Count);
			if (Cards.Count == 0) {
				print("no cards left");
				return false;
			}
            CardAsset cardAsset = Cards.Pop();
			print(cardAsset.name);
			// TODO: maybe move this into the hand manager to listen the event
			if (!this.handManager.AddCard(cardAsset)) {
				print("didnt draw. hand too full. putting back on deck");
				Cards.Push(cardAsset);
				return false;
			}
			print("drew card");
			EventManager.Fire(Events.CARD_DRAWN, cardAsset);
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
    }
}
