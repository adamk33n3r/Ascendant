using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace Ascendant.Network {
	internal sealed class RoomInfo : BaseBehaviour {
		public Text RoomNameText;

		private NetworkManager network;

		private void Start () {
			this.network = NetworkManager.singleton;
		}

		private void Update () {
			this.RoomNameText.text = "Room Name: " + this.network.matchName;
		}
	}
}
