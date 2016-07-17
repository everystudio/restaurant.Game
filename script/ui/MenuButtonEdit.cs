using UnityEngine;
using System.Collections;

public class MenuButtonEdit : ButtonBase {

	private void pushedMenuEdit(){
		UIAssistant.main.ShowPage ("EditIdle");
	}

	void Start(){
		ClickButtonEvent.AddListener (pushedMenuEdit);
	}
}
