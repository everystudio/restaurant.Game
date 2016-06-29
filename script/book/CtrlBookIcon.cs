using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CtrlBookIcon : ButtonBase {

	#region SerializeFieldとか
	[SerializeField]
	private UILabel m_lbNumber;
	[SerializeField]
	private UILabel m_lbName;
	[SerializeField]
	private UI2DSprite m_sprMonster;
	#endregion

	public CsvMonsterParam m_monsterData;

	private CtrlBookMonster m_ctrlBookMonster;

	public bool Initialize( CsvMonsterParam _data , GameObject _posDisplay ){
		bool bRet = true;
		m_monsterData = _data;

		m_lbNumber.text = "NO." + m_monsterData.monster_id.ToString ();
		m_lbName.text = m_monsterData.name;

		//m_sprMonster.atlas = AtlasManager.Instance.GetAtlas (strMonster);
		m_sprMonster.sprite2D = SpriteManager.Instance.Load( string.Format( "texture/monster/chara{0:D2}.png", m_monsterData.monster_id));

		List<DataMonsterParam> datamonster_list = DataManager.Instance.dataMonster.Select (string.Format (" monster_id = {0}", m_monsterData.monster_id));

		if (datamonster_list.Count == 0) {
			bRet = false;
			m_sprMonster.color = Color.black;
			m_lbName.text = "？？？？？？";
		}
		m_sprMonster.width = (int)m_sprMonster.sprite2D.textureRect.width;
		m_sprMonster.height = (int)m_sprMonster.sprite2D.textureRect.height;
		return bRet;
	}
	public bool Initialize( int _iMonsterId , GameObject _posDisplay ){
		return Initialize (DataManager.Instance.m_csvMonster.Select (_iMonsterId), _posDisplay);
	}

	public int GetMonsterId(){
		return m_monsterData.monster_id;
	}


	// Update is called once per frame
	void Update () {

		/*
		if (ButtonPushed && m_ctrlBookMonster == null ) {

			TriggerClear ();

			GameObject obj = PrefabManager.Instance.MakeObject ("prefab/PrefBookMonster", m_posDisplay);

			m_ctrlBookMonster = obj.GetComponent<CtrlBookMonster> ();
			m_ctrlBookMonster.Initialize (m_monsterData.monster_id);
		}
		*/


	
	}
}






















