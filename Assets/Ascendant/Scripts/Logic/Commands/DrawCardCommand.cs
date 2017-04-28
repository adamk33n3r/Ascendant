using System;
using Ascendant.ScriptableObjects;
using DG.Tweening;

namespace Ascendant.Scripts.Logic.Commands {
    public class DrawCard : Command {
        private DeckManager deckManager;
        private CardAsset cardAsset;

        public DrawCard(DeckManager deckManager, CardAsset cardAsset) {
            this.deckManager = deckManager;
            this.cardAsset = cardAsset;
        }

        public override void Execute (Action doneCallback) {
//            this.deckManager
            doneCallback();
        }
    }
}
