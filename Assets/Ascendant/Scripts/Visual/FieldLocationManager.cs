using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

using Ascendant.Scripts.Logic.Commands;

namespace Ascendant.Scripts.Visual {
	public class FieldLocationManager : BaseBehaviour, IPointerClickHandler {
		public int X { get; protected set; }
		public int Y { get; protected set; }

		private CommandBus bus;
		private FieldManager fieldManager;
		private Logic.FieldLocationManager logic;

		private List<Cards.CardManager> cards = new List<Cards.CardManager>();

		private void Start() {
			string[] pos = name.Split(',');
			X = int.Parse(pos[0]);
			Y = int.Parse(pos[1]);
			this.bus = Container.Get<CommandBus>();
			this.fieldManager = Container.Get<FieldManager>();
			this.logic = Container.Get<Logic.FieldManager>()
				.GetLocation(X, Y);
			this.logic.OnCardPlayed += OnCardPlayed;
		}

		private void OnCardPlayed(ScriptableObjects.CardAsset cardAsset) {
			print("card was just played", cardAsset.name);
            GameObject card = Instantiate(this.fieldManager.cardPrefab);
			card.transform.SetParent(this.transform, false);
			Cards.CardManager manager = card.GetComponent<Cards.CardManager>();
			manager.CardAsset = cardAsset;
			//manager.RegisterCardClickedCallback(OnCardClicked);
            this.cards.Add(manager);
		}

		public void OnPointerClick(PointerEventData eventData) {
			print("clicked on field location", this.name);

			this.bus.Add(new PlayCardCommand(this.logic));
		}
	}
}
