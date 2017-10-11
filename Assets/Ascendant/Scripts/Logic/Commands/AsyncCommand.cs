using System;

namespace Ascendant.Scripts.Logic.Commands {
	public abstract class AsyncCommand : Command {
		public virtual void Execute(Action doneCallback) {
            doneCallback();
        }
	}
}
