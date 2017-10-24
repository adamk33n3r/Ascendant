using System;
using UnityEngine;

using Ascendant.ScriptableObjects;

namespace Ascendant.Logic.Commands {

	/// <summary>
	/// Ask HandManager for selected card.
	/// Check if can place card on location.
	/// Place card.
	/// </summary>
	public class PlayCardCommand : Command {
		private FieldLocationManager fieldLocation;
		private HandManager handManager;

		public PlayCardCommand(FieldLocationManager fieldLocation) {
			this.fieldLocation = fieldLocation;
			this.handManager = Container.Get<HandManager>();
		}

		public override void Execute() {
			// Ask hand manager for its selected card
			// TODO: probably should change this to check for both if there is one to take,
			// and if there is to CHECK if it can be played before ACTUALLY taking it out of
			// the hand. Then you don't have to put it back and rearrange cards.
			CardAsset card = this.handManager.GetSelectedCard();

			// If there isn't one, bail
			if (card == null) {
				return;
			}

			// Actually take the card and tell field location to play card
			card = this.handManager.TakeSelectedCard();
			bool played = this.fieldLocation.PlayCard(card);
			if (!played) {
				this.handManager.AddCard(card);
			}
		}
	}
}
