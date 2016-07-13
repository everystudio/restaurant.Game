﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RestaurantEdit : RestaurantPageBase {

	public enum STEP{
		NONE		= 0,
		IDLE		,
		MOVE_INIT	,
		MOVE_IDLE	,
		MOVE_SWIPE	,
		MOVE_MAP_SWIPE	,
		MOVE_SET	,
		MOVE_END	,
		SELECTING	,
		END			,
		MAX			,
	}
	public STEP m_eStep;
	public STEP m_eStepPre;

	public MapChipRestaurant m_mapchipRestaurant;
	public DataMapChipRestaurantParam m_paramMove;

	public List<MapGrid> m_DontSetGridList = new List<MapGrid> ();
	public bool m_bSetEditAble;
	public int m_iEditX;
	public int m_iEditY;
	public int m_iEditOffsetX;
	public int m_iEditOffsetY;

	[SerializeField]
	private ButtonBase m_btnEditYes;
	[SerializeField]
	private ButtonBase m_btnEditNo;

	protected override void initialize ()
	{
		m_eStep = STEP.NONE;
		m_eStepPre = STEP.MAX;
	}

	protected override void pageStart ()
	{
		m_eStep = STEP.IDLE;
		m_eStepPre = STEP.MAX;
		m_btnEditYes.TriggerClear ();
		m_btnEditNo.TriggerClear ();
		return;
	}

	protected override void pageEnd ()
	{
		return;
	}

	public override bool IsEnd ()
	{
		if (m_eStep == STEP.END) {
			return true;
		}
		return false;
	}


	// Update is called once per frame
	void Update () {

		bool bInit = false;
		if (m_eStepPre != m_eStep) {
			//Debug.LogError (m_eStep);
			m_eStepPre  = m_eStep;
			bInit = true;
		}

		switch (m_eStep) {
		case STEP.IDLE:
			if (bInit) {
				m_btnEditYes.TriggerClear ();
				m_btnEditNo.TriggerClear ();
				InputManager.Instance.Info.TouchUp = false;
			}

			if (InputManager.Instance.Info.TouchUp) {
				InputManager.Instance.Info.TouchUp = false;
				int iGridX = 0;
				int iGridY = 0;

				if (mapRoot.GetGrid (InputManager.Instance.Info.TouchPoint, out iGridX, out iGridY)) {
					Debug.Log (string.Format ("grid({0},{1})", iGridX, iGridY));

					if (DataManager.Instance.dataMapChipRestaurant.GetExist (iGridX, iGridY, out m_paramMove)) {
						Debug.LogError (m_paramMove.sample_string);
						m_eStep = STEP.MOVE_INIT;
					}

				} else {
					Debug.Log ("no hit");
				}
			}

			if (m_btnEditYes.ButtonPushed) {
				m_eStep = STEP.END;
			} else if (m_btnEditNo.ButtonPushed) {
				m_eStep = STEP.END;
			} else {
			}
			break;

		case STEP.MOVE_INIT:
			if (bInit) {
				m_DontSetGridList.Clear ();

				List<int> iSerialList = new List<int> ();
				iSerialList.Add (m_paramMove.mapchip_serial);
				MapGrid.SetUsingGrid (ref m_DontSetGridList, DataManager.Instance.dataMapChipRestaurant.GetActiveList (),iSerialList );

				int iRemoveIndex = 0;
				foreach (MapChipRestaurant mapchip in mapRoot.m_mapchipList) {
					if (mapchip.param.mapchip_serial == m_paramMove.mapchip_serial) {
						//item.Remove ();
						m_mapchipRestaurant = mapchip;

						m_iEditX = m_mapchipRestaurant.param.x;
						m_iEditY = m_mapchipRestaurant.param.y;
						//GameMain.Instance.m_iSettingItemId = moveMapChip.param.item_id;		// 別にここでやる必要はない
						mapRoot.m_mapchipList.RemoveAt (iRemoveIndex);
						break;
					}
					iRemoveIndex += 1;
				}
				//m_mapchipRestaurant.SetEditAble (true);
			}
			m_eStep = STEP.MOVE_IDLE;
			break;

		case STEP.MOVE_IDLE:
			if (bInit) {
				bool bAble = MapGrid.AbleSettingItem (m_mapchipRestaurant.param, m_iEditX, m_iEditY, mapRoot.map_data.GetWidth (), mapRoot.map_data.GetHeight (), m_DontSetGridList);
				if (m_bSetEditAble != bAble) {
					m_bSetEditAble = bAble;
					m_mapchipRestaurant.SetEditAble (m_bSetEditAble);
				}

				m_mapchipRestaurant.SetEditAble (m_bSetEditAble);
				InputManager.Instance.Info.TouchUp = false;
			}

			if (InputManager.Instance.Info.TouchUp) {
				InputManager.Instance.Info.TouchUp = false;
				if (mapRoot.GetGrid (InputManager.Instance.Info.TouchPoint, out m_iEditX, out m_iEditY)) {
					m_eStep = STEP.MOVE_SET;
				}
			}
			if (InputManager.Instance.Info.Swipe) {

				bool bHit = false;
				
				int iGridX = 0;
				int iGridY = 0;
				if (mapRoot.GetGrid (InputManager.Instance.Info.TouchPoint, out iGridX, out iGridY)) {
					if (mapRoot.GridHit (iGridX, iGridY, m_iEditX, m_iEditY, m_paramMove.width, m_paramMove.height, out m_iEditOffsetX, out m_iEditOffsetY)) {
						mapRoot.SetLockSwipeMove (true);
						bHit = true;
					}
				}
				if (bHit) {
					m_eStep = STEP.MOVE_SWIPE;
				} else {
					m_eStep = STEP.MOVE_MAP_SWIPE;
				}
			}
			break;

		case STEP.MOVE_SWIPE:

			int iMoveSwipeX = 0;
			int iMoveSwipeY = 0;
			if (mapRoot.GetGrid (InputManager.Instance.Info.TouchPoint, out iMoveSwipeX, out iMoveSwipeY)) {
				m_iEditX = iMoveSwipeX - m_iEditOffsetX;
				m_iEditY = iMoveSwipeY - m_iEditOffsetY;
				m_mapchipRestaurant.SetPos (m_iEditX, m_iEditY);
				bool bAble = MapGrid.AbleSettingItem (m_mapchipRestaurant.param, m_iEditX, m_iEditY, mapRoot.map_data.GetWidth (), mapRoot.map_data.GetHeight (), m_DontSetGridList);
				if (m_bSetEditAble != bAble) {
					m_bSetEditAble = bAble;
					m_mapchipRestaurant.SetEditAble (m_bSetEditAble);
				}
			}

			if (InputManager.Instance.Info.Swipe == false) {
				m_eStep = STEP.MOVE_IDLE;
				mapRoot.SetLockSwipeMove (false);
			}

			break;

		case STEP.MOVE_MAP_SWIPE:
			if (InputManager.Instance.Info.Swipe == false) {
				m_eStep = STEP.MOVE_IDLE;
			}
			break;

		case STEP.MOVE_SET:
			m_mapchipRestaurant.SetPos (m_iEditX, m_iEditY);
			m_eStep = STEP.MOVE_IDLE;
			break;

		case STEP.END:
			break;
		case STEP.MAX:
		default:
			break;
		}
	
	}
}




