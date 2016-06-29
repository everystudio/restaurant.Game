using UnityEngine;
using System.Collections;


public class DebugSendData : ButtonBase {

	// Update is called once per frame
	void Update () {

		if (ButtonPushed) {
			DataManager.Instance.send_data ();
			gameObject.SetActive (false);
		}
	
	}
}
