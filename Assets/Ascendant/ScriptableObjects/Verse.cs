using UnityEngine;

namespace Ascendant.ScriptableObjects {
    [CreateAssetMenu(fileName = "Verse", menuName = "Ascendant/Verse", order = 3)]
    public class Verse : ScriptableObject {
        public Set[] sets;
    }
}
