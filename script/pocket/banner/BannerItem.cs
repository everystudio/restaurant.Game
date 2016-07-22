using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(ButtonBase))]
public class BannerItem : BannerBase {
	public enum STEP
	{
		NONE			= 0,
		IDLE			,
		DETAIL			,

		EXPAND_CHECK	,
		EXPAND_BUY		,

		TICKET_CHECK	,
		TICKET_BUY		,

		GOLD_CHECK		,
		GOLD_BUY		,

		MAX				,
	}
	public STEP m_eStep;
	public STEP m_eStepPre;

	#region SerializeField設定
	[SerializeField]
	private UI2DSprite m_sprBuyBase;
	[SerializeField]
	private UILabel m_lbBuyPrice;
	#endregion

	public int m_iItemId;
	public int m_iItemSerial;
	private ButtonBase m_buttonBase;
	public int m_iTicketNum;

	public CsvItemParam m_ItemMaster;
	public CtrlOjisanCheck m_ojisanCheck;


	public bool Initialize( CsvItemParam _data , int _iCostNokori ){

		bool bRet = true;

		m_iTicketNum = 0;
		m_ItemMaster = _data;
		m_bIsUserData = false;

		m_iItemId = _data.item_id;
		m_iItemSerial = 0;
		m_buttonBase = GetComponent<ButtonBase> ();

		m_lbTitle.text = _data.name;
		m_lbTitle2.text = m_lbTitle.text;
		m_lbDescription.text = _data.description;

		m_lbPrize.text = _data.size.ToString();
		m_lbPrizeExp.text = _data.cost.ToString ();

		m_lbDifficulty.text = "";

		string strIcon = GetSpriteName (_data);
		//UIAtlas atlas = AtlasManager.Instance.GetAtlas (strIcon);
		//m_sprIcon.atlas = atlas;
		m_sprIcon.sprite2D = SpriteManager.Instance.Load( strIcon );
		SpriteIconAdjust (m_sprIcon);

		SetPrice (_data);

		// 上限確認の為にここで所持数チェック
		int iHave = DataManager.Instance.m_dataItem.Select (string.Format (" item_id = {0} ", _data.item_id)).Count;

		m_bAbleUse = DataManager.Instance.user.AbleBuy (_data.need_coin, _data.need_ticket, 0 , _iCostNokori , iHave , _data.setting_limit ,ref m_eReason);
		SetReasonSprite (m_sprReason, m_eReason);
		m_sprIgnoreBlack.gameObject.SetActive (!m_bAbleUse);
		if ((DefineOld.Item.Category)_data.category == DefineOld.Item.Category.SHOP) {
			m_lbPrize.text = _data.size.ToString();
			m_lbPrizeExp.text = "";
			m_sprBackground.sprite2D= SpriteManager.Instance.Load (string.Format ("texture/ui/list_item_2.png"));
			m_lbDifficulty.text = UtilString.GetSyuunyuu( m_ItemMaster.revenue , m_ItemMaster.revenue_interval );
		} else if ((DefineOld.Item.Category)_data.category == DefineOld.Item.Category.EXPAND || 
			(DefineOld.Item.Category)_data.category == DefineOld.Item.Category.GOLD ||
			(DefineOld.Item.Category)_data.category == DefineOld.Item.Category.TICKET ) {
			m_sprBackground.sprite2D= SpriteManager.Instance.Load (string.Format ("texture/ui/list_item_4.png"));
			m_lbPrize.text = "";
			m_lbPrizeExp.text = "";
			m_lbDifficulty.text = "";
		} else {
		}
		SetEnableIcon (m_bAbleUse);
		// こっちのInitializeは通る

		//Debug.LogError ( string.Format( "item_id={0} setting_limit={1}" , _data.item_id,_data.setting_limit));
		if (m_eReason == ABLE_BUY_REASON.LIMIT) {
			bRet = false;
		}

		m_eStep = STEP.IDLE;
		m_eStepPre = STEP.MAX;
		return bRet;

	}
	public bool Initialize( DataItemParam _data , int _iCostNokori ){

		CsvItemParam item_master = DataManager.GetItemMaster (_data.item_id);

		Debug.LogError (string.Format ("name:{0} item_id:{1}", item_master.name, item_master.item_id));

		Initialize (item_master , _iCostNokori );
		m_iItemSerial = _data.item_serial;

		m_bIsUserData = true;
		m_sprBuyBase.gameObject.SetActive (false);

		//m_bAbleUse = DataManager.Instance.user.AbleBuy (0 , 0, 0 , _iCostNokori);
		m_bAbleUse = DataManager.Instance.user.AbleBuy (0, 0, 0, 0, 0, 0, ref m_eReason);

		//m_lbReason.gameObject.SetActive (!m_bAbleUse);
		SetReasonSprite (m_sprReason, m_eReason);
		m_sprIgnoreBlack.gameObject.SetActive (!m_bAbleUse);
		SetEnableIcon (m_bAbleUse);

		return true;
	}

