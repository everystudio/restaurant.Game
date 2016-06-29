using UnityEngine;
using System.Collections;

public class CtrlDispMonsterDetailStaffSub : MonoBehaviour {

	public void Initialize( string _strStaff ){
		m_sprChara.sprite2D = SpriteManager.Instance.Load (string.Format ("texture/staff/{0}.png", _strStaff));

		m_sprChara.width =  (int)m_sprChara.sprite2D.rect.width;
		m_sprChara.height = (int)m_sprChara.sprite2D.rect.height;

		/*
		m_sprChara.width = sprite_data.width;
		m_sprChara.height = sprite_data.height;
		*/

		m_lbNo.enabled = false;
	}

	#region SerializeField
	[SerializeField]
	private UI2DSprite m_sprChara;
	[SerializeField]
	private UILabel m_lbNo;			// NOしかないんだろうけど
	#endregion


}
