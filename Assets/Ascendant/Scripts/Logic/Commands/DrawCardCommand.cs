using System;
using UnityEngine;

namespace Ascendant.Logic.Commands {
    public class DrawCardCommand : AsyncCommand {
        private DeckManager deckManager;

        public DrawCardCommand() {
            this.deckManager = Container.Get<DeckManager>();
        }

        public override void Execute (Action doneCallback) {
			this.deckManager.Draw();

			doneCallback();
        }
    }
}
