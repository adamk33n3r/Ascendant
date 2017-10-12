using Ascendant.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ascendant.Scripts.Logic {
	public class FieldLocationManager {
		public CardAsset[] Cards { get; protected set; }

		// TODO: Change this to use the Event system since it's likely that other
		// field locations would like to know for adjacency stuff
		public event System.Action<CardAsset> OnCardPlayed;

		private Visual.FieldLocationManager visual;
		private List<CardAsset> cards = new List<CardAsset>();

		public void Start() {
			//this.visual = Container.Get<Visual.FieldManager>().GetLocation();
		}

		public bool PlayCard(CardAsset card) {
			Debug.Log("trying to play card " + card.name);

			// TODO: logic for valid placement of cards
			// Like we'll probably need to allow for "modifier" cards?
			// Ask Mike. The "modifiers" could just be attached to the card.
			if (this.cards.Count > 0) {
				return false;
			}

			this.cards.Add(card);
			OnCardPlayed(card);
			return true;
		}
	}
}
