using Ascendant.Scripts.Enums;
using UnityEngine;

namespace Ascendant.ScriptableObjects {
    [CreateAssetMenu(fileName = "Card", menuName = "Ascendant/Card", order = 2)]
    public class CardAsset : ScriptableObject {
        public string displayName;
        [Range(0, 100)]
        public int cost;
        [Range(0, 100)]
        public int attack;
        [Range(0, 100)]
        public int defense;
        [Range(0, 100)]
        public int delay;

        [HideInInspector]
        public SuperType superType;
        [HideInInspector]
        public SubType subType;

        public Biome biome;
        public Rarity rarity;

        [TextArea(3, 10)]
        public string abilities;
        [TextArea(3, 10)]
        public string flavor;
    }
}