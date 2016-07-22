using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CtrlUserParam : MonoBehaviour {

	[SerializeField]
	private Text m_textParam;
	[SerializeField]
	private Image m_imgIcon;

	private void UpdateParam( int _iParam ){
		m_textParam.text = _iParam.ToString ();
	}

	public void Initialize( DataManager.USER_PARAM _eUserParam ){

		switch (_eUserParam) {
		case DataManager.USER_PARAM.COIN:
			m_imgIcon.sprite = SpriteManager.Instance.Load ("texture/ui/icon_coin");
			UpdateParam (DataManager.Instance.user.m_iCoin);
			DataManager.Instance.user.UpdateCoin.AddListener (UpdateParam);
			break;
		case DataManager.USER_PARAM.TICKET:
			m_imgIcon.sprite = SpriteManager.Instance.Load ("texture/ui/icon_ticket");
			UpdateParam (DataManager.Instance.user.m_iTicket);
			DataManager.Instance.user.UpdateTicket.AddListener (UpdateParam);
			break;
		case DataManager.USER_PARAM.POPULARITY:
			m_imgIcon.sprite = SpriteManager.Instance.Load ("texture/ui/icon_popularity");
			UpdateParam (DataManager.Instance.user.m_iPopularity);
			DataManager.Instance.user.UpdatePopularity.AddListener (UpdateParam);
			break;
		default:
			Debug.LogError (_eUserParam);
			break;
		}


	}
	
}
