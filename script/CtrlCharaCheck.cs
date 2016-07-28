using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CtrlCharaCheck : MonoBehaviour {

	#region SerializeField
	[SerializeField]
	private Text m_textMessage;
	[SerializeField]
	private Button m_btnYes;
	public Button btnYes{
		get{ return m_btnYes; }
	}
	[SerializeField]
	private Button m_btnNo;
	public Button btnNo{
		get{ return m_btnNo; }
	}

	#endregion

	public void SelfDestroy(){
		Destroy (gameObject);
	}

	virtual public void Initialize(string _strMessage , bool _bIsYesOnly = false ){
		m_textMessage.text = _strMessage;

	}

}
