using UnityEngine;

namespace Ascendant.ScriptableObjects {
    [CreateAssetMenu(fileName = "Set", menuName = "Ascendant/Set", order = 4)]
    public class Set : ScriptableObject {
        public CardAsset[] cardAssets = new CardAsset[0];
    }
}
