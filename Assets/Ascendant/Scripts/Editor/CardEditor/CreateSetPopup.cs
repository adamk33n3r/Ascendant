using Ascendant.ScriptableObjects;
using Ascendant.Scripts.Utils;
using UnityEditor;
using UnityEngine;

namespace Ascendant.Scripts.Editor.CardEditor {
    public class CreateSetPopup : PopupWindowContent {
        private MainWindow main;
        private string setName;
        private Rect buttonRect;

        public CreateSetPopup(Rect rect) {
            this.buttonRect = rect;
        }

        public override void OnGUI(Rect rect) {
            GUILayout.Label("Create a new set", EditorStyles.boldLabel);
            this.setName = EditorGUILayout.TextField("Name", this.setName);
            if (GUILayout.Button("Save", GUILayout.Height(25))) {
                Set newSet = ScriptableObject.CreateInstance<Set>();
                AssetDatabase.CreateAsset(newSet, "Assets/Ascendant/Resources/Sets/" + this.setName + ".asset");
                AssetDatabase.Refresh();
                this.main.Load(newSet);
                this.editorWindow.Close();
            }
        }

        public override void OnOpen() {
            this.main = EditorWindow.GetWindow<MainWindow>();
        }

        public override Vector2 GetWindowSize() {
            return new Vector2(this.buttonRect.width, 75);
        }
    }
}
