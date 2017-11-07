using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ascendant.UI.Dialog {
	public sealed class TestDialog : BaseBehaviour {
		private void Start() {
			
		}

		public void LaunchDialog() {
			float width = Random.Range(0, 600);
			float height = Random.Range(200, 600);
			DialogFactory factory = Container.Get<DialogFactory>();
			Rect rect = new Rect(0.5f, 0.5f, width, height);
			factory.CreateDialog(
				rect,
				"this is test",
				"body asdlkjfa skdjf asdjflkaj dlkfjalkg jerlkj goijhdfkvbh dkfh shjas dfhj hjkdsh fjksdhf sdkjhf ajkdshf kajsdhv kdfjg lkrtjg kjgkldjf aklsdjf akls fksjdklvj klfj dsalfj ad",
				(Dialog _dialog, bool success) => {
					Debug.Log("Success: " + success);
					_dialog.Close();
					factory.CreateDialog(rect, "head", "bod", (Dialog sub, bool result) => { sub.Close(); });
				}
			);
		}
	}
}
