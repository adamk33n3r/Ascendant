using System;
using UnityEngine;

using Ascendant.ScriptableObjects;

namespace Ascendant.Scripts.Logic.Commands {
	public class PlayCardCommand : AsyncCommand {
		private FieldLocationManager fieldLocation;
		private HandManager handManager;

		public PlayCardCommand(FieldLocationManager fieldLocation) {
			this.fieldLocation = fieldLocation;
			this.handManager = Container.Get<HandManager>();
		}

		public override void Execute(Action doneCallback) {
			// Ask hand manager for its selected card
			// TODO: probably should change this to check for both if there is one to take,
			// and if there is to CHECK if it can be played before ACTUALLY taking it out of
			// the hand.
			CardAsset card = this.handManager.TakeSelectedCard();

			// If there isn't one, bail
			if (card == null) {
				doneCallback();
				return;
			}

			// Tell field location to play card
			bool played = this.fieldLocation.PlayCard(card);
			if (!played) {
				this.handManager.AddCard(card);
			}
			// If it says you can't, undo stuff
			doneCallback();
		}
	}
}
