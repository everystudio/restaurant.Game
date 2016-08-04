using UnityEngine;
using System.Collections;

public class WindowStaffNewcomer : CPanel {

	protected override void panelStart ()
	{
		base.panelStart ();
		UIAssistant.main.SetPrevPage ("WindowStaffList");

		DataStaffParam param = new DataStaffParam ();

		DataManager.Instance.masterStaff.Create (ref param);

		DataManager.Instance.dataStaff.AddNewStaff (param);

	}



}
