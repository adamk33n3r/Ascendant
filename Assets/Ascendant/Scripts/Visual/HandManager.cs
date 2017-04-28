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
            float span = this.last.position.x - this.first.position.x;
            Vector3 dist = new Vector3(span / (this.cards.Count - 1), 0, 0);
            this.cards[0].position = this.first.position;
            for (int i = 1; i < this.cards.Count; i++) {
                this.cards[i].position = this.cards[i - 1].position + dist;
            }
        }
    }
}
