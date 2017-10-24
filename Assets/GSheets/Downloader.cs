using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace GSheets {
	public class Downloader : MonoBehaviour {
		[Tooltip("https://docs.google.com/spreadsheets/d/[THIS_PART]/edit?usp=sharing")]
		public string fileId;
		public string sheetId = "0";

		private void Start() {
			StartCoroutine(GetSheet());
			//https://docs.google.com/spreadsheets/u/0/d/1d3yK_T_JWFj-2aaheBnbfqVDHyTpPvd7od3gYvqrw70/export?format=csv&id=1d3yK_T_JWFj-2aaheBnbfqVDHyTpPvd7od3gYvqrw70&gid=0
			//https://docs.google.com/spreadsheets/u/0/d/1d3yK_T_JWFj-2aaheBnbfqVDHyTpPvd7od3gYvqrw70/export?format=csv&id=1d3yK_T_JWFj-2aaheBnbfqVDHyTpPvd7od3gYvqrw70&gid=22745775
		}

		private IEnumerator GetSheet() {
			string uri = "https://docs.google.com/spreadsheets/d/" + this.fileId + "/export?format=csv&gid=" + this.sheetId;
			UnityWebRequest request = UnityWebRequest.Get(uri);
			yield return request.Send();
			if (request.isNetworkError || request.isHttpError) {
				Debug.LogError(request.responseCode + ":" + request.error);
			} else {
				Debug.Log(request.downloadHandler.text);
			}
		}
	}
}
