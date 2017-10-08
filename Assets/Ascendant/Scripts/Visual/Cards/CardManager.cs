using System;
using UnityEngine;
using UnityEngine.UI;
using Ascendant.ScriptableObjects;
using Ascendant.Scripts.Enums;

namespace Ascendant.Scripts.Visual.Cards {
    public class CardManager : MonoBehaviour {
        public CardAsset CardAsset {
            set {
                Setup(value);
            }
        }

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

        public void RegisterDeckClickedCallback(Action<CardManager> callback) {
            this.cbCardClicked += callback;
        }

		public void UnregisterDeckClickedCallback(Action<CardManager> callback) {
			this.cbCardClicked -= callback;
		}

		public void OnClick() {
			if (this.cbCardClicked != null) {
				this.cbCardClicked(this);
			}
		}

        private void Setup (CardAsset cardAsset) {
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
    }
}
