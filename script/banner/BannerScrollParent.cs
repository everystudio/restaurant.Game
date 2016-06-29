using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BannerScrollParent : MonoBehaviourEx {

	protected CtrlTabParent m_tabParent;
	public List<BannerBase> m_bannerBaseList = new List<BannerBase> ();
	public List<GameObject> m_childList = new List<GameObject> ();
	protected void BannerBaseClear(){
		foreach (GameObject obj in m_childList) {
			if (obj != null) {
				Destroy (obj);
			}
		}
		m_childList.Clear ();
		m_bannerBaseList.Clear ();
		return;
	}
	protected void BannerBaseAdd( GameObject _goChild ){
		BannerBase bannerbase = _goChild.GetComponent<BannerBase> ();
		bannerbase.SetTabParent (m_tabParent);
		m_bannerBaseList.Add (bannerbase);
		m_childList.Add (_goChild);
		return;
	}
	public void Initialize( CtrlTabParent _tabParent ){
		m_tabParent = _tabParent;
		return;
	}

	#region SerializeField設定
	[SerializeField]
	private GameObject m_goScrollViewParent;
	[SerializeField]
	private UIGrid m_Grid;

	[SerializeField]
	private BannerScrollSwitch m_bannerScrollSwitch;
	#endregion

	public void Display( List<DataWorkParam> _workList , int _iIndex , string _strButton = "" ){
		BannerBaseClear ();
		foreach (DataWorkParam data in _workList) {
			GameObject objBanner = PrefabManager.Instance.MakeObject ("prefab/PrefBannerWork" , m_Grid.gameObject );
			BannerWork script = objBanner.GetComponent<BannerWork> ();
			script.Initialize (data);
			BannerBaseAdd(objBanner);
		}
		setSwitchButton (_strButton, m_goScrollViewParent, m_Grid , _iIndex);
	}

	public void Display( List<CsvItemParam> _itemMasterList , int _iIndex , string _strButton = "" ){
		BannerBaseClear ();
		foreach (CsvItemParam data in _itemMasterList) {
			GameObject objBanner = PrefabManager.Instance.MakeObject ("prefab/PrefBannerItem" , gameObject );
			BannerItem script = objBanner.GetComponent<BannerItem> ();
			if (script.Initialize (data, GameMain.Instance.m_iCostNokori)) {
				BannerBaseAdd (objBanner);
				objBanner.transform.parent = m_Grid.transform;
				//Debug.Log ("insert");
			} else {
				//Debug.LogError ( string.Format( "nokemono:{0}" , data.item_id));
				Destroy (objBanner);
			}
		}
		setSwitchButton (_strButton, m_goScrollViewParent, m_Grid , _iIndex);
		m_Grid.enabled = true;
	}

	public void Display( List<DataItemParam> _itemList , int _iIndex , string _strButton = "" ){
		BannerBaseClear ();
		foreach (DataItemParam data in _itemList) {

			Debug.LogError (string.Format ("serial:{0} item_id:{1}", data.item_serial, data.item_id));
			GameObject objBanner = PrefabManager.Instance.MakeObject ("prefab/PrefBannerItem" , m_Grid.gameObject );
			BannerItem script = objBanner.GetComponent<BannerItem> ();
			script.Initialize (data, GameMain.Instance.m_iCostNokori);
			BannerBaseAdd (objBanner);
		}
		setSwitchButton (_strButton, m_goScrollViewParent, m_Grid , _iIndex);
	}

	public void Display( List<DataMonsterParam> _monsterList , int _iIndex , string _strButton = "" ){
		BannerBaseClear ();
		foreach (DataMonsterParam data in _monsterList) {
			GameObject objBanner = PrefabManager.Instance.MakeObject ("prefab/PrefBannerMonster" , m_Grid.gameObject );
			BannerMonster script = objBanner.GetComponent<BannerMonster> ();
			script.Initialize (data, GameMain.Instance.m_iCostNokori);
			BannerBaseAdd (objBanner);
		}
		setSwitchButton (_strButton, m_goScrollViewParent, m_Grid , _iIndex);
	}

	public void Display( List<CsvMonsterParam> _monsterMasterList , int _iIndex , string _strButton = "" ){
		BannerBaseClear ();
		foreach (CsvMonsterParam data_master in _monsterMasterList) {
			GameObject objBanner = PrefabManager.Instance.MakeObject ("prefab/PrefBannerMonster" , m_Grid.gameObject );
			BannerMonster script = objBanner.GetComponent<BannerMonster> ();
			script.Initialize (data_master, GameMain.Instance.m_iCostNokori);
			BannerBaseAdd (objBanner);
		}
		setSwitchButton (_strButton, m_goScrollViewParent, m_Grid , _iIndex);
	}

	public void Display( List<DataStaffParam> _StaffList , int _iIndex , string _strButton = "" ){
		BannerBaseClear ();
		foreach (DataStaffParam data in _StaffList) {
			Debug.LogError (string.Format ("serial:{0} item_serial:{1} staff_id:{2}", data.m_staff_serial, data.item_serial, data.staff_id));
			GameObject objBanner = PrefabManager.Instance.MakeObject ("prefab/PrefBannerStaff" , m_Grid.gameObject );
			BannerStaff script = objBanner.GetComponent<BannerStaff> ();
			script.Initialize (data, GameMain.Instance.m_iCostNokori);
			BannerBaseAdd (objBanner);
		}
		setSwitchButton (_strButton, m_goScrollViewParent, m_Grid , _iIndex);
	}
	public void Display( List<CsvStaffParam> _csvStaffList , int _iIndex , string _strButton = "" ){
		BannerBaseClear ();
		foreach (CsvStaffParam data in _csvStaffList) {
			GameObject objBanner = PrefabManager.Instance.MakeObject ("prefab/PrefBannerStaff" , m_Grid.gameObject );
			BannerStaff script = objBanner.GetComponent<BannerStaff> ();
			script.Initialize (data, GameMain.Instance.m_iCostNokori);
			BannerBaseAdd (objBanner);
		}
		setSwitchButton (_strButton, m_goScrollViewParent, m_Grid , _iIndex);
	}

	public void CloseSwitchButton(){
		m_bannerScrollSwitch.gameObject.SetActive (false);
	}

	public void setSwitchButton( string _strButton , GameObject _goParent , UIGrid _grid , int _iIndex ){
		float fOffset = 0.0f;
		m_bannerScrollSwitch.gameObject.SetActive (true);
		m_bannerScrollSwitch.Init (_strButton, _iIndex);

		if (_strButton.Equals( "" ) == true ) {
			fOffset = 0.0f;
		} else {
			fOffset = -50.0f;
		}

		_goParent.transform.localPosition = new Vector3 (0.0f, fOffset, 0.0f);
		_grid.Reposition ();
		// あんまり良くないけど都合ここで処理を行う
		if (m_bannerBaseList.Count <= 5) {
			_goParent.GetComponent<UIScrollView> ().enabled = false;
		}

	}


	// Update is called once per frame
	void Update () {

		foreach (BannerBase banner_base in m_bannerBaseList) {
			int iRequestTabIndex = 0;
			if (banner_base.RequestTabIndex (ref iRequestTabIndex)) {
				;// 用意したけどここでやらないほうがいいね
			}
		}
	
	}

	public int GetSwitchIndex(){
		return m_bannerScrollSwitch.SelectingIndex;
	}
	// これはあんまり使わないで
	public void SetSwitchIndex( int _iIndex ){
		m_bannerScrollSwitch.SetSelectingIndex (_iIndex);
		//m_bannerScrollSwitch.SelectingIndex = _iIndex;
	}
}
