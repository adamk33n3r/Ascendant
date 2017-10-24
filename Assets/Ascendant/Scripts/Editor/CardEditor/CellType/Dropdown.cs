using System.Collections.Generic;

namespace Ascendant.Editor.CardEditor.CellType {
    public class Dropdown : CellType {
        public string[] items;

        public Dropdown(string[] items) {
            this.items = items;
        }
    }
}