	private void SetPrice( CsvItemParam _data ){
		string strText = "";
		string strImageName = "";
		if (0 < _data.need_coin) {
			strImageName = "list_buy1";
			strText = _data.need_coin.ToString () + "G";
		} else if (0 < _data.need_ticket) {
			strImageName = "list_buy2";
			strText = _data.need_ticket.ToString () + "枚";
		} else if (0 < _data.need_money) {
			strImageName = "list_buy3";
			strText = _data.need_money.ToString () + "円";
		} else {
			Debug.LogError ("no need");
		}
		m_sprBuyBase.sprite2D = SpriteManager.Instance.Load (string.Format ("texture/ui/{0}.png", strImageName));
		m_lbBuyPrice.text = strText;
		return;
	}

	static public string GetItemSpriteName( int _iItemId ){
		string strRet = "";

		bool bIsUI = true;

		switch (_iItemId) {

		case 30:
			strRet = "ticket010";
			break;
		case 31:
			strRet = "ticket055";
			break;
		case 32:
			strRet = "ticket125";
			break;
		case 33:
			strRet = "ticket350";
			break;
		case 34:
			strRet = "ticket800";
			break;
		case 35:
			strRet = "coin1000";
			break;
		case 36:
			strRet = "coin5500";
			break;
		case 37:
			strRet = "coin125000";
			break;
		case 38:
			strRet = "coin550000";
			break;

		default:
			bIsUI = false;
			strRet = string.Format ("item{0:D2}_01", _iItemId);
			//Debug.LogError (strRet);
			break;
		}

		if (bIsUI) {
			strRet = string.Format ("texture/ui/{0}.png", strRet);
		} else {
			strRet = string.Format ("texture/item/{0}.png", strRet);
		}
		return strRet;
	}

	private string GetSpriteName( CsvItemParam _data ){

		return GetItemSpriteName(_data.item_id);
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
				m_buttonBase.TriggerClear ();
			}
			if (m_buttonBase.ButtonPushed) {
				m_buttonBase.TriggerClear ();
				//Debug.Log (m_ItemMaster.category);
				if (m_bAbleUse) {

					switch ((DefineOld.Item.Category) m_ItemMaster.category) {

					case DefineOld.Item.Category.EXPAND:
						m_eStep = STEP.EXPAND_CHECK;
						break;

					case DefineOld.Item.Category.TICKET:

						if (m_ItemMaster.need_coin <= DataManager.Instance.user.m_iGold) {
							m_eStep = STEP.TICKET_CHECK;
						} else {
							m_bAbleUse = false;
							SetEnableIcon (m_bAbleUse);
						}
						break;
					case DefineOld.Item.Category.GOLD:
						if (m_ItemMaster.need_ticket <= DataManager.Instance.user.m_iTicket) {
							m_eStep = STEP.GOLD_CHECK;
						} else {
							m_bAbleUse = false;
							SetEnableIcon (m_bAbleUse);
						}
						break;
					default:
						GameMain.Instance.PreSettingItemId = 0;
						SoundManager.Instance.PlaySE (SoundName.BUTTON_PUSH, "https://s3-ap-northeast-1.amazonaws.com/every-studio/app/sound/se");
						GameMain.Instance.SettingItem (m_iItemId, m_iItemSerial);
						GameMain.Instance.SetStatus (GameMain.STATUS.PARK);
						break;
					}
				}
			}
			break;

