using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Ascendant.ScriptableObjects;
using Ascendant.Enums;
using DG.Tweening;

namespace Ascendant.Visual.Cards {
    public class CardManager : BaseBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
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

		private float hoverDist = 1.5f;
		private float animationTime = 0.25f;

		private Action<CardManager> cbCardClicked;

        public void RegisterCardClickedCallback(Action<CardManager> callback) {
            this.cbCardClicked += callback;
        }

		public void UnregisterCardClickedCallback(Action<CardManager> callback) {
			this.cbCardClicked -= callback;
		}

		private void Start() {
			print("card spawned");
			// Get actual height of card
			RectTransform rectTransform = this.transform.GetComponentInChildren<RectTransform>();
			Rect rect = rectTransform.rect;
			this.hoverDist = (rect.height * rectTransform.localScale.y) / 2;
			print("hover dist: " + this.hoverDist);
		}

		private void Setup(CardAsset cardAsset) {
            // Setup texts
            this.nameText.text = cardAsset.displayName.Length > 0 ? cardAsset.displayName : cardAsset.name;
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

		public void OnPointerEnter(PointerEventData eventData) {
			this.transform.DOComplete();
			this.transform.DOMoveY(this.transform.position.y + this.hoverDist, this.animationTime);
			this.transform.DOScale(2, this.animationTime);
		}

		public void OnPointerExit(PointerEventData eventData) {
			this.transform.DOComplete();
			this.transform.DOMoveY(this.transform.position.y - this.hoverDist, this.animationTime);
			this.transform.DOScale(1, this.animationTime);
		}
		#endregion
	}
}
