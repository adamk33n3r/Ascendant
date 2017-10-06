using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour {
	public void Start () {
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
	}
	
	public void Update () {
	}
}