		case STEP.EXPAND_CHECK:
			if (bInit) {
				GameObject objOjisan = PrefabManager.Instance.MakeObject ("prefab/PrefOjisanCheck", gameObject.transform.parent.parent.parent.parent.gameObject );
				m_ojisanCheck = objOjisan.GetComponent<CtrlOjisanCheck> ();
				m_ojisanCheck.Initialize ("動物園を\n拡張します\n\nよろしいですか");
			}
			if (m_ojisanCheck.IsYes ()) {
				SoundManager.Instance.PlaySE (SoundName.BUTTON_PUSH, "https://s3-ap-northeast-1.amazonaws.com/every-studio/app/sound/se");
				Destroy (m_ojisanCheck.gameObject);
				m_eStep = STEP.EXPAND_BUY;
			} else if (m_ojisanCheck.IsNo ()) {
				SoundManager.Instance.PlaySE (SoundName.BUTTON_PUSH, "https://s3-ap-northeast-1.amazonaws.com/every-studio/app/sound/se");
				Destroy (m_ojisanCheck.gameObject);
				m_eStep = STEP.IDLE;
			} else {
			}
			break;

		case STEP.EXPAND_BUY:
			if (bInit) {
				GameObject objOjisan = PrefabManager.Instance.MakeObject ("prefab/PrefOjisanCheck", gameObject.transform.parent.parent.parent.parent.gameObject);
				m_ojisanCheck = objOjisan.GetComponent<CtrlOjisanCheck> ();
				m_ojisanCheck.Initialize ("拡張いたしました！", true);

				DataManager.Instance.m_dataItem.Insert (m_ItemMaster, 0, 0, 0);
				DataItem.OpenNewItem (m_ItemMaster.item_id);

				GameObject prefab = PrefabManager.Instance.PrefabLoadInstance ("prefab/PrefFieldItem");
				DataManager.Instance.user.AddGold (-1 * m_ItemMaster.need_coin);
				for (int x = 0; x < DataManager.Instance.user.m_iWidth + DefineOld.EXPAND_FIELD + 1; x++) {
					for (int y = 0; y < DataManager.Instance.user.m_iHeight + DefineOld.EXPAND_FIELD + 1; y++) {
						if ( DataManager.Instance.user.m_iWidth <= x || DataManager.Instance.user.m_iHeight <= y ) {

							CtrlFieldItem script = null;
							script = GameMain.ParkRoot.GetFieldItem (x, y);

							if (script == null) {
								GameObject obj = PrefabManager.Instance.MakeObject (prefab, GameMain.ParkRoot.gameObject);
								obj.name = "fielditem_" + x.ToString () + "_" + y.ToString ();
								script = obj.GetComponent<CtrlFieldItem> ();
								GameMain.ParkRoot.AddFieldItem (script);
							}

							int iDummyItemId = 0;
							if (x == DataManager.Instance.user.m_iWidth + DefineOld.EXPAND_FIELD|| y == DataManager.Instance.user.m_iHeight+ DefineOld.EXPAND_FIELD) {
								iDummyItemId = -1;
							}
							script.Init (x, y, iDummyItemId);

						}
					}
				}
				DataManager.Instance.user.m_iWidth += DefineOld.EXPAND_FIELD;
				DataManager.Instance.user.m_iHeight += DefineOld.EXPAND_FIELD;
				PlayerPrefs.SetInt (DefineOld.USER_WIDTH, DataManager.Instance.user.m_iWidth);
				PlayerPrefs.SetInt (DefineOld.USER_HEIGHT, DataManager.Instance.user.m_iHeight);
				PlayerPrefs.Save ();
			}
			if (m_ojisanCheck.IsYes ()) {
				GameMain.ListRefresh = true;
				Destroy (m_ojisanCheck.gameObject);
				m_eStep = STEP.IDLE;
			}
			break;

