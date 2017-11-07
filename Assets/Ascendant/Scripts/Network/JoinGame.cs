using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;

namespace Ascendant.Network {
	public sealed class JoinGame : BaseBehaviour {
		public Text RoomNameText;

		private List<MatchInfoSnapshot> matchList;
		private NetworkManager network;

		private void Start () {
			this.network = NetworkManager.singleton;
			if (this.network.matchMaker == null) {
				this.network.StartMatchMaker();
			}
		}

		private void Update () {
			
		}

		public void JoinRoom() {
			RefreshRoomList(() => {
				string matchName = RoomNameText.text == "" ? "default" : RoomNameText.text;
				MatchInfoSnapshot foundMatch = null;
				foreach (MatchInfoSnapshot matchInfo in this.matchList) {
					if (matchInfo.name == matchName) {
						foundMatch = matchInfo;
					}
				}
				if (foundMatch == null) {
					// Pop a dialog
					UnityEngine.Debug.LogError("NO ROOM '" + matchName + "' found!");
					return;
				}
				this.network.matchMaker.JoinMatch(foundMatch.networkId, "", "", "", 0, 0, (bool success, string extendedInfo, MatchInfo matchInfo) => {
					print("Joined game", foundMatch.name, matchInfo.address);
					this.network.OnMatchJoined(success, extendedInfo, matchInfo);
				});
			});
		}

		public void RefreshRoomList(System.Action callback) {
			this.network.matchMaker.ListMatches(0, 20, "", false, 0, 0, (bool success, string extendedInfo, List<MatchInfoSnapshot> matchList) => {
				this.matchList = matchList;
				print(success, extendedInfo, matchList.Count);
				foreach (MatchInfoSnapshot match in matchList) {
					print(match.name + ":", match.currentSize, "/", match.maxSize, "-", match.networkId);
				}

				callback();
			});
		}
	}
}
