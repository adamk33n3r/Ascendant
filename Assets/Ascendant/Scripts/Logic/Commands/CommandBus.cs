using System.Collections.Generic;
using UnityEngine;

namespace Ascendant.Scripts.Logic.Commands {
    public class CommandBus : BaseBehaviour {
        private readonly Queue<Command> CommandQueue = new Queue<Command>();
		private bool CommandIsRunning = false;

		public void Awake() {
			Container.Register(this);
			Events.Listen("test", (data) => {
				print("test event was fired. i am command bus");
			});
		}

		public void Update() {
            if (CommandIsRunning || CommandQueue.Count == 0) {
                return;
            }
			CommandIsRunning = true;
			Command command = CommandQueue.Dequeue();
			if (command is AsyncCommand) {
				((AsyncCommand) command).Execute(DoneCallback);
			} else {
				command.Execute();
				DoneCallback();
			}
		}

        public void Add(Command command) {
            CommandQueue.Enqueue(command);
        }

		private void DoneCallback() {
			CommandIsRunning = false;
		}
    }
}
