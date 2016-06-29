using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CtrlDispOffice : CtrlItemDetailBase {

	private List<CtrlFieldItem> m_areaFieldItem = new List<CtrlFieldItem> ();
	private GameObject m_goRootPosition;

	private GameObject m_goBlackBack;

	override protected void close(){
		m_goRootPosition.transform.localScale = Vector3.one;

		m_ctrlFieldItem.gameObject.transform.parent = GameMain.ParkRoot.transform;
		m_ctrlFieldItem.gameObject.transform.localScale = Vector3.one;
		m_ctrlFieldItem.SetPos (m_dataItemParam.x, m_dataItemParam.y);
		foreach (CtrlFieldItem field_item in m_areaFieldItem) {
			field_item.gameObject.transform.parent = GameMain.ParkRoot.transform;
			field_item.gameObject.transform.localScale = Vector3.one;
			field_item.ResetPos ();
			field_item.SetColor (Color.white);
		}
		Destroy (m_goRootPosition);
		Release (m_goBlackBack);

		return;
	}

	public void SetColor( DefineOld.Item.Category _category , Color _color ){
		foreach (CtrlFieldItem script in m_areaFieldItem) {
			if (script.m_dataItemParam.category == (int)_category) {
				script.SetColor (_color);
			}
		}
		return;
	}

	// これを利用して初期化する
	public void Initialize( int _iItemSerial , GameObject _rootPosition ){
		if (m_goRootPosition == null) {
			m_goRootPosition = new GameObject ();
		}
		Debug.LogError ("here");
		m_goRootPosition.name = "ctrldispoffice_rootposition";		// 別に名付ける必要はなかったんですけどね
		m_goRootPosition.transform.parent = _rootPosition.transform;
		m_goRootPosition.transform.localScale = Vector3.one;
		//m_goRootPosition.transform.localScale = new Vector3( 0.5f , 0.5f , 0.5f );
		m_goRootPosition.transform.localPosition = Vector3.zero;
		// ここ別にbaseいらないね
		m_goBlackBack = PrefabManager.Instance.MakeObject ("prefab/PrefBlackBack", m_goRootPosition);
		m_goBlackBack.name = DataManager.Instance.KEY_TOUCHABLE_FIELD_NAME;
		m_goBlackBack.transform.localPosition = new Vector3 (0.0f, 0.0f, -10.0f);
		//m_goBlackBack.name = string.Format ("{0}(fromCtrlDispOffice)", m_goBlackBack.name);
		base.Initialize (_iItemSerial);

		return;
	}

	override protected void initialize ()
	{
		m_ctrlFieldItem.gameObject.transform.parent = m_goRootPosition.transform;
		m_ctrlFieldItem.transform.localScale = Vector3.one;
		m_ctrlFieldItem.ResetPos ();

		CsvItemParam master_data = DataManager.Instance.m_csvItem.Select( m_dataItemParam.item_id );

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
		for( int x = m_dataItemParam.x - (master_data.area ) ; x < m_dataItemParam.x + master_data.size + (master_data.area ) ; x++ ){
			for( int y = m_dataItemParam.y - (master_data.area ) ; y < m_dataItemParam.y + master_data.size + (master_data.area ) ; y++ ){
				//Debug.Log ("x=" + x.ToString () + " y=" + y.ToString ());
				foreach (DataItem data_item in DataManager.Instance.m_ItemDataList) {

					// xyが合ってて、シリアルは別
					if (data_item.x == x && data_item.y == y && m_dataItemParam.item_serial != data_item.item_serial ) {
						CtrlFieldItem script = GameMain.ParkRoot.GetFieldItem (data_item.item_serial);
						m_areaFieldItem.Add (script);
						script.gameObject.transform.parent = m_goRootPosition.transform;
						script.ResetPos ();
					}
				}
			}
		}
		*/

		float fScale = 0.5f;
		m_goRootPosition.transform.localScale = new Vector3 (fScale, fScale, fScale);
		m_goRootPosition.transform.localPosition = (-1.0f * DefineOld.CELL_X_DIR.normalized * DefineOld.CELL_X_LENGTH * m_dataItemParam.x) + (-1.0f * DefineOld.CELL_Y_DIR.normalized * DefineOld.CELL_Y_LENGTH * m_dataItemParam.y + new Vector3(0.0f, -240.0f,0.0f ));
		m_goRootPosition.transform.localPosition *= fScale;

		return;
	}

	private int m_iSelectingCageSerial;
	public int SelectingCageSerial{
		get{ return m_iSelectingCageSerial; }
	}

	// Update is called once per frame
	void Update () {
		if (InputManager.Info.TouchON) {
			int iGridX = 0;
			int iGridY = 0;

			int iSelectCageSerial = 0;

			if (m_goRootPosition) {
				if (GameMain.GetGrid (m_goRootPosition, InputManager.Info.TouchPoint, out iGridX, out iGridY)) {
					//Debug.Log ("x=" + iGridX.ToString () + " y=" + iGridY.ToString ());

					foreach (DataItemParam data_item_param in DataManager.Instance.m_dataItem.list) {
						if (GameMain.GridHit (iGridX, iGridY, data_item_param)) {

							if (data_item_param.category == (int)DefineOld.Item.Category.CAGE) {
								iSelectCageSerial = data_item_param.item_serial;
								m_iSelectingCageSerial = iSelectCageSerial;
								break;
							}
						}
					}
					/*
				foreach (DataItem data_item in DataManager.Instance.m_ItemDataList) {
					// xyが合ってて、シリアルは別
					if (data_item.x == iGridX && data_item.y == iGridY && data_item.category == (int)DefineOld.Item.Category.CAGE ) {
						iSelectCageSerial = data_item.item_serial;
						m_iSelectingCageSerial = iSelectCageSerial;
						break;
					}
				}
				*/
					/*
				if (0 < m_iSelectingCageSerial) {
					CtrlFieldItem script = GameMain.ParkRoot.GetFieldItem (m_iSelectingCageSerial);
					script.SetColor (Color.white);
					if (0 < iSelectCageSerial) {
						CtrlFieldItem script = GameMain.ParkRoot.GetFieldItem (iSelectCageSerial);
						script.SetColor (Color.red);
						m_iSelectingCageSerial = iSelectCageSerial;
					}
				}
				*/
				}
			}
		}


		// 勝手にちっさくなるのでここで再度修正。苛ついてるのでずっとスケールかけてます
		// こういうのがあるからUnity嫌いなんだよね
		//m_goBlackBack.transform.localScale = new Vector3 (100.0f, 100.0f, 100.0f);

	}
}
