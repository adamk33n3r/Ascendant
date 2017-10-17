using System;
using UnityEngine;

namespace Ascendant.Scripts.Logic.Commands {
	public class DiscardCardCommand : Command {
        private HandManager handManager;

		public DiscardCardCommand() {
            this.handManager = Container.Get<HandManager>();
        }

        public override void Execute() {
			this.handManager.Discard();
        }
	}
}
