using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Ascendant.ScriptableObjects;
using Ascendant.Scripts.Enums;

namespace Ascendant.Scripts.Visual.Cards {
    public class CardManager : BaseBehaviour, IPointerClickHandler {
        public CardAsset CardAsset {
			get { return this.cardAsset; }
            set {
				this.cardAsset = value;
                Setup(value);
            }
        }
		private CardAsset cardAsset;

        [Header("Text Component References")]
        public Text nameText;
        public Text costText;
        public Text abilitiesText;
        public Text flavorText;
        public Text attackText;
        public Text defenseText;
        [Header("Graphic References")]
        public Image cardGraphic;

		private Action<CardManager> cbCardClicked;

        public void RegisterCardClickedCallback(Action<CardManager> callback) {
            this.cbCardClicked += callback;
        }

		public void UnregisterCardClickedCallback(Action<CardManager> callback) {
			this.cbCardClicked -= callback;
		}

        private void Setup(CardAsset cardAsset) {
            // Setup texts
            this.nameText.text = cardAsset.name;
            this.costText.text = cardAsset.cost.ToString();
            this.abilitiesText.text = cardAsset.abilities;
            this.flavorText.text = cardAsset.flavor;
            this.attackText.text = cardAsset.attack.ToString();
            this.defenseText.text = cardAsset.defense.ToString();

            // Setup graphics
            this.cardGraphic.color = cardAsset.biome.ToColor();
        }

		#region Events
		public void OnPointerClick(PointerEventData eventData) {
			if (this.cbCardClicked != null) {
				this.cbCardClicked(this);
			}
		}
		#endregion
	}
}
