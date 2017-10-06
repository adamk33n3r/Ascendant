using System;
using System.Collections.Generic;
using System.Linq;
using Ascendant.ScriptableObjects;
using UnityEditor;
using UnityEngine;

using Ascendant.Scripts.Enums;
using Ascendant.Scripts.Utils;

namespace Ascendant.Scripts.Editor.CardEditor {
    public class MainWindow : EditorWindow {
        private Set selectedSet;
        private Table table;
        private Rect loadButtonRect;
        private Rect createSetButtonRect;
        private bool renamingSet;

        private IOrderedEnumerable<ScriptableObjects.SuperType> superTypes;
        private IOrderedEnumerable<ScriptableObjects.SubType> subTypes;
        private string[] superTypeNames;
        private string[] subTypeNames;

        public void OnEnable() {
            this.selectedSet = null;
            this.renamingSet = false;
            this.superTypes = Asset.GetAllOfType<ScriptableObjects.SuperType>().OrderBy(superType => superType.name);
            this.superTypeNames = this.superTypes.Select(type => type.name).ToArray();
            this.subTypes = Asset.GetAllOfType<ScriptableObjects.SubType>().OrderBy(subType => subType.name);
            this.subTypeNames = this.subTypes.Select(type => type.name).ToArray();
            Column superTypeCol = new Column("Super Type", new CellType.Dropdown(this.superTypeNames));
            Column subtypeCol = new Column("Sub Type", new CellType.Dropdown(this.subTypeNames));
            this.table = new Table(this.position.width, new[] {
                new Column("ID", new CellType.Text()), 
                new Column("Name", new CellType.Text()), 
                new Column("Cost", new CellType.Int()), 
                new Column("Attack", new CellType.Int()), 
                new Column("Defense", new CellType.Int()), 
                new Column("Delay", new CellType.Int()), 
                superTypeCol,
                subtypeCol,
                new Column("Biome", new CellType.Dropdown(Enum.GetNames(typeof(Biome)))), 
                new Column("Rarity", new CellType.Dropdown(Enum.GetNames(typeof(Rarity)))), 
                new Column("Abilities", new CellType.Text(true)), 
                new Column("Flavor", new CellType.Text(true)), 
            });
            if (this.selectedSet != null) {
                Load(this.selectedSet);
            }
        }

        [MenuItem("Ascendant/Card Editor")]
        public static void ShowWindow() {
            EditorWindow window = GetWindow<MainWindow>("Card Editor");
            window.minSize = new Vector2(600, 300);
            window.Show();
        }

