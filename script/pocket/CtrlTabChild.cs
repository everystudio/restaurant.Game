using UnityEngine;
using System.Collections;

public class CtrlTabChild : ButtonBase {

	#region SerializeField設定
	[SerializeField]
	private UI2DSprite m_sprImage;
	#endregion

	public Tab m_tTab;

	public void Init( Tab _tab ){
		m_tTab = _tab;

		// 設定書くのがめんどくなったので一回全部つける
		Switch (true);
	}

	public Tab.TYPE GetTabType(){
		return m_tTab.m_eType;
	}

	public void Switch( bool _bFlag ){
		string strSpriteName = m_tTab.m_strButtonHeader;
		if (_bFlag) {
			strSpriteName += "_on";
		} else {
			strSpriteName += "_off";
		}
		strSpriteName = string.Format ("texture/ui/{0}.png", strSpriteName);
		m_sprImage.sprite2D = SpriteManager.Instance.Load (strSpriteName);
		//m_uiButton.normalSprite = strSpriteName;
		return;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
