using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[System.Serializable]
public class DataUser {

	public UnityEventInt UpdateCoin = new UnityEventInt ();
	protected int m_iCoin;
	public int coin{
		get{
			return m_iCoin;
		}
		set{
			m_iCoin = value;
			UpdateCoin.Invoke (m_iCoin);
		}
	}

	public UnityEventInt UpdateTicket = new UnityEventInt ();
	protected int m_iTicket;
	public int ticket{
		get{
			return m_iTicket;
		}
		set{
			m_iTicket = value;
			UpdateTicket.Invoke (m_iTicket);
		}
	}
	public UnityEventInt UpdatePopularity = new UnityEventInt ();
	public int m_iPopularity;
	public int popularity{
		get{
			return m_iPopularity;
		}
		set{
			m_iPopularity = value;
			UpdatePopularity.Invoke (m_iPopularity);
		}
	}





	#region Pocket
	public int m_iSyakkin;
	public string m_strName;
	public int m_iLevel;
	public int m_iTotalExp;
	public int m_iGold;
	public int m_iWidth;
	public int m_iHeight;

	public int m_iCollectCount;

	public int m_iGoldPerHour;
	public int m_iShisyutsuPerHour;

	public bool m_bInitialized;
	/*
	public void AddSyakkin( int _iGold ){
		m_iSyakkin += _iGold;
		if (m_iSyakkin <= 0) {
			m_iSyakkin = 0;
			if (PlayerPrefs.HasKey ("kansai") == false) {
				PlayerPrefs.SetInt ("kansai", 1);
				//CtrlDebit.m_bOpen = true;
			}
		}
		DataManager.Instance.kvs_data.WriteInt (DefineOld.USER_SYAKKIN, m_iSyakkin);
		GameMain.Instance.HeaderRefresh ();
	}

	public void AddGold( int _iGold ){
		//Debug.LogError (string.Format ("gold now:{0} add_num:{1}", m_iGold, _iGold));
		m_iGold += _iGold;

		if (m_iGold < 0) {
			m_iGold = 0;
		}
		DataManager.Instance.kvs_data.WriteInt (DefineOld.USER_SYOJIKIN, m_iGold);
		GameMain.Instance.HeaderRefresh ();
	}

	public void AddExp( int _iExp ){
		m_iTotalExp += _iExp;
		DataManager.Instance.kvs_data.WriteInt(DefineOld.USER_TOTAL_EXP , m_iTotalExp);
	}
	public void AddCollect( int _iCount = 1 ){
		m_iCollectCount += _iCount;
		//Debug.LogError (m_iCollectCount); 
		DataManager.Instance.kvs_data.WriteInt(DefineOld.USER_COLLECT_COUNT , m_iCollectCount);
		//Debug.LogError (DataManager.Instance.kvs_data.ReadInt (DefineOld.USER_COLLECT_COUNT));
	}

	public bool AbleBuy( int _iGold , int _iTicket , int _iCost , int _iCostNokori , int _iHave , int _iLimit , ref BannerBase.ABLE_BUY_REASON _eReason ){
		bool bRet = true;
		_eReason = BannerBase.ABLE_BUY_REASON.OKEY;

		if (m_iGold < _iGold) {
			bRet = false;
			_eReason = BannerBase.ABLE_BUY_REASON.GOLD;
		}
		if (m_iTicket < _iTicket) {
			bRet = false;
			_eReason = BannerBase.ABLE_BUY_REASON.TICKET;
		}
		if (_iCostNokori < _iCost ) {
			bRet = false;
			_eReason = BannerBase.ABLE_BUY_REASON.COST;
		}

		if (0 < _iLimit) {
			//Debug.Log( string.Format( "setting_limit={0}" , _iLimit ));
			if (_iLimit <= _iHave) {
				_eReason = BannerBase.ABLE_BUY_REASON.LIMIT;
			}
		}
		return bRet;
	}
	*/
	#endregion

}
