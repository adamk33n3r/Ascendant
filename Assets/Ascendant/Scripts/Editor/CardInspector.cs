using System;
using System.Linq;
using Ascendant.ScriptableObjects;
using NUnit.Framework.Constraints;
using UnityEditor;
using UnityEngine;

namespace Ascendant.Scripts.Editor {
    [CustomEditor(typeof(CardAsset))]
    public class CardInspector : UnityEditor.Editor {
        private SuperType[] superTypes;
        private string[] superTypeNames;
        private int superTypeIndex;

        private SubType[] subTypes;
        private string[] subTypeNames;
        private int subTypeIndex;

        private CardAsset cardAsset;

        public void OnEnable() {
            this.cardAsset = (CardAsset) this.target;
            this.superTypes = Resources.LoadAll<SuperType>("SuperTypes");
            this.superTypeNames = this.superTypes.Select(type => type.name).ToArray();
            this.superTypeIndex = Array.IndexOf(this.superTypes, this.cardAsset.superType);
            if (this.superTypeIndex < 0) {
                this.superTypeIndex = 0;
            }
            GetFilteredSubTypes();
        }

        public override void OnInspectorGUI() {
            EditorGUI.BeginChangeCheck();
            DrawDefaultInspector();
            GetFilteredSubTypes();
            this.superTypeIndex = EditorGUILayout.Popup("SuperType", this.superTypeIndex, this.superTypeNames);
            this.subTypeIndex = EditorGUILayout.Popup("SubType", this.subTypeIndex, this.subTypeNames);
            this.cardAsset.superType = this.superTypeIndex <= this.superTypes.Length - 1 ? this.superTypes[this.superTypeIndex] : null;
            this.cardAsset.subType = this.subTypeIndex <= this.subTypes.Length - 1 ? this.subTypes[this.subTypeIndex] : null;

            if (EditorGUI.EndChangeCheck()) {
                EditorUtility.SetDirty(this.target);
            }
        }

        private void GetFilteredSubTypes() {
            this.subTypes = Resources.LoadAll<SubType>("SubTypes");
            foreach (SubType subType in this.subTypes) {
                if (subType.superType == null) {
                    Debug.Log(subType);
                }
            }
            this.subTypes = this.subTypes
                .Where(subtype => subtype.superType.name == this.superTypes[this.superTypeIndex].name)
                .ToArray();
            this.subTypeNames = this.subTypes.Select(type => type.name).ToArray();
            this.subTypeIndex = Array.IndexOf(this.subTypes, this.cardAsset.subType);
            if (this.subTypeIndex < 0) {
                this.subTypeIndex = 0;
            }
        }
    }
}
