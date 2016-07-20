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
			break;
		case DataManager.USER_PARAM.TICKET:
			m_imgIcon.sprite = SpriteManager.Instance.Load ("texture/ui/icon_ticket");
			break;
		default:
			Debug.LogError (_eUserParam);
			break;
		}

		UpdateParam (DataManager.user.m_iCoin);
		DataManager.user.UpdateCoin.AddListener (UpdateParam);

	}
	
}
