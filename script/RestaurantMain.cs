using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RestaurantMain : Singleton<RestaurantMain> {

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

	[SerializeField]
	private MapRootRestaurant m_maprootRestaurant;
	public MapRootRestaurant mapRoot{
		get {
			return m_maprootRestaurant;
		}
	}

	[SerializeField]
	private Camera m_setCamera;

	// Use this for initialization
	void Start () {
		// このシーンはここから始まる

		UIParam.Instance.Reset ();

		m_eStep = STEP.IDLE;
		m_eStepPre = STEP.MAX;


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

		SpriteManager.Instance.LoadTexture2D (m_strTexture);

		m_imgTest.sprite = SpriteManager.Instance.SpriteCreate (m_strTexture, new Rect (0.0f, 98.0f, 64.0f, 98.0f), new Vector2(0.0f,0.0f));
		m_imgTest.rectTransform.sizeDelta = new Vector2 (m_imgTest.sprite.rect.width, m_imgTest.sprite.rect.height);

		/*
		DataMapChipRestaurant mapchip_sample = new DataMapChipRestaurant ();
		mapchip_sample.Load ("data/mapchip_sample");
		List<DataMapChipRestaurantParam> param_list = mapchip_sample.list;
		*/

		List<DataMapchipParam> param_list = new List<DataMapchipParam> ();
		foreach (DataMapchipParam param in DataManager.Instance.dataMapchip.list) {
			// ここで条件を追加する
			param_list.Add (param);
		}

		m_maprootRestaurant.Initialize (param_list,"data/mapdata_restaurant" , m_setCamera );

	}

	public float m_fTime;
	public int m_iAnim;
	public int m_iAnimOffset;
	public Image m_imgTest;
	public string m_strTexture;
	// Update is called once per frame
	void Update () {
		bool bInit = false;
		if (m_eStepPre != m_eStep) {
			m_eStepPre  = m_eStep;
			bInit = true;
		}

		m_fTime += Time.deltaTime;

		if (0.25f < m_fTime) {
			m_iAnim += 1;
			if (9 <= m_iAnim) {
				m_iAnim = 1;
			}
			m_iAnimOffset %= 8;
			m_imgTest.sprite = SpriteManager.Instance.SpriteCreate (m_strTexture, new Rect (64.0f*m_iAnim, 98.0f*m_iAnimOffset, 64.0f, 98.0f), new Vector2(0.0f,0.0f));
			m_fTime -= 0.25f;
		}


		switch (m_eStep) {
		case STEP.MAX:
		default:
			if (bInit) {
			}
			break;
		}
	
	}
}
