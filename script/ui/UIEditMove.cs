using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIEditMove : CPanel {

	public enum STEP{
		NONE		= 0,
		// 移動とか
		MOVE_INIT	,
		MOVE_IDLE	,
		MOVE_SWIPE	,
		MOVE_MAP_SWIPE	,
		MOVE_SET	,
		MOVE_END	,

		END					,
		MAX					,
	}
	public STEP m_eStep;
	public STEP m_eStepPre;

	private MapRootRestaurant mapRoot {
		get{
			return RestaurantMain.Instance.mapRoot;
		}
	}

	private CtrlBackyardItem m_ctrlBackyardItem;
	public MapChipRestaurant m_mapchipRestaurant;
	public DataMapchipParam m_paramMove;

	public List<MapGrid> m_DontSetGridList = new List<MapGrid> ();
	public bool m_bSetEditAble;
	public int m_iEditX;
	public int m_iEditY;
	public int m_iEditOffsetX;
	public int m_iEditOffsetY;

	protected override void panelStart ()
	{
		//Debug.LogError ( string.Format("UIEditMove.panelStart:{0}" ,UIParam.Instance.m_iEditMapChipSerial));
		if (UIParam.Instance.m_iEditMapChipSerial == 0) {
			//Debug.LogError ("here");
			UIAssistant.main.ShowPage ("EditIdle");
			return;
		}

		m_eStep = STEP.MOVE_INIT;
		m_eStepPre = STEP.MAX;
		foreach (DataMapchipParam param in DataManager.Instance.dataMapchip.list) {
			if (param.mapchip_serial == UIParam.Instance.m_iEditMapChipSerial) {
				m_paramMove = param;
			}
		}
		return;
	}

	protected override void panelEndStart ()
	{
		//Debug.LogError ("panelEndStart");
		base.panelEndStart ();

		if (m_mapchipRestaurant != null) {
			Destroy (m_mapchipRestaurant.gameObject);
			m_mapchipRestaurant = null;
		}
	}


	#region イベント関数
	private void onBackyard(){
		// 参照渡しなので、一応これでDatamanagerの方も変わる。ポインターが使いたいよー
		m_paramMove.x = -1;
		m_paramMove.y = -1;

		UIParam.Instance.m_iEditMapChipSerial = 0;
	}

	private void onFix(){
		//Debug.LogError (string.Format ("onFix:{0}", gameObject.name));
		m_paramMove.x = m_iEditX;
		m_paramMove.y = m_iEditY;
		if (m_mapchipRestaurant != null) {
			// なんか知らんけど2回呼ばれる
			RestaurantMain.Instance.mapRoot.AddFieldItem (m_mapchipRestaurant);
			m_mapchipRestaurant = null;
			UIAssistant.main.ShowPage ("EditIdle");
			UIParam.Instance.m_iEditMapChipSerial = 0;
			//Debug.LogError ("null:m_mapchipRestaurant");
		}
	}

	private void onSell(){
		UIParam.Instance.m_iEditMapChipSerial = 0;
	}

	public void onBackyardStart(){
	}

	#endregion


	void Update(){

		bool bInit = false;
		if (m_eStepPre != m_eStep) {
			//Debug.LogError (m_eStep);
			m_eStepPre  = m_eStep;
			bInit = true;
		}

		switch (m_eStep) {

		case STEP.MOVE_INIT:
			if (bInit) {
				m_DontSetGridList.Clear ();

				List<int> iSerialList = new List<int> ();
				iSerialList.Add (m_paramMove.mapchip_serial);
				MapGrid.SetUsingGrid (ref m_DontSetGridList, DataManager.Instance.dataMapchip.GetActiveList (), iSerialList);
				//Debug.LogError (m_paramMove.mapchip_serial);
				//Debug.LogError (mapRoot.m_mapchipList.Count);

				int iRemoveIndex = 0;
				foreach (MapChipRestaurant mapchip in mapRoot.m_mapchipList) {
					if (mapchip.param.mapchip_serial == m_paramMove.mapchip_serial) {
						//Debug.LogError ("here");
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

				if (m_mapchipRestaurant == null ) {

					MapChipRestaurant script = PrefabManager.Instance.AddGameObject<MapChipRestaurant> (RestaurantMain.Instance.mapRoot.gameObject);
					script.gameObject.name = string.Format ("fielditem_serial" ,m_paramMove.mapchip_serial);
					script.Initialize (RestaurantMain.Instance.mapRoot.map_data, m_paramMove);
					m_iEditX = 3;
					m_iEditY = 3;
					script.SetPos (m_iEditX, m_iEditY);
					m_mapchipRestaurant = script;
				}
				m_mapchipRestaurant.ShowEditMenu (true);
				m_mapchipRestaurant.OnBackyard.AddListener (onBackyard);
				m_mapchipRestaurant.OnFix.AddListener (onFix);

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
		}

	}

}
