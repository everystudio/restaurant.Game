using UnityEngine;
using System.Collections;

public class WindowStaffList : CPanel {
	[SerializeField]
	private StaffList m_staffList;

	protected override void panelStart ()
	{
		base.panelStart ();
		m_staffList.Initialize ();
	}

}
