using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

using Ascendant.Logic;

namespace Ascendant.Network {
	public class RoomInfo : BaseBehaviour {
		public Text InfoText;

		public Player[] Players { get { return this.players.Values.ToArray(); } }

		private NetworkManager network;
		private Dictionary<string, Player> players = new Dictionary<string, Player>();
		private const string PLAYER_PREFIX = "Player ";

		private void Awake() {
			Container.Register(this);
		}

		private void Start() {
			this.network = NetworkManager.singleton;
		}

		private void Update() {
			InfoText.text = "Room Name: " + this.network.matchName + "\n" +
				"Players: " + this.players.Count
			;

			foreach(string playerId in this.players.Keys) {
				InfoText.text += "\n\t" + playerId + " - " + this.players[playerId].name;
			}
		}

		// TODO: Belong in a game manager or something
		public void RegisterPlayer(string netId, Player player) {
			netId = PLAYER_PREFIX + netId;
			this.players.Add(netId, player);
			player.name = netId;
		}

		public void UnregisterPlayer(string netId) {
			this.players.Remove(netId);
		}
	}
}
