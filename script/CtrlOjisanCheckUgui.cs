using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CtrlOjisanCheckUgui : MonoBehaviour {

	#region SerializeField
	[SerializeField]
	private Text m_textMessage;
	public ButtonBase m_btnYes;
	public ButtonBase m_btnNo;

	#endregion

	virtual public void Initialize(string _strMessage , bool _bIsYesOnly = false ){
		m_textMessage.text = _strMessage;

		m_btnYes.ButtonInit ();
		m_btnNo.ButtonInit ();
	}

}
