using UnityEngine;
using System.Collections;

public class UIMainIdle : CPanel {
	
	public void pushedShopGoods(){
		UIAssistant.main.ShowPage ("ShopGoodsIdle");
	}
	public void pushedMenuEdit(){
		UIAssistant.main.ShowPage ("EditIdle");
	}
}
