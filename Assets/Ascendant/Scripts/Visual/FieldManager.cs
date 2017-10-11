using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Ascendant.Scripts.Logic.Commands;

namespace Ascendant.Scripts.Visual {
	public class FieldManager : MonoBehaviour {
		public GameObject FieldLocationPrefab;
		public int Rows = 4;
		public int Columns = 9;
		public float Width = 3f;
		public float Height = 5f;
		public float Padding = 1f;

		//private Logic.HandManager handManager;
		private CommandBus bus;

		public void Awake() {
			Container.Register(this);
		}

		public void Start() {
			this.bus = Container.Get<CommandBus>();
			//this.handManager = Container.Get<Logic.HandManager>();

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
					fieldPosition.name = string.Format("{0},{1}", row, column);
					EventTrigger eventTrigger = fieldPosition.GetComponent<EventTrigger>();
					EventTrigger.Entry entry = new EventTrigger.Entry();
					entry.eventID = EventTriggerType.PointerClick;
					entry.callback = new EventTrigger.TriggerEvent();
					entry.callback.AddListener((eventData) => {
						OnFieldLocationClicked(fieldPosition);
					});
					eventTrigger.triggers.Add(entry);
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

		public FieldLocationManager GetLocation() {
			return null;
		}

		public void OnFieldLocationClicked(GameObject clicked) {
			print("clicked on field location");
			print(clicked.name);

			// VisualField -> LogicHand -> LogicField -> VisualField
			// TODO: Make this a DrawCommand?
			//bus.Add(new PlayCardCommand());
			Cards.CardManager selectedCard = null;//this.handManager.TakeSelectedCard();

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
