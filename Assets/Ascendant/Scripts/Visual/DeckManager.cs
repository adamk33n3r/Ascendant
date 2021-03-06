using UnityEngine.EventSystems;
using Ascendant.Logic.Commands;

namespace Ascendant.Visual {
    public class DeckManager : BaseBehaviour, IPointerClickHandler {
		private CommandBus bus;

        public void Start () {
			this.bus = Container.Get<CommandBus>();
        }

		public void OnPointerClick(PointerEventData eventData) {
			print("CLICKED");
			this.bus.Add(new DrawCardCommand());
		}
	}
}
