using System.Collections.Generic;

namespace Ascendant.Scripts.Editor.CardEditor.CellType {
    public class Dropdown : CellType {
        public string[] items;

        public Dropdown(string[] items) {
            this.items = items;
        }
    }
}
