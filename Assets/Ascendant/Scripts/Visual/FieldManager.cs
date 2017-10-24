using UnityEngine;
using UnityEngine.EventSystems;

namespace Ascendant.Visual {
	public class FieldManager : BaseBehaviour {
		public bool EnableGeneration = false;
		public GameObject FieldLocationPrefab;
		public int Rows = 4;
		public int Columns = 9;
		public float Width = 3f;
		public float Height = 5f;
		public float Padding = 1f;

        [Header("Prefabs")]
        public GameObject cardPrefab;

		public void Awake() {
			Container.Register(this);
		}

		public void Start() {
			if (!EnableGeneration) return;

			Vector2 offset = new Vector2(
				((Width + Padding) * Columns - Padding) / 2 - Width / 2,
				((Height + Padding) * Rows - Padding) / 2 - Height / 2
			);
			for (int row = 0; row < Rows; row++) {
				for (int column = 0; column < Columns; column++) {
					GameObject fieldPosition = Instantiate(
						FieldLocationPrefab,
						this.transform.position + new Vector3(
							-offset.x + (Width + Padding) * column,
							-offset.y + (Height + Padding) * row,
							0
						),
						Quaternion.identity,
						this.transform
					);
					fieldPosition.name = string.Format("{0},{1}", column, row);
				}
			}


			// Keep code. Reference for spacing elements in canvas
			/*
			RectTransform rect = GetComponent<RectTransform>();
			Vector2 bottomLeft = rect.offsetMin;
			Vector2 topRight = rect.offsetMax;
			Vector2 topLeft = new Vector2(bottomLeft.x, topRight.y);
			// Vector2 bottomRight = new Vector2(topRight.x, bottomLeft.y);

			int childCount = this.transform.childCount;
			float width = Mathf.Abs(topRight.x - topLeft.x);
			int childNum = 0;
			float spacing = (1f / childCount) * width;
			foreach (Transform child in this.transform) {
				child.position = Vector3.Scale(
					new Vector3(
						(topLeft.x + spacing / 2 + (spacing * childNum)) + this.transform.position.x / this.transform.localScale.x,
						(topLeft.y + bottomLeft.y) / 2 + this.transform.position.y / this.transform.localScale.y
					),
					this.transform.localScale
				);
				childNum++;
			}
			*/
		}
	}
}
