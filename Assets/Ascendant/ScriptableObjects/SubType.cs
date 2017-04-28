using UnityEngine;

namespace Ascendant.ScriptableObjects {
    [CreateAssetMenu(fileName = "SubType", menuName = "Ascendant/SubType", order = 6)]
    public class SubType : ScriptableObject {
        public SuperType superType;
    }
}
