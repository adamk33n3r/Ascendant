using System;
using DG.Tweening;
using UnityEngine;

namespace Ascendant.Logic.Commands {
    public class DelayCommand : AsyncCommand {
        private readonly float delay;

        public DelayCommand(float delay) {
            this.delay = delay;
        }

		public override void Execute(Action doneCallback) {
			Debug.Log(string.Format("Waiting for {0} seconds", this.delay));
			Sequence s = DOTween.Sequence();
			s.PrependInterval(this.delay);
			s.OnComplete(() => {
				doneCallback();
			});
		}
	}
}
