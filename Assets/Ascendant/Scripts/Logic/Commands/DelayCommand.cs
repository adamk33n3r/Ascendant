using System;
using DG.Tweening;

namespace Ascendant.Scripts.Logic.Commands {
    public class DelayCommand : Command {
        private readonly float delay;

        public DelayCommand(float delay) {
            this.delay = delay;
        }

        public override void Execute(Action doneCallback) {
            Sequence s = DOTween.Sequence();
            s.PrependInterval(this.delay);
            s.OnComplete(() => {
                doneCallback();
            });
        }
    }
}
