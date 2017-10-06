namespace Ascendant.Scripts.Editor.CardEditor {
    public class Column {
        public string name;
        public CellType.CellType type;

        public Column(string name, CellType.CellType type) {
            this.name = name;
            this.type = type;
        }
    }
}
