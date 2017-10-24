namespace Ascendant.Editor.CardEditor {
    public class Cell {
        public object data;

        public Cell(object data) {
            this.data = data;
        }
        public Cell(ref object data) {
            this.data = data;
        }
    }
}
