using System;
using UnityEngine;
using DG.Tweening;

namespace Ascendant.Scripts.Logic.Commands {
    public class LogCommand : Command {
        private readonly string text;

        public LogCommand(string text) {
            this.text = text;
        }

        public override void Execute (Action doneCallback) {
            Debug.Log(this.text);
            doneCallback();
        }
    }
}
