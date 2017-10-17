using Ascendant.Scripts.Logic.Commands;
using UnityEngine.EventSystems;

namespace Ascendant.Scripts.Visual {
	public class DiscardManager : BaseBehaviour, IPointerClickHandler {
		private CommandBus bus;
		private void Start () {
			this.bus = Container.Get<CommandBus>();
		}

		public void OnPointerClick(PointerEventData eventData) {
			this.bus.Add(new DiscardCardCommand());
		}
	}
}
