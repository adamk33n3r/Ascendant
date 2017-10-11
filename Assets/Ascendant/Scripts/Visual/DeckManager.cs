using System;
using Ascendant.Scripts.Logic.Commands;
using UnityEngine;

namespace Ascendant.Scripts.Visual {
    public class DeckManager : MonoBehaviour {
        public float thicknessOfOneCard = 0.012f;
        public Logic.DeckManager logicDeckManager;

		private CommandBus bus;

        private int cardsInDeck;
        public int CardsInDeck {
            get { return this.cardsInDeck; }
            set {
                this.cardsInDeck = value;
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -this.thicknessOfOneCard * value);
            }
        }

        public void Start () {
			this.bus = Container.Get<CommandBus>();
            this.CardsInDeck = 40;
            this.logicDeckManager.RegisterDrawCardCallback(() => {
                this.CardsInDeck--;
            });
        }

        public void OnClick() {
			bus.Add(new LogCommand("before delay"));
			bus.Add(new DelayCommand(2f));
			bus.Add(new LogCommand("after 2 second delay"));
			bus.Add(new DrawCardCommand());
			Logic.Events.Fire("test", 4);
        }
    }
}
