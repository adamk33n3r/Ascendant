using System.Collections.Generic;
using Ascendant.Scripts.Editor.CardEditor.CellType;
using UnityEditor;
using UnityEngine;

namespace Ascendant.Scripts.Editor.CardEditor {
    public class Table {
        public IList<IList<Cell>> Rows {
            get { return this.rows; }
        }

        private readonly IList<Column> headers;
        private readonly IList<IList<Cell>> rows;
        private float columnWidth;
        private Vector2 scrollPos;
        private float totalScrollHeight = 0f;

        public Table(float width) : this(width, new List<Column>(), new List<IList<Cell>>()) {
        }

        public Table(float width, IList<Column> headers): this(width, headers, new List<IList<Cell>>()) {
        }

        public Table(float width, IList<Column> headers, IList<IList<Cell>> rows) {
            this.headers = headers;
            this.rows = rows;
            SetColumnWidth(width);
        }

        public void AddRow(params Cell[] cells) {
            this.rows.Add(cells);
        }

        public void Clear() {
            this.rows.Clear();
        }

        public void ScrollToTop() {
            this.scrollPos = new Vector2(this.scrollPos.x, 0);
        }

        public void ScrollToBottom() {
            this.scrollPos = new Vector2(this.scrollPos.x, this.totalScrollHeight);
        }

        public void Render(float width) {
            SetColumnWidth(width);

            RenderHeaders();
            this.totalScrollHeight = 0;
            this.scrollPos = EditorGUILayout.BeginScrollView(this.scrollPos);
            foreach (IList<Cell> row in this.rows) {
                RenderRow(row);
                this.totalScrollHeight += GUILayoutUtility.GetLastRect().height;
            }
            EditorGUILayout.EndScrollView();
        }

        private void RenderHeaders() {
            EditorGUILayout.BeginHorizontal();
            foreach (Column header in this.headers) {
                GUILayout.Button(header.name,  EditorStyles.boldLabel, GUILayout.Width(this.columnWidth));
            }
            EditorGUILayout.EndHorizontal();
        }

        private void RenderRow(IList<Cell> row) {
            EditorGUILayout.BeginHorizontal();
            for (int i = 0; i < this.headers.Count; i++) {
                if (i >= row.Count) {
                    continue;
                }
                Column column = this.headers[i];
                Cell cell = row[i];
                if (column.type is Int) {
                    cell.data = EditorGUILayout.IntField((int) cell.data, GUILayout.Width(this.columnWidth));
                } else if (column.type is Text) {
                    Text textType = (Text) column.type;
                    if (textType.area) {
                        cell.data = EditorGUILayout.TextArea((string) cell.data, GUILayout.Width(this.columnWidth));
                    } else {
                        cell.data = EditorGUILayout.TextField((string) cell.data, GUILayout.Width(this.columnWidth));
                    }
                } else if (column.type is Dropdown) {
                    Dropdown dropdown = (Dropdown) column.type;
                    cell.data = EditorGUILayout.Popup((int) cell.data, dropdown.items, GUILayout.Width(this.columnWidth));
                } else if (column.type is Range) {
                    cell.data = EditorGUILayout.IntSlider((int) cell.data, ((Range) column.type).min,
                        ((Range) column.type).max, GUILayout.Width(this.columnWidth));
                } else {
                    cell.data = EditorGUILayout.TextField((string) cell.data, GUILayout.Width(this.columnWidth));
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        private void SetColumnWidth(float width) {
            this.columnWidth = (width - this.headers.Count * 4 - 20) / this.headers.Count;
        }
    }
}
