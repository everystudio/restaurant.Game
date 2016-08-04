using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WindowStaffList : CPanel {
	[SerializeField]
	private StaffList m_staffList;

	[SerializeField]
	private Button m_btnEmploy;

	private void BtnEmploy(){
		UIAssistant.main.ShowPage ("WindowStaffEmploy");
	}

	protected override void awake ()
	{
		base.awake ();
		m_btnEmploy.onClick.AddListener (BtnEmploy);

	}


	protected override void panelStart ()
	{
		base.panelStart ();
		m_staffList.Initialize ();

		UIAssistant.main.SetPrevPage ("MainIdle");
	}

}
