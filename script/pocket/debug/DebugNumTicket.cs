using UnityEngine;
using System.Collections;

public class DebugNumTicket : DebugNumBase {

	override protected void initialize (){
		m_lbNum.text = DataManager.Instance.user.ticket.ToString ();
	}

	override protected void func_plus (){
		DataManager.Instance.user.ticket += (m_iRate);
		m_lbNum.text = DataManager.Instance.user.ticket.ToString ();
		GameMain.Instance.HeaderRefresh ();
	}

	override protected void func_minus(){
		DataManager.Instance.user.ticket += (m_iRate * -1);
		m_lbNum.text = DataManager.Instance.user.ticket.ToString ();
		GameMain.Instance.HeaderRefresh ();
	}
}
