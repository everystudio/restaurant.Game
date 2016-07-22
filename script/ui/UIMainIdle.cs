using UnityEngine;
using System.Collections;

public class UIMainIdle : CPanel {
	
	public void pushedShopItem(){
		UIAssistant.main.ShowPage ("ShopIdle");
	}
	public void pushedMenuEdit(){
		UIAssistant.main.ShowPage ("EditIdle");
	}
}
