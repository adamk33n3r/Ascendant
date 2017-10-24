using System.Linq;
using UnityEngine;

namespace Ascendant.Logic.Commands {
    public abstract class Command {
        public virtual void Execute() {
        }

		public static void print(params object[] messages) {
			MonoBehaviour.print(
				string.Join(
					", ",
					messages.Select((message) => message.ToString()).ToArray()
				)
			);
		}
    }
}
