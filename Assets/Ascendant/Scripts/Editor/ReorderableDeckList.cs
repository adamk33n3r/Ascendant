using System;
using System.Collections.Generic;
using Ascendant.ScriptableObjects;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Ascendant.Scripts.Editor {
    [CustomEditor(typeof(Deck))]
    public class ReorderableDeckList : UnityEditor.Editor {
        private ReorderableList reorderableList;

        private Deck deck {
            get { return this.target as Deck; }
        }

        private void OnEnable() {
            // Look up source for button on click editor?
            this.reorderableList = new ReorderableList(this.deck.cards, typeof(CardAsset), true, true, true, true);

            this.reorderableList.drawHeaderCallback += DrawHeader;
            this.reorderableList.drawElementCallback += DrawElement;
            this.reorderableList.onAddCallback += AddItem;
            this.reorderableList.onRemoveCallback += RemoveItem;
        }

        private void OnDisable() {
            // Make sure we don't get memory leaks etc.
            this.reorderableList.drawHeaderCallback -= DrawHeader;
            this.reorderableList.drawElementCallback -= DrawElement;

            this.reorderableList.onAddCallback -= AddItem;
            this.reorderableList.onRemoveCallback -= RemoveItem;
        }

        private void DrawHeader(Rect rect) {
            GUI.Label(rect, "Cards");
        }

        private void DrawElement(Rect rect, int index, bool active, bool focused) {
            CardAmountPair cardAmount = this.deck.cards[index];
            EditorGUI.BeginChangeCheck();
            cardAmount.cardAsset = EditorGUI.ObjectField(new Rect(rect.x, rect.y, rect.width - 50 - 5 - 5, rect.height), "", cardAmount.cardAsset, typeof(CardAsset), false) as CardAsset;
//            cardAmount.amount = EditorGUI.IntField(new Rect(rect.x, rect.y, 30, rect.height), cardAmount.amount);
            cardAmount.amount = EditorGUI.IntField(new Rect(rect.x + rect.width - 5 - 50, rect.y, 50, rect.height), cardAmount.amount);
            if (EditorGUI.EndChangeCheck()) {
                EditorUtility.SetDirty(this.target);
            }
        }

        private void AddItem(ReorderableList list) {
            this.deck.cards.Add(new CardAmountPair());

            EditorUtility.SetDirty(this.target);
        }

        private void RemoveItem(ReorderableList list) {
            this.deck.cards.RemoveAt(list.index);

            EditorUtility.SetDirty(this.target);
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            // Actually draw the list in the inspector
            this.reorderableList.DoLayoutList();
        }
    }
}
