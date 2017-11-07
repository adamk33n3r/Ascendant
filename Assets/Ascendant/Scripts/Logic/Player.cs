using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Ascendant.Logic {
	public sealed class Player : NetworkBehaviour {
		private void Start() {
			Debug.Log(string.Format("Name: {0} | IsLocal: {1}", name, isLocalPlayer));
		}

		private void Update() {
			// This script will be on all player objects so it will be run for every player.
			// We only want code to run on the local player so we will return if it is not.
			if (!isLocalPlayer) { return; }
			if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape)) {
				Debug.Log("PRESSED ESCAPE. Calling command");
				CmdTestCommand();
			}
		}

		public override void OnStartClient() {
			base.OnStartClient();
			Debug.Log("CLIENT CONNECTED");

			string netId = GetComponent<NetworkIdentity>().netId.ToString();
			Container.Get<Network.RoomInfo>().RegisterPlayer(netId, this);
		}

		[Command]
		public void CmdTestCommand() {
			Debug.Log("Test command");
			foreach (Player player in Container.Get<Network.RoomInfo>().Players) {
				player.RpcTestRpc();
			}
		}

		[ClientRpc]
		public void RpcTestRpc() {
			Debug.Log("TEST RPC ON: " + name);
		}
	}
}
