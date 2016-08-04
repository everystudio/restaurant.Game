using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StaffParam : MonoBehaviour {

	public enum TYPE
	{
		NONE		=0,
		MANNER		,
		FOOTWORK	,
		COOK		,
		MAX			,
	}

	[SerializeField]
	private Image m_imgIcon;

	[SerializeField]
	private Text m_textParam;

	public void Set( StaffParam.TYPE _eType , DataStaffParam _param ){

		m_imgIcon.sprite = SpriteManager.Instance.LoadSprite (string.Format ("texture/staff/staff_paramicon_{0:D2}", (int)_eType));

		string strParam = "0";
		switch (_eType) {
		case TYPE.MANNER:
			strParam = _param.manner.ToString();
			break;
		case TYPE.FOOTWORK:
			strParam = _param.footwork.ToString ();
			break;
		case TYPE.COOK:
			strParam = _param.cook.ToString ();
			break;
		}
		m_textParam.text = strParam;
	}


}



