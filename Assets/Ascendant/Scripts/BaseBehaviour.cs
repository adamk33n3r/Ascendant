using System.Text;
using System.Linq;
using UnityEngine;

namespace Ascendant {
	public class BaseBehaviour : MonoBehaviour {
		public static void print(params object[] messages) {
			MonoBehaviour.print(
				string.Join(
					" ",
					messages.Select((message) => message == null ? "Null" : message.ToString()).ToArray()
				)
			);
		}
	}
}