        //public int selected;
        public void OnGUI() {
			//this.selected = GUILayout.Toolbar(this.selected, new[] { "first", "second", "third", "fourth" });
			EditorStyles.textField.wordWrap = true;

            if (this.selectedSet != null) {
                EditorGUILayout.BeginHorizontal();
                {
                    if (this.renamingSet) {
                        GUILayout.Label("Set: ", EditorStyles.boldLabel);
                        this.selectedSet.name = EditorGUILayout.TextField(this.selectedSet.name);
                        if (GUILayout.Button("Save")) {
                            this.renamingSet = false;
                            AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(this.selectedSet), this.selectedSet.name);
                            AssetDatabase.Refresh();
                        }
                    } else {
                        GUILayout.Label("Set: " + this.selectedSet.name, EditorStyles.boldLabel);
                    }
                    GUILayout.FlexibleSpace();
                    if (GUILayout.Button("Rename Set")) {
                        this.renamingSet = true;
                    }
                    if (GUILayout.Button("Delete Set")) {
                        if (EditorUtility.DisplayDialog("Delete Set",
                            "Are you sure you want to delete this set and all of its cards?", "I'm sure! Burninate!", "Now that I think about it...")) {
                            foreach (CardAsset card in this.selectedSet.cardAssets) {
                                Asset.Delete(card);
                            }
                            Asset.Delete(this.selectedSet);
                            this.selectedSet = null;
                        }
                    }
                }
                EditorGUILayout.EndHorizontal();

                this.table.Render(this.position.width);
            }

            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Add New Card", GUILayout.Height(25))) {
                    Debug.Log("add new card");
                    this.table.ScrollToBottom();
                    this.table.AddRow(
                        new Cell("NBT001"),
                        new Cell(""),
                        new Cell(0),
                        new Cell(0),
                        new Cell(0),
                        new Cell(0),
                        new Cell(0),
                        new Cell(0),
                        new Cell(0),
                        new Cell(0),
                        new Cell(""),
                        new Cell("")
                    );
                }
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Load Set", GUILayout.Height(25))) {
                    PopupWindow.Show(this.loadButtonRect, new SetChooserWindow());
                }
                if (Event.current.type == EventType.Repaint) {
                    this.loadButtonRect = GUILayoutUtility.GetLastRect();
                }
                if (this.selectedSet != null) {
                    if (GUILayout.Button("Save Set", GUILayout.Height(25))) {
                        Save();
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Create New Set", /*new GUIStyle("OL Title"), */GUILayout.Height(25))) {
                    PopupWindow.Show(this.createSetButtonRect, new CreateSetPopup(this.createSetButtonRect));
                }
                if (Event.current.type == EventType.Repaint) {
                    this.createSetButtonRect = GUILayoutUtility.GetLastRect();
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        public void Load(Set set) {
            this.selectedSet = set;
            Debug.Log("set was clicked " + set.name);
            this.table.Clear();
            float i = 0;
            foreach (CardAsset card in set.cardAssets) {
                UpdateProgressBar(card.name, i / set.cardAssets.Length);
                int superTypePosition = 0;
                if (card.superType != null) {
                    superTypePosition = Array.IndexOf(this.superTypeNames, card.superType.name);
                }
                int subTypePosition = 0;
                if (card.subType != null) {
                    subTypePosition = Array.IndexOf(this.subTypeNames, card.subType.name);
                }
                this.table.AddRow(
                    new Cell(card.name),
                    new Cell(card.displayName),
                    new Cell(card.cost),
                    new Cell(card.attack),
                    new Cell(card.defense),
                    new Cell(card.delay),
                    new Cell(superTypePosition),
                    new Cell(subTypePosition),
                    new Cell((int) card.biome),
                    new Cell((int) card.rarity),
                    new Cell(card.abilities),
                    new Cell(card.flavor)
                );
                i++;
            }
            EditorUtility.ClearProgressBar();
        }

        private void UpdateProgressBar(string card, float progress) {
            EditorUtility.DisplayProgressBar("Loading cards from set", "Loading " + card, progress);
        }

        private void Save() {
            Debug.Log("clicked on save");
            for (int i = 0; i < this.table.Rows.Count; i++) {
                bool newCard = i >= this.selectedSet.cardAssets.Length;
                IList<Cell> row = this.table.Rows[i];

                CardAsset card;
                if (newCard) {
                    card = CreateInstance<CardAsset>();
                } else {
                    card = this.selectedSet.cardAssets[i];
                }
                card.displayName = (string) row[1].data;
                card.cost = (int) row[2].data;
                card.attack = (int) row[3].data;
                card.defense = (int) row[4].data;
                card.delay = (int) row[5].data;


                int superTypePos = (int) row[6].data;
                int subTypePos = (int) row[7].data;
                card.superType = this.superTypes.ToArray()[superTypePos];
                card.subType = this.subTypes.ToArray()[subTypePos];

                card.biome = (Biome) row[8].data;
                card.rarity = (Rarity) row[9].data;
                card.abilities = (string) row[10].data;
                card.flavor = (string) row[11].data;

                if (newCard) {
                    card.name = (string) row[0].data;
                    string path = "Assets/Ascendant/Resources/Cards";
                    if (!AssetDatabase.IsValidFolder(path)) {
                        AssetDatabase.CreateFolder(path, this.selectedSet.name);
                    }
                    path += "/" + this.selectedSet.name;
                    if (!AssetDatabase.IsValidFolder(path)) {
                        AssetDatabase.CreateFolder(path, card.biome.ToString());
                    }
                    path += "/" + card.biome;
                    AssetDatabase.CreateAsset(card, path + "/" + card.name + ".asset");
                    List<CardAsset> cards = new List<CardAsset>(this.selectedSet.cardAssets) { card };
                    this.selectedSet.cardAssets = cards.ToArray();
                    continue;
                }

                // If name changed, rename asset
                if (card.name != (string) row[0].data) {
                    AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(card), (string) row[0].data);
                }
                EditorUtility.SetDirty(card);
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
