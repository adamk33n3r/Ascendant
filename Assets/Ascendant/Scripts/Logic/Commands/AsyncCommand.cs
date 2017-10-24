using System;

namespace Ascendant.Logic.Commands {
	public abstract class AsyncCommand : Command {
		public virtual void Execute(Action doneCallback) {
            doneCallback();
        }
	}
}
