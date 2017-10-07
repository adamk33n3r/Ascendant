using System;
using Ascendant.ScriptableObjects;
using UnityEngine;

namespace Ascendant.Scripts.Logic.Commands {
    public class DrawCardCommand : Command {
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
