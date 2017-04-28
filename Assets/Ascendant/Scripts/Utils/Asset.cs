using Ascendant.ScriptableObjects;
using UnityEngine;

namespace Ascendant.Scripts.Utils {
    public static class Asset {
        public static void Init() {
            Resources.LoadAll<Set>("Sets");
//            Resources.LoadAll<SuperType>("SuperTypes");
//            Resources.LoadAll<SubType>("SubTypes");
        }

        public static T[] GetAllOfType<T>() where T : Object {
            return Resources.FindObjectsOfTypeAll<T>();
        }
    }
}
