using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectBase : MonoBehaviourEx {

	private bool m_bInitialized = false;
	DataItemParam m_dataItemParam = null;
	CsvItemParam m_csvItemParam = null;
	DataMonsterParam m_dataMonsterParam = null;
	CsvMonsterParam m_csvMonsterParam = null;

	public int m_iCollectGold = 0;
	public int m_iCollectExp = 0;

	CtrlFieldItem m_ctrlFieldItem = null;


	public bool m_bCollectCheck;

	private void Collected(){
		if (m_bCollectCheck == true) {
			m_bCollectCheck = false;
			// リセット処理
			if (m_dataItemParam != null) {
				string strNow = TimeManager.StrGetTime ();
				Dictionary< string , string > dict = new Dictionary< string , string > ();
				dict.Add ("collect_time", "\"" + strNow + "\"");
				DataManager.Instance.m_dataItem.Update (m_dataItemParam.item_serial, dict);
			}
			if (m_dataMonsterParam != null) {
				string strNow = TimeManager.StrNow ();
				Dictionary< string , string > dict = new Dictionary< string , string > ();
				dict.Add ("collect_time", "\"" + strNow + "\"");
				DataManager.Instance.dataMonster.Update (m_dataMonsterParam.monster_serial, dict );
			}

			collectCheck ();
		}
	}

	private void initializeCommon(int _iItemSerial){
		if (m_bInitialized == false) {
			CtrlCollectGold.Instance.m_eventCollect.AddListener (Collected);
		}
		m_ctrlFieldItem = GameMain.ParkRoot.GetFieldItem (_iItemSerial);

	}

	public void Initialize( DataItemParam _item ){
		initializeCommon (_item.item_serial);
		m_dataItemParam = _item;
		m_csvItemParam = DataManager.GetItem (_item.item_id);

		collectCheck();
	}

	public void Initialize( int _iItemSerial ,  DataMonsterParam _monster ){
		initializeCommon ( _iItemSerial );
		m_dataMonsterParam = _monster;
		m_csvMonsterParam = DataManager.GetMonster (_monster.monster_id);
		collectCheck();
	}

	private void collectCheck(){
		int iDelay = 0;
		if (m_dataItemParam != null) {
			if (getCollectShop (out m_iCollectGold, out m_iCollectExp, out iDelay)) {
				Invoke ("add_collect", iDelay);
			}
		} else if (m_dataMonsterParam != null) {
			if (getCollectMonster (out m_iCollectGold, out m_iCollectExp, out iDelay)) {
				Invoke ("add_collect", iDelay);
			}
		} else {
			;// 何もない場合は無視
		}
	}
	private void add_collect(){

		int igold = m_iCollectGold;
		int iexp = m_iCollectExp;
		if (m_ctrlFieldItem != null && m_ctrlFieldItem.m_eRoad != DefineOld.ROAD.CONNECTION_SHOP) {
		} else {
			igold += igold / 2;
			iexp += iexp / 2;
		}

		CtrlCollectGold.Instance.AddCollect (igold, iexp);
		m_bCollectCheck = true;
	}



	public bool getCollectShop(  out int _iCollectGold , out int _iCollectExp , out int _iDelay ){
		float fTotalRate = DataManager.Instance.GetSymbolRate();

		int iShopCollectGold = 0;
		double dNokoriSec = 0;
		if (0 < m_csvItemParam.revenue) {
			// お店自体金額回収
			double diffSec = TimeManager.Instance.GetDiffNow (m_dataItemParam.collect_time).TotalSeconds * -1;
			Debug.LogError( m_dataItemParam.collect_time );
			Debug.LogError( diffSec );
			dNokoriSec = m_csvItemParam.revenue_interval - diffSec ;

			if (0 < dNokoriSec) {
			} else {
				dNokoriSec = 0;
			}

			iShopCollectGold = m_csvItemParam.revenue;
			iShopCollectGold = (int)(fTotalRate * iShopCollectGold);
			CtrlFieldItem ctrlFieldItem = GameMain.ParkRoot.GetFieldItem (m_dataItemParam.item_serial);

			if (ctrlFieldItem != null && ctrlFieldItem.m_eRoad != DefineOld.ROAD.CONNECTION_SHOP) {
			} else {
				iShopCollectGold += iShopCollectGold / 2;
			}
		}
		_iCollectGold = iShopCollectGold;
		_iCollectExp = 0;
		_iDelay = (int)dNokoriSec;
		return (0 < _iCollectGold);
	}

	private bool getCollectMonster( out int _iCollectGold , out int _iCollectExp , out int _iDelay ){
		double diffSec = TimeManager.Instance.GetDiffNow (m_dataMonsterParam.collect_time).TotalSeconds * -1;
		//Debug.Log (diffSec.ToString() + ":" + condition.ToString() );
		Debug.LogError( m_dataMonsterParam.collect_time );
		Debug.LogError( diffSec );
		double dNokoriSec = m_csvMonsterParam.revenew_interval - diffSec;

		if (0 < dNokoriSec) {
		} else {
			dNokoriSec = 0;
		}

		int iCollectGold = m_csvMonsterParam.revenew_coin;
		int iCollectExp = m_csvMonsterParam.revenew_exp;

		_iCollectGold = iCollectGold;
		_iCollectExp = iCollectExp;
		_iDelay = (int)dNokoriSec;

		// 健全なのは1
		if ( (int)DefineOld.Monster.CONDITION.SICK  == m_dataMonsterParam.condition ) {
			iCollectGold = 0;
		}
		return (0 < iCollectGold);
	}

}



