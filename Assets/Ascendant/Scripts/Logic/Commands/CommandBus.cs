using System.Collections.Generic;
using UnityEngine;

namespace Ascendant.Logic.Commands {
    public class CommandBus : BaseBehaviour {
        private readonly Queue<Command> commandQueue = new Queue<Command>();
		private bool commandIsRunning = false;

		public void Awake() {
			Container.Register(this);
		}

		public void Update() {
            if (this.commandIsRunning || this.commandQueue.Count == 0) {
                return;
            }
			this.commandIsRunning = true;
			Command command = this.commandQueue.Dequeue();
			if (command is AsyncCommand) {
				((AsyncCommand) command).Execute(DoneCallback);
			} else {
				command.Execute();
				DoneCallback();
			}
		}

        public void Add(Command command) {
            this.commandQueue.Enqueue(command);
        }

		private void DoneCallback() {
			this.commandIsRunning = false;
		}
    }
}
