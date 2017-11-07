using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Ascendant.UI.Dialog {
	public delegate void DialogCallback(Dialog dialog, bool result);

	public struct DialogConfig {
		public string headerText;
		public string bodyText;
		public float width;
		public float height;
		public DialogCallback onCloseCallback;
	}

	public class Dialog : BaseBehaviour {

		[SerializeField] protected TextMeshProUGUI Header;
		[SerializeField] protected TextMeshProUGUI Body;
		protected DialogCallback onCloseCallback;

		private RectTransform rectTransform;

		private void Awake() {
			this.rectTransform = GetComponent<RectTransform>();
		}

		public void Setup(DialogConfig config) {
			Debug.Log("SETUP");
			Header.text = config.headerText ?? "Header Text";
			Body.text = config.bodyText ?? "Body Text";

			this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Max(config.width, 350));
			this.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Max(config.height, 200));
			this.onCloseCallback = config.onCloseCallback;
		}

		public void Close() {
			Destroy(this.gameObject);
		}

		#region Events
		public void OnClickYes() {
			if (this.onCloseCallback == null) return;
			this.onCloseCallback(this, true);
		}

		public void OnClickNo() {
			if (this.onCloseCallback == null) return;
			this.onCloseCallback(this, false);
		}
		#endregion
	}
}
