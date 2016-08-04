using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
public class BtnShopTab : MonoBehaviour {

	public UnityEventString OnClick = new UnityEventString ();

	[SerializeField]
	private string m_strTabImage;

	[SerializeField]
	private Image m_imgButton;
	[SerializeField]
	private string m_strCategory;
	[SerializeField]
	private string m_strType;

	private Button m_btn;

	void Awake(){

		m_btn = gameObject.GetComponent<Button> ();
		m_btn.onClick.AddListener (pushedButton);
	}

	public void ChangePushed( string _strCategory , string _strType ){
		string strSprite = "";
		if (_strCategory.Equals (m_strCategory) && _strType.Equals (m_strType)) {
			strSprite = string.Format ("texture/ui/{0}_on", m_strTabImage);
		} else {
			strSprite = string.Format ("texture/ui/{0}_off", m_strTabImage);
		}
		m_imgButton.sprite = SpriteManager.Instance.LoadSprite (strSprite);
	}

	private void pushedButton (){
		string strTemp = string.Format ("{0},{1}", m_strCategory, m_strType);
		OnClick.Invoke (strTemp);
	}



}




