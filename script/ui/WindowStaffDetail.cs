using UnityEngine;
using System.Collections;

public class WindowStaffDetail : CPanel {

	[SerializeField]
	private StaffDetail m_staffDetail;

	protected override void panelStart ()
	{
		base.panelStart ();

		m_staffDetail.Initialize (UIParam.Instance.staff_detail_serial);
		UIParam.Instance.staff_detail_serial = 0;
	}

}
