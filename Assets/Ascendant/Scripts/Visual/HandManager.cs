using System.Collections.Generic;
using Ascendant.ScriptableObjects;
using Ascendant.Scripts.Cards;
using UnityEngine;

namespace Ascendant.Scripts.Visual {
    public class HandManager : MonoBehaviour {
        [Header("Markers")]
        public Transform first;
        public Transform last;

        [Header("Hand Info")]
        public int maxCount = 7;
        public GameObject cardPrefab;

        private readonly List<Transform> cards = new List<Transform>();

		private float width;

		public void Start() {
			this.width = this.last.position.x - this.first.position.x;
		}

        public bool AddCard(CardAsset cardAsset) {
            if (this.cards.Count == this.maxCount) {
                return false;
            }
            GameObject card = Instantiate(this.cardPrefab);
            card.GetComponent<CardManager>().CardAsset = cardAsset;
            card.transform.SetParent(this.transform);
            card.GetComponentInChildren<Canvas>().sortingOrder = this.cards.Count;
            this.cards.Add(card.transform);
            SetCardPositions();
            return true;
        }

        private void SetCardPositions() {
			float spacing = (1f / this.cards.Count) * this.width;
			for (int i = 0; i < this.cards.Count; i++) {
				this.cards[i].position = new Vector3(
					(this.first.position.x + spacing / 2 + (spacing * i)),
					this.first.position.y
				);
			}
		}
    }
}
