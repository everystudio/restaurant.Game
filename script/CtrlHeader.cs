using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CtrlHeader : MonoBehaviour {

	public UILabel m_lbSyakkin;
	public UILabel m_lbUriagePerHour;
	public UILabel m_lbShisyutsuPerHour;
	public UILabel m_lbShojikin;
	public UILabel m_lbTicket;
	public UILabel m_lbLevel;
	public UILabel m_lbLevelNextExp;
	public UILabel m_lbNextLevelMade;

	public CtrlDispNumbers m_numSyakkin;
	public CtrlDispNumbers m_numUriagePerHour;
	public CtrlDispNumbers m_numShisyutsuPerHour;
	public CtrlDispNumbers m_numShojikin;
	public CtrlDispNumbers m_numTicket;
	public CtrlDispNumbers m_numLevel;
	public CtrlDispNumbers m_numLevelNextExp;

	public PopupAnimation m_popupAnimation;
	public UISlider m_slExp;

	public CtrlHeaderExp m_ctrlHeaderExp;


	public void Initialize(){
		/*
		// ここで計算してるけど、あまりよろしくない
		// GameMainでも同じことやってます
		int iUriagePerHour = 0;
		List<DataItem> item_list = GameMain.dbItem.Select (" item_serial != 0 ");
		foreach (DataItem item in item_list) {
			iUriagePerHour += item.GetUriagePerHour ();
		}
		DataManager.Instance.kvs_data.WriteInt (DefineOld.USER_URIAGE_PAR_HOUR, iUriagePerHour);


		// 一時間あたりの支出
		int iShisyutsuHour = 0;
		foreach (DataItem item in item_list) {
			iShisyutsuHour += item.GetShiSyutsuPerHour ();
		}
		DataManager.Instance.kvs_data.WriteInt (DefineOld.USER_SHISYUTU_PAR_HOUR, iShisyutsuHour);
		*/


		//GetUriagePerHour

		m_numSyakkin.InitializeNumberOnly (DataManager.Instance.kvs_data.ReadInt(DefineOld.USER_SYAKKIN));
		m_numUriagePerHour.InitializeNumberOnly (DataManager.Instance.kvs_data.ReadInt(DefineOld.USER_URIAGE_PAR_HOUR));
		m_numShisyutsuPerHour.InitializeNumberOnly (DataManager.Instance.kvs_data.ReadInt(DefineOld.USER_SHISYUTU_PAR_HOUR));
		m_numShojikin.InitializeNumberOnly (DataManager.Instance.kvs_data.ReadInt(DefineOld.USER_SYOJIKIN));
		m_numTicket.InitializeNumberOnly (DataManager.Instance.kvs_data.ReadInt(DefineOld.USER_TICKET));

		DataManager.user.ParamUpdate ();

		/*
		m_numLevel.InitializeNumberOnly (DataManager.Instance.kvs_data.ReadInt(DefineOld.USER_LEVEL));
		m_numLevelNextExp.InitializeNumberOnly (DataManager.Instance.kvs_data.ReadInt(DefineOld.USER_NEXT_EXP));
		*/

		m_ctrlHeaderExp = gameObject.AddComponent<CtrlHeaderExp> ();
		m_ctrlHeaderExp.Initialize (m_slExp, m_lbLevel, m_lbLevelNextExp,m_lbNextLevelMade , m_popupAnimation);

		return;
	}

	// Update is called once per frame
	void Update () {
		setNum (m_lbSyakkin, m_numSyakkin , "" , "");
		setNum (m_lbUriagePerHour, m_numUriagePerHour ,"" ,"");
		setNum (m_lbShisyutsuPerHour, m_numShisyutsuPerHour,"" ,"");
		setNum (m_lbShojikin, m_numShojikin,"" ,"");
		setNum (m_lbTicket , m_numTicket);
		/*
		setNum (m_lbLevel , m_numLevel);
		setNum (m_lbLevelNextExp,m_numLevelNextExp);
		*/
	}



	private void setNum( UILabel _lb , CtrlDispNumbers _number , string _strFront = "" , string _strAfter = ""){
		_lb.text = string.Format( "{0}{1}{2}"  , _strFront , _number.m_iNumDisp , _strAfter );
	}

	public void RefleshNum(bool _bForce = false ){

		if (_bForce) {
			m_numSyakkin.ForceSet (DataManager.Instance.kvs_data.ReadInt (DefineOld.USER_SYAKKIN));
			m_numUriagePerHour.ForceSet (DataManager.Instance.kvs_data.ReadInt (DefineOld.USER_URIAGE_PAR_HOUR));
			m_numShisyutsuPerHour.ForceSet (DataManager.Instance.kvs_data.ReadInt (DefineOld.USER_SHISYUTU_PAR_HOUR));
			m_numShojikin.ForceSet (DataManager.Instance.kvs_data.ReadInt (DefineOld.USER_SYOJIKIN));
			m_numTicket.ForceSet (DataManager.Instance.kvs_data.ReadInt (DefineOld.USER_TICKET));
		} else {
			m_numSyakkin.Change (DataManager.Instance.kvs_data.ReadInt (DefineOld.USER_SYAKKIN));
			m_numUriagePerHour.Change (DataManager.Instance.kvs_data.ReadInt (DefineOld.USER_URIAGE_PAR_HOUR));
			m_numShisyutsuPerHour.Change (DataManager.Instance.kvs_data.ReadInt (DefineOld.USER_SHISYUTU_PAR_HOUR));
			m_numShojikin.Change (DataManager.Instance.kvs_data.ReadInt (DefineOld.USER_SYOJIKIN));
			m_numTicket.Change (DataManager.Instance.kvs_data.ReadInt (DefineOld.USER_TICKET));
		}

		/*
		m_numLevel.Change(DataManager.Instance.kvs_data.ReadInt(DefineOld.USER_LEVEL));
		m_numLevelNextExp.Change(DataManager.Instance.kvs_data.ReadInt(DefineOld.USER_NEXT_EXP));
		*/

	}


}
