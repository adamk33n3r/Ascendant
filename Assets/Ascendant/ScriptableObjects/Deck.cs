using System;
using UnityEngine;
using System.Collections.Generic;

namespace Ascendant.ScriptableObjects {
    [Serializable]
    public class CardAmountPair {
        public CardAsset cardAsset;
        public int amount;
    }

    [CreateAssetMenu(fileName = "Deck", menuName = "Ascendant/Deck", order = 1)]
    public class Deck : ScriptableObject {
        [HideInInspector]
        public List<CardAmountPair> cards;
    }
}