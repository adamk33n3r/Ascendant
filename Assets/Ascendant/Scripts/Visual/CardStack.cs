using UnityEngine;
using Ascendant.Scripts.Logic;
using TMPro;

namespace Ascendant.Scripts.Visual {
	public class CardStack : BaseBehaviour {
        public float thicknessOfOneCard = 0.012f;
		public bool down = true;
		public TextMeshProUGUI cardCountText;

		private int cardsInDeck;
		public int CardsInDeck {
			get { return this.cardsInDeck; }
			set {
				this.cardsInDeck = value;
				this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -this.thicknessOfOneCard * value);
				if (this.cardCountText != null) {
					this.cardCountText.text = this.cardsInDeck.ToString();
				}
			}
		}

		private void Start () {
			int count = Container.Get<Logic.DeckManager>().CardCount;
			// TODO: actual cards in deck?
			CardsInDeck = down ? count : 0;
			EventManager.Listen(down ? Events.CARD_DRAWN : Events.CARD_DISCARDED, (data) => {
				CardsInDeck += down ? -1 : 1;
			});
		}
	}
}
