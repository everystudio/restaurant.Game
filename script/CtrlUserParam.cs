using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CtrlUserParam : MonoBehaviour {

	[SerializeField]
	private Text m_textParam;
	[SerializeField]
	private Image m_imgIcon;

	public void SetNum( DataManager.USER_PARAM _eUserParam , int _iNum ){
		SetIcon (_eUserParam);
		UpdateParam (_iNum);
	}

	private void SetIcon (DataManager.USER_PARAM _eUserParam){
		switch (_eUserParam) {
		case DataManager.USER_PARAM.COIN:
			m_imgIcon.sprite = SpriteManager.Instance.Load ("texture/ui/icon_coin");
			break;
		case DataManager.USER_PARAM.TICKET:
			m_imgIcon.sprite = SpriteManager.Instance.Load ("texture/ui/icon_ticket");
			break;
		case DataManager.USER_PARAM.POPULARITY:
			m_imgIcon.sprite = SpriteManager.Instance.Load ("texture/ui/icon_popularity");
			break;
		default:
			Debug.LogError (_eUserParam);
			break;
		}
		return;
	}


	private void UpdateParam( int _iParam ){
		m_textParam.text = _iParam.ToString ();
	}

	public void Initialize( DataManager.USER_PARAM _eUserParam ){

		SetIcon (_eUserParam);

		switch (_eUserParam) {
		case DataManager.USER_PARAM.COIN:
			UpdateParam (DataManager.Instance.user.coin);
			DataManager.Instance.user.UpdateCoin.AddListener (UpdateParam);
			break;
		case DataManager.USER_PARAM.TICKET:
			UpdateParam (DataManager.Instance.user.ticket);
			DataManager.Instance.user.UpdateTicket.AddListener (UpdateParam);
			break;
		case DataManager.USER_PARAM.POPULARITY:
			UpdateParam (DataManager.Instance.user.popularity);
			DataManager.Instance.user.UpdatePopularity.AddListener (UpdateParam);
			break;
		default:
			Debug.LogError (_eUserParam);
			break;
		}


	}
	
}
