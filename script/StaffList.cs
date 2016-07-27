using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StaffList : MonoBehaviour {

	[SerializeField]
	private GameObject m_goContent;

	[SerializeField]
	private List<StaffBanner> m_staffBannerList = new List<StaffBanner>();

	public void OnSelectStaff( int _iStaffSerial ){
		UIParam.Instance.staff_detail_serial = _iStaffSerial;
		UIAssistant.main.ShowPage ("WindowStaffDetail");
	}

	public void Initialize(){
		foreach(StaffBanner banner in m_staffBannerList ){
			Destroy (banner.gameObject);
		}
		m_staffBannerList.Clear ();

		foreach (DataStaffParam param in DataManager.Instance.dataStaff.list) {
			StaffBanner script = PrefabManager.Instance.MakeScript<StaffBanner> ("prefab/PrefStaffBanner", m_goContent);
			script.Initialize (param);
			script.OnSelectStaff.AddListener (OnSelectStaff);
			m_staffBannerList.Add (script);
		}

	}

}








