using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CtrlGate : MonoBehaviour {

	public enum STEP {
		IDLE		= 0,
		CHECK		,
		OUTPUT		,

		MAX			,
	}
	public STEP m_eStep;
	public STEP m_eStepPre;

	public float m_fTimer;

	public int m_iCapacity;

	private int m_iX;
	private int m_iY;
	public void Initialize(int _iX , int _iY ){
		m_iX = _iX;
		m_iY = _iY;
		m_iCapacity = DataManager.Instance.kvs_data.ReadInt (DefineOld.USER_LEVEL) / 2 ;
		m_eStep = STEP.IDLE;
		bool bDispVisitor = true;
		if (DataManager.Instance.data_kvs.HasKey (DataManager.Instance.KEY_DISP_VISITOR)) {
			if (DataManager.Instance.data_kvs.ReadInt (DataManager.Instance.KEY_DISP_VISITOR) == 0) {
				bDispVisitor = false;
			}
		}
		if (bDispVisitor == false) {
			m_eStep = STEP.MAX;
		}
		m_eStepPre = STEP.MAX;

		//InvokeRepeating ("InvokeTest", 1.0f, 1.0f);
		return;
	}
	void InvokeTest(){
		Debug.LogError ("InvokeTest");
	}

	public void OnDisable(){
		CancelInvoke ("InvokeTest");
		Debug.LogError ("cancel invoke InvokeTest");

	}

	public int m_iVisitorNum;

	List<CtrlVisitor> visitor_list = new List<CtrlVisitor>();

	void Update(){

		bool bInit = false;
		if (m_eStepPre != m_eStep) {
			m_eStepPre  = m_eStep;
			bInit = true;
		}

		switch( m_eStep ){
		case STEP.IDLE:
			if (bInit) {
				m_fTimer = 0.0f;
			}
			m_fTimer += Time.deltaTime;
			if (3.0f < m_fTimer) {
				int iActiveNum = 0;
				foreach (CtrlVisitor visitor in visitor_list) {
					if (visitor.IsActive ()) {
						iActiveNum += 1;
					}
				}
				m_iCapacity = DataManager.Instance.kvs_data.ReadInt (DefineOld.USER_LEVEL);

				if (iActiveNum < m_iCapacity) {
					m_eStep = STEP.OUTPUT;
				} else {
					m_fTimer = 0.0f;
				}
			}
			break;

		case STEP.OUTPUT:
			List<DataItemParam > check_list = new List<DataItemParam> ();
			int x = m_iX + 2;
			int y = m_iY + 1;
			DataItemParam param = DataManager.Instance.m_dataItem.SelectOne (string.Format (" item_id = {0} and x = {1} and y = {2} ", DefineOld.ITEM_ID_ROAD, x, y));
			if (param.item_id == DefineOld.ITEM_ID_ROAD) {
				check_list.Add (param);
			}
			x = m_iX + 1;
			y = m_iY + 2;
			param = DataManager.Instance.m_dataItem.SelectOne (string.Format (" item_id = {0} and x = {1} and y = {2} ", DefineOld.ITEM_ID_ROAD, x, y));
			if (param.item_id == DefineOld.ITEM_ID_ROAD) {
				check_list.Add (param);
			}
			if (0 < check_list.Count) {
				int iIndex = UtilRand.GetRand (check_list.Count);
				int road_serial = check_list [iIndex].item_serial;

				int iVisitorType = 1 + UtilRand.GetRand (5);//ダサい

				bool bChecked = false;
				foreach (CtrlVisitor visitor in visitor_list) {
					if (visitor.IsActive () == false) {
						bChecked = true;
						visitor.Initialize (iVisitorType, road_serial);
						break;
					}
				}
				if (bChecked == false) {
					CtrlVisitor visitor = PrefabManager.Instance.MakeScript<CtrlVisitor> ("prefab/PrefVisitor", transform.parent.gameObject);
					visitor.Initialize (iVisitorType, road_serial);
					visitor_list.Add (visitor);
				}
			}
			m_eStep = STEP.IDLE;
			break;

		case STEP.MAX:
		default:
			break;

		}
	}



}
