using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ascendant.Scripts.Visual {
	public class FieldManager : MonoBehaviour {
		private HandManager handManager;

		public void Awake() {
			Container.Register(this);
		}

		public void Start() {
			this.handManager = Container.Get<HandManager>();

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

		public void Update() {
		}

		public void OnFieldLocationClicked(GameObject clicked) {
			print("clicked on field location");
			print(clicked.name);

			// Need to move this to logic cause it needs to know
			// VisualField -> LogicHand -> VisualHand
			Cards.CardManager selectedCard = this.handManager.TakeSelectedCard();

			// If there isn't a selected card do nothing
			if (selectedCard == null) {
				return;
			}

			// If there is already a card here do nothing
			// TODO: or do action on card when thats a thing. although that would be clicking on the card
			if (clicked.GetComponentInChildren<Cards.CardManager>() != null) {
				print("already card here");
				return;
			}

			// Remove card from hand

			selectedCard.transform.SetParent(clicked.transform);
			selectedCard.transform.localPosition = Vector3.zero;
		}
	}
}
