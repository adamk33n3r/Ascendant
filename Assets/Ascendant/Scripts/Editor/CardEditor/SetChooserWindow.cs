using Ascendant.ScriptableObjects;
using Ascendant.Utils;
using UnityEditor;
using UnityEngine;

namespace Ascendant.Editor.CardEditor {
    public class SetChooserWindow : PopupWindowContent {
        private MainWindow main;
        private Set[] sets;

        public override void OnGUI(Rect rect) {
            GUILayout.Label("Pick a set to load", EditorStyles.boldLabel);
            foreach (Set set in this.sets) {
                if (GUILayout.Button(set.name)) {
                    this.main.Load(set);
                    this.editorWindow.Close();
                }
            }
        }

        public override void OnOpen() {
            this.main = EditorWindow.GetWindow<MainWindow>();
            Asset.Init();
            this.sets = Asset.GetAllOfType<Set>();
        }
    }
}
