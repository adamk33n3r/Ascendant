namespace Ascendant.Scripts.Editor.CardEditor.CellType {
    public class Range : CellType {
        public int min;
        public int max;

        public Range(int min, int max) {
            this.min = min;
            this.max = max;
        }
    }
}
