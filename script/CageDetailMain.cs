using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CageDetailMain : PageBase2 {

	public enum STEP
	{
		NONE			= 0,
		IDLE			,
		KATAZUKE_CHECK	,
		KATAZUKE		,
		MAX				,
	}
	public STEP m_eStep;
	public STEP m_eStepPre;

	public UI2DSprite m_sprHospital;

	private CtrlKatazukeCheck m_csKatazukeCheck;

	#region SerializeFieldでの設定が必要なメンバー変数

	#endregion

	public Tab[] ITEM_DETAIL_TABS = new Tab[5] {
		new Tab (Tab.TYPE.ITEM_GAUGE, "aaa", "shisetsu1page_tab1", "", new SearchData[1]{
			new SearchData( GameMain.TABLE_TYPE.NONE , "" ),
		} , "OriMes1" , "prefab/PrefItemDetailCage" ),
		//} , "prefab/PrefItemDetailDisp" ),
		new Tab (Tab.TYPE.ITEM_OFFICE, "aaa", "shisetsu1page_tab2", "", new SearchData[1]{
			new SearchData( GameMain.TABLE_TYPE.MONSTER , "@where_key01" , BannerBase.MODE.MONSTER_DETAIL ),
		} , "OriMes2"),
		new Tab (Tab.TYPE.ITEM_SHOP, "aaa", "shisetsu1page_tab3", "itempage_menu", new SearchData[2]{
			new SearchData( GameMain.TABLE_TYPE.MONSTER_MASTER , " status = 1 " , BannerBase.MODE.MONSTER_SET_BUY ),
			new SearchData( GameMain.TABLE_TYPE.MONSTER , " item_serial = 0 " , BannerBase.MODE.MONSTER_SET_MINE )
		} , "OriMes3"),
		new Tab (Tab.TYPE.ITEM_EXTEND, "aaa", "shisetsu1page_tab4", "", new SearchData[1]{ 
			new SearchData( GameMain.TABLE_TYPE.NONE , "" ),
		} , "OriMes4" , "prefab/PrefItemDetailBuildupCage" ),
		new Tab (Tab.TYPE.ITEM_TICKET, "aaa", "shisetsu1page_tab5", "", new SearchData[1]{ 
			new SearchData( GameMain.TABLE_TYPE.MONSTER , "@where_key02" , BannerBase.MODE.MONSTER_SICK),
		} , "OriMes5"),
	};



	public int m_iItemSerial;

	public DataItemParam m_dataItemParam;
	public CsvItemParam m_CsvItemParam;

	protected override void initialize(){
		m_WhereHash.Clear ();
		m_WhereHash.Add ("@where_key01", "item_serial = " + GameMain.Instance.m_iSettingItemSerial.ToString ());
		m_WhereHash.Add ("@where_key02", "item_serial = " + GameMain.Instance.m_iSettingItemSerial.ToString () + " and condition = " + ((int)DefineOld.Monster.CONDITION.SICK).ToString () + " ");

		m_eStep = STEP.IDLE;
		m_eStepPre = STEP.MAX;

		m_iItemSerial = GameMain.Instance.m_iSettingItemSerial;
		//Debug.Log ("serial:" + m_iItemSerial.ToString ());

		GameObject objTabParent = PrefabManager.Instance.MakeObject ("prefab/PrefTabParent", gameObject);
		m_tabParent = objTabParent.GetComponent<CtrlTabParent> ();
		m_tabParent.Init (ITEM_DETAIL_TABS);

		// ここはserialから対応した文字列を選択する必要があります
		m_pageHeader = makeHeader ("header_shisetsu1" ,  ITEM_DETAIL_TABS[0].m_strWordKey , "btn_katazuke");
		makeCloseButton ();

		m_dataItemParam = DataManager.Instance.m_dataItem.Select (m_iItemSerial);
		m_CsvItemParam = DataManager.Instance.m_csvItem.Select (m_dataItemParam.item_id);

		// これ、別のところでもやってます
		List<DataMonsterParam> monster_list = DataManager.Instance.dataMonster.Select (" item_serial = " + m_iItemSerial.ToString ());
		int iUseCost = 0;
		foreach (DataMonsterParam monster in monster_list) {
			CsvMonsterParam data_master = DataManager.Instance.m_csvMonster.SelectOne ( string.Format( "monster_id = {0}" , monster.monster_id ));
			iUseCost += data_master.cost;
		}
		CsvItemDetailData detail_data = DataManager.GetItemDetail (m_dataItemParam.item_id, m_dataItemParam.level);
		GameMain.Instance.m_iCostMax = detail_data.cost;
		GameMain.Instance.m_iCostNow = iUseCost;
		GameMain.Instance.m_iCostNokori = detail_data.cost - iUseCost;

		GameObject bannerParent = PrefabManager.Instance.MakeObject ("prefab/PrefBannerScrollParent", gameObject);
		m_bannerScrollParen = bannerParent.GetComponent<BannerScrollParent> ();
		m_bannerScrollParen.Initialize (m_tabParent);

		m_iTabIndex = 0;
		m_iSwitchIndex = 0;
		Display (m_bannerScrollParen, ITEM_DETAIL_TABS, m_iTabIndex, m_iSwitchIndex);

		m_csKatazukeCheck = null;
		//m_dataItem.category 

	}

	public bool m_bDisp;

	// Update is called once per frame
	void Update () {

		bool bInit = false;
		if (m_eStepPre != m_eStep) {
			m_eStepPre  = m_eStep;
			bInit = true;
			//Debug.Log (m_eStep);
		}

		if (m_iTabIndex == 4 && m_bDisp == false) {
			m_bDisp = true;
			if (0 == m_bannerScrollParen.m_childList.Count) {
				m_sprHospital.gameObject.SetActive (m_bDisp);
			}
		} else if (m_bDisp == true && m_iTabIndex != 4) {
			m_bDisp = false;
			m_sprHospital.gameObject.SetActive (m_bDisp);
		} else {
		}

		switch (m_eStep) {
		case STEP.IDLE:
			if (bInit) {
				if (m_csKatazukeCheck != null) {
					Destroy (m_csKatazukeCheck.gameObject);
					m_csKatazukeCheck = null;
				}

				m_pageHeader.TriggerClear ();
			}
			displayAutoUpdate (ITEM_DETAIL_TABS);

			if (m_pageHeader.ButtonPushed) {
				m_eStep = STEP.KATAZUKE_CHECK;
			}
			break;
		case STEP.KATAZUKE_CHECK:
			if (bInit) {

				GameObject obj = PrefabManager.Instance.MakeObject ("prefab/PrefKatazukeCheck", gameObject);
				m_csKatazukeCheck = obj.GetComponent<CtrlKatazukeCheck> ();
				m_csKatazukeCheck.Initialize ();
			}
			if (m_csKatazukeCheck.YesOrNo.IsYes ()) {
				m_eStep = STEP.KATAZUKE;
			} else if (m_csKatazukeCheck.YesOrNo.IsNo ()) {
				m_eStep = STEP.IDLE;
			} else {
				;// なにもしない
			}
			break;

		case STEP.KATAZUKE:
			if (bInit) {
			}

			// 消す予定のところに新しい土地を設置する
			for (int x = m_dataItemParam.x; x < m_dataItemParam.x + m_dataItemParam.width; x++) {
				for (int y = m_dataItemParam.y; y < m_dataItemParam.y + m_dataItemParam.height; y++) {
					GameObject obj = PrefabManager.Instance.MakeObject ("prefab/PrefFieldItem", GameMain.ParkRoot.gameObject);
					obj.name = "fielditem_" + x.ToString () + "_" + y.ToString ();
					CtrlFieldItem script = obj.GetComponent<CtrlFieldItem> ();
					script.Init (x, y, 0);
					GameMain.ParkRoot.m_fieldItemList.Add (script);
				}
			}

			DataManager.Instance.m_dataItem.Update (GameMain.Instance.m_iSettingItemSerial, 0, 0, 0);

			int iRemoveIndex = 0;
			foreach (CtrlFieldItem item in GameMain.ParkRoot.m_fieldItemList) {
				if (item.m_dataItemParam.item_serial == GameMain.Instance.m_iSettingItemSerial) {
					item.Remove ();
					GameMain.ParkRoot.m_fieldItemList.RemoveAt (iRemoveIndex);
					break;
				}
				iRemoveIndex += 1;
			}

			// 取り除く
			m_itemDetailBase.Remove ();

			int iUriagePerHour = 0;
			List<DataItemParam> item_list = DataManager.Instance.m_dataItem.Select (" item_serial != 0 ");
			foreach (DataItemParam item in item_list) {
				iUriagePerHour += item.GetUriagePerHour ();
			}
			DataManager.Instance.kvs_data.WriteInt (DefineOld.USER_URIAGE_PAR_HOUR, iUriagePerHour);

			// 仕事の確認
			DataWork.WorkCheck ();
			GameMain.Instance.HeaderRefresh ();
			GameMain.ParkRoot.ConnectingRoadCheck ();

			// 片付けして戻る
			m_closeButton.Close ();
			Destroy (m_csKatazukeCheck.gameObject);
			m_csKatazukeCheck = null;
			break;

		default:
			break;
		}
	}
}
