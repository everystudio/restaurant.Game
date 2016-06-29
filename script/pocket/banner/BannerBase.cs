using UnityEngine;
using System.Collections;

/// <summary>
/// Banner base.
/// このベースクラスはかなりださい形になっています
/// おそらくテンプレートを使えばもう少しましな形になりそうなきもしますけど
/// そこまで手が回らず
/// </summary>

public class BannerBase : MonoBehaviour {
	#region SerializeField設定
	[SerializeField]
	protected UILabel m_lbTitle;
	[SerializeField]
	protected UILabel m_lbTitle2;
	[SerializeField]
	protected UILabel m_lbDescription;
	[SerializeField]
	protected UILabel m_lbPrize;		// gold or ticket
	[SerializeField]
	protected UILabel m_lbPrizeExp;
	[SerializeField]
	protected UILabel m_lbDifficulty;
	[SerializeField]
	protected UI2DSprite m_sprIcon;			// これはアトラスも変更しないとダメ
	[SerializeField]
	protected UI2DSprite m_sprBackground;		// 背景
	[SerializeField]
	protected UI2DSprite m_sprIgnoreBlack;
	[SerializeField]
	protected UILabel m_lbReason;
	[SerializeField]
	protected UI2DSprite m_sprReason;
	#endregion

	public enum MODE
	{
		NONE			= 0,

		MONSTER_DETAIL	,
		MONSTER_SET_BUY		,
		MONSTER_SET_MINE	,
		MONSTER_SICK		,

		STAFF_BACKYARD_CHECK	,	// バックヤードに待機させる
		STAFF_SET_BUY			,	// 購入して配置する
		STAFF_SET_MINE			,	// バックヤードから配置する

		ITEM_BACKYARD		,
		ITEM_BUY			,

		MAX				,
	}
	static private MODE m_eMode;
	static public MODE Mode{
		get{ return m_eMode; }
		set{ m_eMode = value; }
	}

	protected bool m_bIsEnd;
	public bool IsEnd(){
		return m_bIsEnd;
	}

	public enum ABLE_BUY_REASON
	{
		NONE	= 0,
		OKEY	,
		GOLD	,
		TICKET	,
		COST	,
		LIMIT	,
		MAX		,
	}
	public ABLE_BUY_REASON m_eReason;

	protected void SetEnableIcon( bool _bEnable ){
		if (_bEnable) {
			m_sprIcon.alpha = 1.0f;
		}else {
			m_sprIcon.alpha = 0.4f;
		}
		return;
	}

	protected void SpriteIconAdjust( UI2DSprite _sprite ){
		_sprite.width = (int)_sprite.sprite2D.textureRect.width;
		_sprite.height = (int)_sprite.sprite2D.textureRect.height;
		float set_size = 120.0f;

		if (_sprite.width < _sprite.height) {
			float rate = set_size / (float)_sprite.height;
			_sprite.width = (int)(_sprite.width * rate);
			_sprite.height = (int)set_size;
		} else {
			float rate = set_size / (float)_sprite.width;
			_sprite.width = (int)set_size;
			_sprite.height = (int)(_sprite.height * rate);
		}


		return;
	}

	public string SetReasonSprite( UI2DSprite _sprite , ABLE_BUY_REASON _eReason ){
		string strRet = "";
		switch (_eReason) {
		case ABLE_BUY_REASON.COST:
			strRet = "no_cost";
			break;
		case ABLE_BUY_REASON.GOLD:
			strRet = "no_gold";
			break;
		case ABLE_BUY_REASON.TICKET:
			strRet = "no_ticket";
			break;
		case ABLE_BUY_REASON.LIMIT:
			strRet = "これ以上購入できません";
			break;
		default:
			strRet = "";
			_sprite.enabled = false;
			break;
		}

		if (strRet.Equals ("") == false) {
			_sprite.sprite2D = SpriteManager.Instance.Load (string.Format ("texture/ui/{0}.png", strRet));
		}

		return strRet;
	}

	public string SetReasonString( UILabel _lbReason , ABLE_BUY_REASON _eReason ){
		string strRet = "";
		switch (_eReason) {
		case ABLE_BUY_REASON.COST:
			strRet = "コスト不足";
			break;
		case ABLE_BUY_REASON.GOLD:
			strRet = "ゴールド不足";
			break;
		case ABLE_BUY_REASON.TICKET:
			strRet = "チケット不足";
			break;
		case ABLE_BUY_REASON.LIMIT:
			strRet = "これ以上購入できません";
			break;
		default:
			strRet = "";
			break;
		}
		_lbReason.text = strRet;
		return strRet;
	}

	protected bool m_bIsUserData;
	protected bool m_bAbleUse;

	protected bool m_bRequestChangeTab;
	protected int m_iRequestTabIndex;
	public bool RequestTabIndex( ref int _iIndex ){
		_iIndex = m_iRequestTabIndex;
		return m_bRequestChangeTab;
	}

	// たぶんprotectedでいい
	public CtrlTabParent m_tabParent;
	public void SetTabParent( CtrlTabParent _tabParent ){
		m_tabParent = _tabParent;
	}

}











