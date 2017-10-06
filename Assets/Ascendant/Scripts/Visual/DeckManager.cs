using System;
using Ascendant.Scripts.Logic.Commands;
using UnityEngine;

namespace Ascendant.Scripts.Visual {
    public class DeckManager : MonoBehaviour {
        public float thicknessOfOneCard = 0.012f;
        public Logic.DeckManager deckManager;

        private Action cbDeckClicked;

        private int cardsInDeck;
        public int CardsInDeck {
            get { return this.cardsInDeck; }
            set {
                this.cardsInDeck = value;
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -this.thicknessOfOneCard * value);
            }
        }

        public void Start () {
            this.CardsInDeck = 40;
            this.deckManager.RegisterDrawCardCallback(() => {
                this.CardsInDeck--;
            });
        }

        public void RegisterDeckClickedCallback(Action callback) {
            this.cbDeckClicked += callback;
        }

        public void OnClick() {
            CommandBus.Add(new LogCommand("before delay"));
            CommandBus.Add(new DelayCommand(2f));
            CommandBus.Add(new LogCommand("after 2 second delay"));
            // Call callbacks
            if (this.cbDeckClicked != null) {
                this.cbDeckClicked();
            }
        }
    }
}
