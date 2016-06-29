﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CtrlParkRoot : MonoBehaviourEx {

	private bool m_bInitialized;

	public List<CtrlFieldItem> m_fieldItemList = new List<CtrlFieldItem>();

	public void MoveAdd( float _fX , float _fY ){
		myTransform.localPosition += new Vector3 (_fX, _fY, 0.0f); 
	}

	public void Initialize( List<DataItemParam> _itemList ){

		if (m_bInitialized == false) {

			myTransform.localPosition = new Vector3 (0.0f, -300.0f, 0.0f);

			m_fieldItemList.Clear ();

			GameObject prefab = PrefabManager.Instance.PrefabLoadInstance ("prefab/PrefFieldItem");

			List<Grid> ignoreGridList = new List<Grid> ();

			//Debug.Log ("Here");

			//DataManager.user.m_iWidth = 20;
			//DataManager.user.m_iHeight = 20;
			for (int x = 0; x < DataManager.user.m_iWidth+1; x++) {
				for( int y = 0; y < DataManager.user.m_iHeight+1;y++ ){

					if (IsGridIgnore (ignoreGridList, x, y)) {
						//Debug.Log ("same" + x.ToString () + " " + y.ToString ());
					} else {
						GameObject obj = PrefabManager.Instance.MakeObject (prefab, gameObject);

						obj.name = "fielditem_" + x.ToString () + "_" + y.ToString ();
						CtrlFieldItem script = obj.GetComponent<CtrlFieldItem> ();
						bool bHit = false;
						foreach (DataItemParam item in _itemList) {
							if (item.x == x && item.y == y) {
								script.Init (item);
								bHit = true;

								for (int dx = item.x; dx < item.x + item.width; dx++) {
									for (int dy = item.y; dy < item.y + item.height; dy++) {
										Grid ignoreGrid = new Grid (dx, dy);
										ignoreGridList.Add (ignoreGrid);
									}
								}
								break;
							}
						}
						if (bHit == false) {
							int iDummyItemId = 0;
							if (x == DataManager.user.m_iWidth || y == DataManager.user.m_iHeight) {
								iDummyItemId = -1;
							}
							script.Init (x, y, iDummyItemId);
						}
						//Debug.LogError ("here:" + obj.name);
						m_fieldItemList.Add (script);
					}
				}
			}

			// 所属しているモンスターのみを取り出す
			//Debug.LogError ("monster start");
			List<DataMonsterParam> monster_list = DataManager.Instance.dataMonster.Select (" item_serial != 0 ");
			foreach (DataMonsterParam monster in monster_list) {
				//Debug.Log (monster.item_serial);
				CtrlFieldItem fielditem = GetFieldItem (monster.item_serial);
				if (fielditem != null) {
					GameObject objIcon = PrefabManager.Instance.MakeObject ("prefab/PrefIcon", fielditem.gameObject);
					CtrlIconRoot iconRoot = objIcon.GetComponent<CtrlIconRoot> ();
					//iconRoot.m_iSize = fielditem.m_dataItemParam.width;
					iconRoot.Initialize (monster, fielditem);
					fielditem.Add (iconRoot);
				}
			}
			//Debug.LogError ("monster end");

			//Debug.LogError ("staff start");
			List<DataStaffParam> staff_list = DataManager.Instance.dataStaff.Select (" item_serial != 0 ");
			foreach (DataStaffParam staff in staff_list) {
				CtrlFieldItem fielditem = GetFieldItem (staff.item_serial);
				//Debug.Log (staff.item_serial);
				if (fielditem != null) {
					GameObject objIcon = PrefabManager.Instance.MakeObject ("prefab/PrefIcon", fielditem.gameObject);
					CtrlIconRoot iconRoot = objIcon.GetComponent<CtrlIconRoot> ();
					//iconRoot.m_iSize = fielditem.m_dataItemParam.width;
					iconRoot.Initialize (staff, fielditem);
					fielditem.Add (iconRoot);
				}
			}
			//Debug.LogError ("staff end");
		}

		m_bInitialized = true;
		GameMain.Instance.HeaderRefresh (true);
	}


	private bool IsGridIgnore( List<Grid> _ignoreGridList , int _x , int _y ){
		bool bRet = false;

		foreach (Grid checkGrid in _ignoreGridList) {
			if (checkGrid.Equals (_x, _y)) {
				bRet = true;
				break;
			}
		}
		return bRet;
	}


	public CtrlFieldItem GetFieldItem( int _iSerial ){
		foreach (CtrlFieldItem item in m_fieldItemList) {
			if (item.m_dataItemParam.item_serial == _iSerial) {
				return item;
			}
		}
		return null;
	}

	public CtrlFieldItem GetFieldItem( int _iX , int _iY ){
		foreach (CtrlFieldItem item in m_fieldItemList) {
			if (item.m_dataItemParam.x == _iX && item.m_dataItemParam.y == _iY) {
				return item;
			}
		}
		return null;
	}

	protected void RemoveFieldItem( int _iX , int _iY ){
		int iIndex = 0;
		CtrlFieldItem removeScript = null;
		foreach (CtrlFieldItem script in m_fieldItemList) {
			if (script.m_dataItemParam.x == _iX && script.m_dataItemParam.y == _iY) {
				removeScript = script;
				break;
			}
			iIndex += 1;
		}
		if (removeScript != null) {
			//Debug.Log ("removefielditem x:" + _iX.ToString () + " y:" + _iY.ToString ());
			removeScript.Remove ();
			m_fieldItemList.RemoveAt (iIndex);
		} else {
			//Debug.Log ("removefielditem null x:" + _iX.ToString() + " y:" + _iY.ToString ());
		}
		return;
	}

	public void AddFieldItem( CtrlFieldItem _ctrlFieldItem ){
		for (int x = _ctrlFieldItem.m_dataItemParam.x; x < _ctrlFieldItem.m_dataItemParam.x + _ctrlFieldItem.m_dataItemParam.width; x++) {
			for (int y = _ctrlFieldItem.m_dataItemParam.y ; y < _ctrlFieldItem.m_dataItemParam.y + _ctrlFieldItem.m_dataItemParam.height; y++) {
				RemoveFieldItem (x, y);
				/*
				CtrlFieldItem script = GetFieldItem (x, y);
				if (script != null) {
					script.Remove ();
				}
				*/
			}
		}
		_ctrlFieldItem.gameObject.name = "fielditem_" + _ctrlFieldItem.m_dataItemParam.x.ToString () + "_" + _ctrlFieldItem.m_dataItemParam.y.ToString ();
		m_fieldItemList.Add (_ctrlFieldItem);
		return;
	}

	protected bool checkRaod( int _iX , int _iY ){
		//Debug.Log( string.Format( "checkRoad x:{0} y{1}" , _iX , _iY ));
		bool bRet = false;
		CtrlFieldItem temp = GetFieldItem (_iX , _iY);
		// コントローラーがとれて、まだチェックしてないやつはチェック
		if (temp != null && temp.m_eRoad == DefineOld.ROAD.NO_CHECK ){
			//Debug.Log (temp.gameObject.name);
			if (temp.m_dataItemParam.item_id == DefineOld.ITEM_ID_ROAD) {
				bRet = true;
				temp.m_eRoad = DefineOld.ROAD.CONNECTION;
			} else {
				temp.m_eRoad = DefineOld.ROAD.DISCONNECT;
			}
		}
		return bRet;
	}

	protected void checkRaodSub( int _iX , int _iY ){

		// 自分のところは普通に調べる
		// というかこれやらなくてもよさそう
		// checkRaod (_iX, _iY);

		int iTempX = _iX;
		int iTempY = _iY;


		iTempX = _iX + 1;
		iTempY = _iY;
		if( checkRaod( iTempX , iTempY )){
			checkRaodSub (iTempX , iTempY);
		}
		iTempX = _iX;
		iTempY = _iY + 1;
		if( checkRaod( iTempX , iTempY )){
			checkRaodSub (iTempX , iTempY);
		}
		iTempX = _iX - 1;
		iTempY = _iY;
		if( checkRaod( iTempX , iTempY )){
			checkRaodSub (iTempX , iTempY);
		}
		iTempX = _iX;
		iTempY = _iY -1;
		if( checkRaod( iTempX , iTempY )){
			checkRaodSub (iTempX , iTempY);
		}
		return;
	}
	/*
	public void CheckRoadRoot( int _iX , int _iY ){

		// 侵入されたらまず自分のチェック
		if (GetFieldItem (_iX, _iY).m_dataItemParam.item_id == DefineOld.ITEM_ID_ROAD) {
			GetFieldItem (_iX, _iY).m_eRoad = DefineOld.ROAD.CONNECTION;
		}

		CtrlFieldItem temp = GetFieldItem (_iX + 1, _iY);

		// コントローラーがとれて、まだチェックしてないやつはチェック
		if (temp != null && temp.m_eRoad != DefineOld.ROAD.NO_CHECK ){
			if (temp.m_dataItemParam.item_id == DefineOld.ITEM_ID_ROAD) {
				temp.m_eRoad = DefineOld.ROAD.CONNECTION;
			} else {
				temp.m_eRoad = DefineOld.ROAD.DISCONNECT;
			}
		}
	}
	*/

	public void ConnectingRoadCheck(){

		// 状態を一度リセット
		foreach (CtrlFieldItem field_item in m_fieldItemList) {
			field_item.m_eRoad = DefineOld.ROAD.NO_CHECK;
		}

		checkRaodSub (1, 1);

		// 状態を一度リセット
		foreach (CtrlFieldItem field_item in m_fieldItemList) {
			switch ((DefineOld.Item.Category)field_item.m_dataItemParam.category) {
			case DefineOld.Item.Category.CAGE:
			case DefineOld.Item.Category.SHOP:
				if (field_item.m_dataItemParam.item_id != DefineOld.ITEM_ID_ROAD) {
					field_item.CheckAroundConnectRoad ();
				}
				break;
			default:
				break;
			}

		}


		return;
	}

	// Update is called once per frame
	void Update () {
	
	}
}


























