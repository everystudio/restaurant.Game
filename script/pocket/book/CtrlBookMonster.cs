using UnityEngine;
using System.Collections;

public class CtrlBookMonster : CtrlMonsterDetail {

	public UILabel m_lbNumber;
	public UILabel m_lbExplain;

	new public void Initialize( int _iMonsterId ){
		m_eStep = STEP.IDLE;
		CsvMonsterParam master_data = DataManager.GetMonster (_iMonsterId);

		m_lbNumber.text = string.Format ("No.{0}", master_data.monster_id);

		m_lbName.text = master_data.name;

		m_lbUriage.text = UtilString.GetSyuunyuu (master_data.revenew_coin, master_data.revenew_interval);
		//m_lbUriage.text = master_data.revenew_coin.ToString() + " / " + master_data.revenew_interval.ToString();
		m_lbExp.text = master_data.revenew_exp.ToString();
		m_lbCost.text = master_data.cost.ToString();

		m_lbExplain.text = master_data.description_book;

		string strRarity = "";
		for (int i = 0; i < master_data.rare; i++) {
			strRarity += "★";
		}
		m_lbRarity.text = strRarity;//master_data.rare.ToString();

		string strSpriteName = GetSpriteName (master_data.monster_id);
		//UIAtlas atlas = AtlasManager.Instance.GetAtlas (strSpriteName);
		//m_sprMonster.atlas = atlas;
		m_sprMonster.sprite2D = SpriteManager.Instance.Load( strSpriteName );

		m_sprMonster.width= (int)m_sprMonster.sprite2D.textureRect.width;
		m_sprMonster.height = (int)m_sprMonster.sprite2D.textureRect.height;

	}

}
