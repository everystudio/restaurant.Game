using UnityEngine;
using System.Collections;

public class WindowStaffEmploy : CPanel {

	[SerializeField]
	private BtnStaffEmploy[] m_btnStaffEmployArr;

	protected override void panelStart ()
	{
		base.panelStart ();

		BtnStaffEmploy.EMPLOY_TYPE[] init_type = new BtnStaffEmploy.EMPLOY_TYPE [] {
			BtnStaffEmploy.EMPLOY_TYPE.NORMAL,
			BtnStaffEmploy.EMPLOY_TYPE.RARE,
			BtnStaffEmploy.EMPLOY_TYPE.RARE11,
		};

		if (init_type.Length == m_btnStaffEmployArr.Length) {
			for (int i = 0; i < init_type.Length; i++) {
				m_btnStaffEmployArr[i].Initialize (init_type [i]);
			}
		}
	}
}
