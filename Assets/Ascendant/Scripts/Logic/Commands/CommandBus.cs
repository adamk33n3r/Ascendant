using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Ascendant.Scripts.Logic.Commands {
    public class CommandBus : MonoBehaviour {
        private readonly Queue<Command> CommandQueue = new Queue<Command>();
        private bool IsPlaying;

		public void Awake() {
			Container.Register(this);
		}

        public void Start() {
            DOTween.Init();
		}

        public void Add(Command command) {
            CommandQueue.Enqueue(command);
            if (!IsPlaying) {
                Run();
            }
        }

        private void Run() {
            if (CommandQueue.Count == 0) {
                IsPlaying = false;
                return;
            }
            IsPlaying = true;
            CommandQueue.Dequeue().Execute(Run);
        }
    }
}
