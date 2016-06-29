﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CtrlItemDetailOffice : CtrlItemDetailBase {
	#region SerializeField
	[SerializeField]
	private UILabel m_lbCostNow;
	[SerializeField]
	private UILabel m_lbCostMax;

	[SerializeField]
	private UILabel m_lbShisyutsu;

	[SerializeField]
	private GameObject m_goRootPosition;

	#endregion

	List<CtrlFieldItem> m_areaFieldItem = new List<CtrlFieldItem> ();

	private bool m_bRemove;

	override protected void initialize(){

		m_bRemove = false;
		m_areaFieldItem.Clear ();

		m_ctrlFieldItem.gameObject.transform.parent = m_goRootPosition.transform;
		m_ctrlFieldItem.gameObject.transform.localScale = Vector3.one;
		m_ctrlFieldItem.ResetPos ();

		CsvItemParam master_data = DataManager.Instance.m_csvItem.Select( m_dataItemParam.item_id );

		CsvItemDetailData item_detail = DataManager.GetItemDetail (m_dataItemParam.item_id, m_dataItemParam.level);
		m_lbCostMax.text = item_detail.cost.ToString();

		int iCostNow = 0;
		int iShisyutsu = 0;
		List<DataStaffParam> staff_list = DataManager.Instance.dataStaff.Select (" office_serial = " + m_dataItemParam.item_serial.ToString() + " " );
		foreach (DataStaffParam staff in staff_list) {
			CsvStaffParam data = DataManager.GetStaff (staff.staff_id);

			iCostNow += data.cost;
			iShisyutsu += data.expenditure;		// わかりにくい名前にしてしまったな
		}
		m_lbCostNow.text = iCostNow.ToString ();
		m_lbShisyutsu.text = iShisyutsu.ToString ();

		//Debug.Log ("count=" + DataManager.Instance.m_ItemDataList.Count);

		for( int x = m_dataItemParam.x - (master_data.area ) ; x < m_dataItemParam.x + master_data.size + (master_data.area ) ; x++ ){
			for( int y = m_dataItemParam.y - (master_data.area ) ; y < m_dataItemParam.y + master_data.size + (master_data.area ) ; y++ ){
				//Debug.Log ("x=" + x.ToString () + " y=" + y.ToString ());

				//foreach (DataItem data_item in DataManager.Instance.m_ItemDataList) {
				foreach (CtrlFieldItem field_item in GameMain.ParkRoot.m_fieldItemList) {

					// xyが合ってて、シリアルは別
					if (field_item.m_dataItemParam.x == x && field_item.m_dataItemParam.y == y && m_dataItemParam.item_serial != field_item.m_dataItemParam.item_serial ) {
						//CtrlFieldItem script = GameMain.ParkRoot.GetFieldItem (data_item.item_serial);
						CtrlFieldItem script = field_item;
						m_areaFieldItem.Add (script);
						script.gameObject.transform.parent = m_goRootPosition.transform;
						script.gameObject.transform.localScale = Vector3.one;

						script.ResetPos ();
					}
				}
			}
		}

		/*
		float fScale = 0.5f;
		m_goRootPosition.transform.localPosition = (-1.0f * DefineOld.CELL_X_DIR.normalized * DefineOld.CELL_X_LENGTH * m_dataItemParam.x) + (-1.0f * DefineOld.CELL_Y_DIR.normalized * DefineOld.CELL_Y_LENGTH * m_dataItemParam.y + new Vector3(0.0f, -240.0f,0.0f ));
		*/
		float fScale = 0.5f;
		m_goRootPosition.transform.localScale = new Vector3 (fScale, fScale, fScale);
		m_goRootPosition.transform.localPosition = (-1.0f * DefineOld.CELL_X_DIR.normalized * DefineOld.CELL_X_LENGTH * m_dataItemParam.x) + (-1.0f * DefineOld.CELL_Y_DIR.normalized * DefineOld.CELL_Y_LENGTH * m_dataItemParam.y + new Vector3(0.0f, -240.0f,0.0f ));
		m_goRootPosition.transform.localPosition *= fScale;

		return;
	}
	override protected void remove(){
		m_bRemove = true;
		return;
	}


	override protected void close(){

		//m_goRootPosition.transform.localScale = Vector3.one;

		if (m_bRemove == false) {
			//Debug.Log ("remove false");
			// 閉じる的な終了時
			m_ctrlFieldItem.gameObject.transform.parent = GameMain.ParkRoot.transform;
			m_ctrlFieldItem.gameObject.transform.localScale = Vector3.one;
			m_ctrlFieldItem.SetPos (m_dataItemParam.x, m_dataItemParam.y);
		} else {
			//Debug.Log ("remove true");
			// 閉じる的な終了時
			Destroy (m_ctrlFieldItem.gameObject);
		}

		foreach (CtrlFieldItem field_item in m_areaFieldItem) {
			field_item.gameObject.transform.parent = GameMain.ParkRoot.transform;
			field_item.gameObject.transform.localScale = Vector3.one;
			field_item.ResetPos ();
		}


		// 片付けルートは別にする必要あり

		return;
	}


}









