using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RestaurantMain : MonoBehaviour {

	public enum STEP
	{
		NONE		= 0,
		IDLE		,
		EDIT		,

		MAX			,
	}
	[SerializeField]
	private STEP m_eStep;
	private STEP m_eStepPre;

	private List<GameObject> m_canvasList = new List<GameObject> ();
	[SerializeField]
	private RestaurantPageBase m_restaurantIdle;
	[SerializeField]
	private RestaurantPageBase m_restaurantEdit;

	[SerializeField]
	private MapRootRestaurant m_maprootRestaurant;
	[SerializeField]
	private Camera m_setCamera;

	// Use this for initialization
	void Start () {
		// このシーンはここから始まる
		Debug.Log( "RestaurantMain.Start()" );

		m_eStep = STEP.IDLE;
		m_eStepPre = STEP.MAX;

		m_restaurantIdle.Initialize (m_maprootRestaurant);
		m_restaurantEdit.Initialize (m_maprootRestaurant);

		m_canvasList.Add (m_restaurantIdle.gameObject);
		m_canvasList.Add (m_restaurantEdit.gameObject);


		// 画像読み込み
		SpriteManager.Instance.LoadAtlas ("atlas/ad001");
		SpriteManager.Instance.LoadAtlas ("atlas/back001");
		SpriteManager.Instance.LoadAtlas ("atlas/back002");
		SpriteManager.Instance.LoadAtlas ("atlas/item001");
		SpriteManager.Instance.LoadAtlas ("atlas/item002");
		SpriteManager.Instance.LoadAtlas ("atlas/item003");
		SpriteManager.Instance.LoadAtlas ("atlas/item004");
		SpriteManager.Instance.LoadAtlas ("atlas/item005");
		SpriteManager.Instance.LoadAtlas ("atlas/monster001");
		SpriteManager.Instance.LoadAtlas ("atlas/monster002");
		SpriteManager.Instance.LoadAtlas ("atlas/staff001");
		SpriteManager.Instance.LoadAtlas ("atlas/tutorial001");
		SpriteManager.Instance.LoadAtlas ("atlas/tutorial002");
		SpriteManager.Instance.LoadAtlas ("atlas/tutorial003");
		SpriteManager.Instance.LoadAtlas ("atlas/ui001");
		SpriteManager.Instance.LoadAtlas ("atlas/ui002");
		SpriteManager.Instance.LoadAtlas ("atlas/ui003");



		/*
		DataMapChipRestaurant mapchip_sample = new DataMapChipRestaurant ();
		mapchip_sample.Load ("data/mapchip_sample");
		List<DataMapChipRestaurantParam> param_list = mapchip_sample.list;
		*/

		List<DataMapChipRestaurantParam> param_list = new List<DataMapChipRestaurantParam> ();
		foreach (DataMapChipRestaurantParam param in DataManager.Instance.dataMapChipRestaurant.list) {
			// ここで条件を追加する
			param_list.Add (param);
		}

		m_maprootRestaurant.Initialize (param_list,"data/mapdata_restaurant" , m_setCamera );
	
	}
	
	// Update is called once per frame
	void Update () {
		bool bInit = false;
		if (m_eStepPre != m_eStep) {
			m_eStepPre  = m_eStep;
			bInit = true;
		}
		switch (m_eStep) {

		case STEP.IDLE:
			if (bInit) {
				m_restaurantIdle.PageStart ();
			}
			if (m_restaurantIdle.IsEnd ()) {
				m_restaurantIdle.PageEnd ();
				m_eStep = STEP.EDIT;
			}
			break;
		case STEP.EDIT:
			if (bInit) {
				m_restaurantEdit.PageStart ();
			}
			if (m_restaurantEdit.IsEnd ()) {
				m_restaurantEdit.PageEnd ();
				m_eStep = STEP.IDLE;
			}
			break;
		case STEP.MAX:
		default:
			break;
		}
	
	}
}
