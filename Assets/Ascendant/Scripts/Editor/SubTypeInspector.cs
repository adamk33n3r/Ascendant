using System.Linq;
using Ascendant.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace Ascendant.Scripts.Editor {
    [CustomEditor(typeof(SubType))]
    public class SubTypeInspector : UnityEditor.Editor {
        private SubType subType;

        private void OnEnable() {
            this.subType = (SubType) this.target;

            // Set default super type to Creature
            if (this.subType.superType == null) {
                SuperType[] superTypes = Resources.FindObjectsOfTypeAll<SuperType>();
                SuperType creatureSuperType = superTypes.First(type => type.name == "Creature");
                this.subType.superType = creatureSuperType;
            }
        }
    }
}
