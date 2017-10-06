using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Ascendant.Scripts.Logic.Commands {
    public class CommandBus : MonoBehaviour {
        private static readonly Queue<Command> CommandQueue = new Queue<Command>();
        private static bool IsPlaying;

        public void Start() {
            DOTween.Init();
        }

        public static void Add(Command command) {
            CommandQueue.Enqueue(command);
            if (!IsPlaying) {
                Run();
            }
        }

        private static void Run() {
            if (CommandQueue.Count == 0) {
                IsPlaying = false;
                return;
            }
            IsPlaying = true;
            CommandQueue.Dequeue().Execute(Run);
        }
    }
}