		case STEP.TICKET_CHECK:
			/*
			if (bInit) {
				string strBuyProductId = DefineOld.GetProductId (m_ItemMaster.item_id , ref m_iTicketNum );
				PurchasesManager.buyItem (strBuyProductId);
			}
			if (PurchasesManager.Instance.IsPurchased ()) {
				m_eStep = STEP.IDLE;
				if (PurchasesManager.Instance.Status == PurchasesManager.STATUS.SUCCESS) {
					m_eStep = STEP.TICKET_BUY;
				}
			}
			*/
			if (bInit) {
				DefineOld.GetProductId (m_ItemMaster.item_id , ref m_iTicketNum );
				GameObject objOjisan = PrefabManager.Instance.MakeObject ("prefab/PrefOjisanCheck", gameObject.transform.parent.parent.parent.parent.gameObject );
				m_ojisanCheck = objOjisan.GetComponent<CtrlOjisanCheck> ();
				//m_ojisanCheck.Initialize ( string.Format("ゴールドををチケットに\n変換します\nよろしいですか"  ));
				m_ojisanCheck.Initialize ( string.Format("ゴールドををチケットに\n変換します\n\n{0}枚→ {1}枚\nよろしいですか" , DataManager.Instance.user.m_iTicket ,DataManager.Instance.user.m_iTicket+m_iTicketNum ));
			}
			if (m_ojisanCheck.IsYes ()) {
				SoundManager.Instance.PlaySE (SoundName.BUTTON_PUSH, "https://s3-ap-northeast-1.amazonaws.com/every-studio/app/sound/se");
				Destroy (m_ojisanCheck.gameObject);
				m_eStep = STEP.TICKET_BUY;
			} else if (m_ojisanCheck.IsNo ()) {
				SoundManager.Instance.PlaySE (SoundName.BUTTON_PUSH, "https://s3-ap-northeast-1.amazonaws.com/every-studio/app/sound/se");
				Destroy (m_ojisanCheck.gameObject);
				m_eStep = STEP.IDLE;
			} else {
			}
			break;

		case STEP.TICKET_BUY:
			Debug.Log (string.Format ("add ticket num:{0}" , m_iTicketNum ));
			DataManager.Instance.user.AddGold (-1*m_ItemMaster.need_coin);
			DataManager.Instance.user.AddTicket (m_iTicketNum);
			GameMain.Instance.HeaderRefresh ();
			m_eStep = STEP.IDLE;
			break;

		case STEP.GOLD_CHECK:
			if (bInit) {
				GameObject objOjisan = PrefabManager.Instance.MakeObject ("prefab/PrefOjisanCheck", gameObject.transform.parent.parent.parent.parent.gameObject );
				m_ojisanCheck = objOjisan.GetComponent<CtrlOjisanCheck> ();
				m_ojisanCheck.Initialize ( string.Format("チケットをゴールドに\n変換します\n\n{0}G→ {1}G\nよろしいですか" , DataManager.Instance.user.m_iGold ,DataManager.Instance.user.m_iGold+m_ItemMaster.add_coin ));
			}
			if (m_ojisanCheck.IsYes ()) {
				SoundManager.Instance.PlaySE (SoundName.BUTTON_PUSH, "https://s3-ap-northeast-1.amazonaws.com/every-studio/app/sound/se");
				Destroy (m_ojisanCheck.gameObject);
				m_eStep = STEP.GOLD_BUY;
			} else if (m_ojisanCheck.IsNo ()) {
				SoundManager.Instance.PlaySE (SoundName.BUTTON_PUSH, "https://s3-ap-northeast-1.amazonaws.com/every-studio/app/sound/se");
				Destroy (m_ojisanCheck.gameObject);
				m_eStep = STEP.IDLE;
			} else {
			}
			break;

		case STEP.GOLD_BUY:
			DataManager.Instance.user.AddTicket (-1 * m_ItemMaster.need_ticket);
			DataManager.Instance.user.AddGold (m_ItemMaster.add_coin);
			GameMain.Instance.HeaderRefresh ();
			m_eStep = STEP.IDLE;
			break;

		default:
			break;
		}
		return;
	}

}















