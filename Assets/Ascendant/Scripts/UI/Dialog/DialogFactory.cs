using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ascendant.UI.Dialog {
	public sealed class DialogFactory : BaseBehaviour {
		public GameObject DialogPrefab;
		public RectTransform ParentCanvas;

		private void Awake() {
			Container.Register(this);
		}

		/// <summary>
		/// Spawn a dialog
		/// </summary>
		/// <param name="pos"></param>
		/// <param name="header"></param>
		/// <param name="body"></param>
		/// <param name="footer"></param>
		public void CreateDialog(
			Rect rect,
			string header,
			string body,
			DialogCallback cb
		) {
			float canvasWidth = ParentCanvas.offsetMax.x - ParentCanvas.offsetMin.x;
			float canvasHeight = ParentCanvas.offsetMax.y - ParentCanvas.offsetMin.y;
			Vector2 pos = new Vector2(canvasWidth * rect.x, canvasHeight * (1 - rect.y));
			GameObject dialogGO = Instantiate(DialogPrefab, pos, ParentCanvas.rotation, ParentCanvas);
			Dialog dialog = dialogGO.GetComponent<Dialog>();
			dialog.Setup(new DialogConfig() { headerText = header, bodyText = body, width = rect.width, height = rect.height, onCloseCallback = cb });
		}
	}
}
