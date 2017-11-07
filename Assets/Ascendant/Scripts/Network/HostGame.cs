using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

namespace Ascendant.Network {
	internal sealed class HostGame : BaseBehaviour {
		public string RoomName { get { return this.network.matchName; } set { this.network.matchName = value; } }

		private uint roomSize = 2;
		private NetworkManager network;

		private void Start () {
			this.gameObject.AddComponent<NetworkMatch>();
			this.network = NetworkManager.singleton;
			if (this.network.matchMaker == null) {
				this.network.StartMatchMaker();
			}
		}

		private void Update () {
			
		}

		public void CreateRoom() {
			print("Creating Room:", RoomName, "with room for", this.roomSize, "players");
			this.network.matchMaker.CreateMatch(RoomName, this.roomSize, true, "", "", "", 0, 0, (bool success, string extendedInfo, MatchInfo info) => {
				Debug.Log("Match created");
				this.network.OnMatchCreate(success, extendedInfo, info);
			});
		}
	}
}
