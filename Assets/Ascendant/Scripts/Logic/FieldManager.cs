using System.Collections.Generic;

namespace Ascendant.Scripts.Logic {
	public class FieldManager : BaseBehaviour {
		public int Rows = 4;
		public int Columns = 9;

		private struct Pos {
			public int x, y;
		}

		private Dictionary<Pos, FieldLocationManager> map;

		private void Awake() {
			Container.Register(this);

			// Create field location objects
			this.map = new Dictionary<Pos, FieldLocationManager>(Rows * Columns);
			for (int row = 0; row < Rows; row++) {
				for (int col = 0; col < Columns; col++) {
					this.map[new Pos() { x = col, y = row }] = new FieldLocationManager();
				}
			}
		}

		public FieldLocationManager GetLocation(int x, int y) {
			return this.map[new Pos(){ x = x, y = y }];
		}
	}
}
