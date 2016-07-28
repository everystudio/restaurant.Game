using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class StaffDetail : MonoBehaviour {

	[SerializeField]
	private StaffBanner m_staffBanner;

	[SerializeField]
	private GameObject m_goTrainIconRoot;
	[SerializeField]
	private GameObject m_goRoleBannerRoot;

	[SerializeField]
	private List<StaffTrainingIcon> m_staffTrainIconList = new List<StaffTrainingIcon>();
	[SerializeField]
	private List<StaffRoleBanner> m_staffRoleBannerList = new List<StaffRoleBanner>();

	[SerializeField]
	private Button m_btnFire;

	private DataStaffParam m_dataStaffParam;

	[SerializeField]
	private CtrlCharaCheck m_ctrlCharaCheck;

	void Awake(){
		m_btnFire.onClick.AddListener (OnClickFire);
	}

	private void OnFireDecide(){
		Destroy (m_ctrlCharaCheck.gameObject);
		DataManager.Instance.dataStaff.list.Remove (m_dataStaffParam);
		UIAssistant.main.ShowPage ("WindowStaffList");
	}
	private void OnFireCancel(){
		Destroy (m_ctrlCharaCheck.gameObject);
	}

	private void OnClickFire(){

		m_ctrlCharaCheck= PrefabManager.Instance.MakeScript<CtrlCharaCheck> ("prefab/UguiCharaCheck", FrontPanel.Instance.gameObject);
		m_ctrlCharaCheck.Initialize ("このスタッフを\n解雇しますがよろしいですか？");
		m_ctrlCharaCheck.btnYes.onClick.AddListener (OnFireDecide);
		m_ctrlCharaCheck.btnNo.onClick.AddListener (OnFireCancel);

	}

	private void Initialize( DataStaffParam _param ){

		m_dataStaffParam = _param;


		m_staffBanner.Initialize (_param);

		foreach (StaffTrainingIcon script in m_staffTrainIconList) {
			Destroy (script.gameObject);
		}
		m_staffTrainIconList.Clear ();

		foreach (StaffRoleBanner script in m_staffRoleBannerList) {
			Destroy (script.gameObject);
		}
		m_staffRoleBannerList.Clear ();

		DataStaff.TRAINING[] trainArr = new DataStaff.TRAINING[] {
			DataStaff.TRAINING.BRUSH,
			DataStaff.TRAINING.DRESS,
			DataStaff.TRAINING.DUMBBELL,
			DataStaff.TRAINING.RIBBON,
			DataStaff.TRAINING.SPOON,
			DataStaff.TRAINING.PAN,
		};
		foreach (DataStaff.TRAINING eTrainingType in trainArr) {
			StaffTrainingIcon script = PrefabManager.Instance.MakeScript<StaffTrainingIcon> ("prefab/StaffTrainIcon", m_goTrainIconRoot);
			script.Initialize (eTrainingType, _param);
			m_staffTrainIconList.Add (script);
		}
		DataStaff.ROLE[] roleArr = new DataStaff.ROLE[] {
			DataStaff.ROLE.FLOOR,
			DataStaff.ROLE.ENTRANCE,
			DataStaff.ROLE.CHEF,
			DataStaff.ROLE.NONE,
		};
		foreach (DataStaff.ROLE eRole in roleArr) {
			StaffRoleBanner script = PrefabManager.Instance.MakeScript<StaffRoleBanner> ("prefab/StaffRoleBanner", m_goRoleBannerRoot);
			script.Initialize (eRole, _param);
			m_staffRoleBannerList.Add (script);
		}

	}

	public void Initialize(int _iStaffSerial ){
		Initialize (DataManager.Instance.dataStaff.Select (_iStaffSerial));
	}




}
