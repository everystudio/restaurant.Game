﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BannerWork : BannerBase {

	public enum STEP
	{
		NONE				= 0,
		IDLE				,
		MONSTER_BUY_CHECK	,
		MONSTER_BUY			,

		ITEM_HINT			,
		ITEM_MOVE			,

		STAFF_MOVE			,
		MAX					,
	}
	public STEP m_eStep;
	public STEP m_eStepPre;

	[SerializeField]
	private GameObject m_goFinished;

	public CtrlOjisanCheck m_ojisanCheck;

	private ButtonBase m_buttonBase;// = GetComponent<ButtonBase> ();
	private DataWorkParam m_dataWork;

	[SerializeField]
	private UI2DSprite m_sprNew;

	// please check BannerBase

	public void Initialize( DataWorkParam _data ){
		m_buttonBase = GetComponent<ButtonBase> ();
		//Debug.Log (m_buttonBase);
		m_eStep = STEP.IDLE;
		m_eStepPre = STEP.MAX;

		// 一旦消しておく
		m_sprNew.enabled = false;

		string strHeader = "";
		if( PlayerPrefs.HasKey( DefineOld.GetWorkNewKey( _data.work_id ) )){
			if (PlayerPrefs.GetInt (DefineOld.GetWorkNewKey (_data.work_id) )== 0) {
			} else {
				strHeader = string.Format ("[FF0000]NEW[-]");
				m_sprNew.enabled = true;
				PlayerPrefs.SetInt (DefineOld.GetWorkNewKey (_data.work_id), 0);
			}
		}
		// Newはテキストから画像に変更
		strHeader = "";
		m_lbTitle.text = string.Format ("{0}{1}", strHeader, _data.title);
		m_lbTitle2.text = m_lbTitle.text;

		m_lbDescription.text = _data.description;
		if (0 < _data.prize_ticket) {
			m_lbPrize.text = "チケット" + _data.prize_ticket.ToString () + "枚";
		} else {
			m_lbPrize.text = _data.prize_coin.ToString () + "G";
		}
		m_lbPrizeExp.text = _data.exp.ToString();
		m_lbDifficulty.text = _data.difficulty;

		if (_data.status == (int)DefineOld.Work.STATUS.CLEARD) {
			m_goFinished.SetActive (true);
		} else {
			m_goFinished.SetActive (false);
		}

		m_dataWork = _data;
		string strIcon = GetSpriteName (_data);
		//UIAtlas atlas = AtlasManager.Instance.GetAtlas (strIcon);
		//m_sprIcon.atlas = atlas;
		m_sprIcon.sprite2D = SpriteManager.Instance.Load( strIcon );
		SpriteIconAdjust (m_sprIcon);



		return;
	}

	public static string GetSpriteName( DataWorkParam _data ){
		return GetSpriteName (_data.work_id);
	}

	public static string GetSpriteName ( int _iWorkId ){
		DataWorkParam work_data = DataManager.GetWork (_iWorkId);
		string strRet = "";
		if (0 < work_data.mission_staff) {
			strRet = "staff_icon" + work_data.mission_staff.ToString ();
			strRet = string.Format ("texture/staff/{0}.png", strRet);

		} else if (0 < work_data.mission_item) {
			strRet = "item" + work_data.mission_item.ToString ();
			strRet = string.Format ("texture/item/item{0:D2}_01.png",work_data.mission_item);
		} else if (0 < work_data.mission_level) {
			strRet = "icon_lv";
			strRet = string.Format ("texture/ui/icon_lv.png");
		} else if (0 < work_data.mission_monster) {
			strRet = string.Format( "chara{0:D2}", work_data.mission_monster );
			strRet = string.Format ("texture/monster/{0}.png" , strRet );
		} else if (0 < work_data.mission_collect) {
			strRet = "icon_gold";
			strRet = string.Format ("texture/ui/icon_gold.png");
		} else {
			Debug.LogError ("none work icon");
			strRet = "";
		}
		return strRet;

	}


	// Update is called once per frame
	void Update () {
		bool bInit = false;
		if (m_eStepPre != m_eStep) {
			m_eStepPre  = m_eStep;
			bInit = true;
		}

		switch (m_eStep) {
		case STEP.IDLE:
			if (bInit) {
				m_buttonBase.TriggerClear ();
			}

			if (m_buttonBase.ButtonPushed) {
				m_buttonBase.TriggerClear ();
				if (m_dataWork.status == 1) {
					if (0 < m_dataWork.mission_monster) {
						m_eStep = STEP.MONSTER_BUY_CHECK;
					} else if (0 < m_dataWork.mission_item) {

						CsvItemParam item_master = DataManager.Instance.m_csvItem.Select (m_dataWork.mission_item);

						if (item_master.status == (int)DefineOld.Item.Status.NONE) {
							m_eStep = STEP.ITEM_HINT;
						} else {
							m_eStep = STEP.ITEM_MOVE;
						}

					}else if(0 < m_dataWork.mission_staff){
						m_eStep = STEP.STAFF_MOVE;
					} else {
					}
				}
			}
			break;
		case STEP.MONSTER_BUY_CHECK:
			if (bInit) {
				GameObject objOjisan = PrefabManager.Instance.MakeObject ("prefab/PrefOjisanCheck", gameObject.transform.parent.parent.parent.parent.gameObject);
				m_ojisanCheck = objOjisan.GetComponent<CtrlOjisanCheck> ();

				CsvMonsterParam monster_data = DataManager.GetMonster (m_dataWork.mission_monster);

				string strDisp = "";
				if ( monster_data.coin <= DataManager.Instance.user.m_iGold ) {
					strDisp = string.Format("本当に購入しますか?\n({0}G)\n\n{1}G→[FFD900]{2}G[-]" , monster_data.coin , DataManager.Instance.user.m_iGold , DataManager.Instance.user.m_iGold - monster_data.coin );
					//strDisp = string.Format("こちらの動物を\n購入しますか({0}G)\n\n{1}G→{2}G" , monster_data.coin , DataManager.Instance.user.m_iGold , DataManager.Instance.user.m_iGold - monster_data.coin );
				} else {
					strDisp = string.Format("こちらの動物を\n購入しますか\n\n購入には[FF0000]{0}G[-]必要です\n[FF0000]{1}G[-]不足しています" , monster_data.coin,monster_data.coin - DataManager.Instance.user.m_iGold);
					m_ojisanCheck.YesOrNo.EnableButtonYes (false);
				}

				m_ojisanCheck.Initialize (strDisp);
			}
			if (m_ojisanCheck.IsYes ()) {
				Destroy (m_ojisanCheck.gameObject);
				SoundManager.Instance.PlaySE (SoundName.BUTTON_PUSH, "https://s3-ap-northeast-1.amazonaws.com/every-studio/app/sound/se");
				m_eStep = STEP.MONSTER_BUY;
			} else if (m_ojisanCheck.IsNo ()) {
				Destroy (m_ojisanCheck.gameObject);
				SoundManager.Instance.PlaySE (SoundName.BUTTON_PUSH, "https://s3-ap-northeast-1.amazonaws.com/every-studio/app/sound/se");
				m_eStep = STEP.IDLE;
			} else {
			}

			break;

		case STEP.MONSTER_BUY:
			if (bInit) {
				GameObject objOjisan = PrefabManager.Instance.MakeObject ("prefab/PrefOjisanCheck", gameObject.transform.parent.parent.parent.parent.gameObject);
				m_ojisanCheck = objOjisan.GetComponent<CtrlOjisanCheck> ();
				string strDisp = "購入完了しました\n檻から設置を\n行ってください\n";
				m_ojisanCheck.Initialize (strDisp, true);

				/*				
				Dictionary< string , string > dict = new Dictionary< string , string > ();
				int iConditionFine = (int)DefineOld.Monster.CONDITION.FINE;
				dict.Add( "condition" , iConditionFine.ToString() ); 
				*/
				DataManager.Instance.dataMonster.Insert (m_dataWork.mission_monster, 0);
				CsvMonsterParam monster_data = DataManager.GetMonster (m_dataWork.mission_monster);

				DataManager.Instance.user.AddGold (monster_data.coin * -1);
				DataWork.WorkCheck ();
			}
			if (m_ojisanCheck.IsYes ()) {
				Destroy (m_ojisanCheck.gameObject);
				m_eStep = STEP.IDLE;
				GameMain.ListRefresh = true;
			}
			break;

		case STEP.ITEM_HINT:
			if (bInit) {
				GameObject objOjisan = PrefabManager.Instance.MakeObject ("prefab/PrefOjisanCheck", gameObject.transform.parent.parent.parent.parent.gameObject);
				m_ojisanCheck = objOjisan.GetComponent<CtrlOjisanCheck> ();

				CsvItemParam item_mission = DataManager.Instance.m_csvItem.Select (m_dataWork.mission_item);
				CsvItemParam item_open = DataManager.Instance.m_csvItem.Select (item_mission.open_item_id);

				string strDisp = string.Format("HINT!\n\n{0}は\n[FF0000]{1}[-]を購入すると\n購入可能になります" , item_mission.name , item_open.name );
				m_ojisanCheck.Initialize (strDisp, true);
			}
			if (m_ojisanCheck.IsYes ()) {
				Destroy (m_ojisanCheck.gameObject);
				m_eStep = STEP.IDLE;
			}
			break;

		case STEP.ITEM_MOVE:
			if (bInit) {
				GameObject objOjisan = PrefabManager.Instance.MakeObject ("prefab/PrefOjisanCheck", gameObject.transform.parent.parent.parent.parent.gameObject);
				m_ojisanCheck = objOjisan.GetComponent<CtrlOjisanCheck> ();


				string strDisp = string.Format ("購入ページに移動します\nよろしいですか？");
				m_ojisanCheck.Initialize (strDisp);
			}
			if (m_ojisanCheck.IsYes ()) {
				Destroy (m_ojisanCheck.gameObject);

				CsvItemParam item_mission = DataManager.Instance.m_csvItem.Select (m_dataWork.mission_item);
				switch (item_mission.category) {

				case 1:
				default:
					GameMain.Instance.m_iMoveTab = 0;
					break;
				case 2:
					GameMain.Instance.m_iMoveTab = 1;
					break;
				case 3:
					GameMain.Instance.m_iMoveTab = 2;
					break;
				}

				GameMain.Instance.SetStatus (GameMain.STATUS.ITEM);
				//m_eStep = STEP.IDLE;
			} else if (m_ojisanCheck.IsNo ()) {
				Destroy (m_ojisanCheck.gameObject);
				m_eStep = STEP.IDLE;
			} else {
			}
			break;
		case STEP.STAFF_MOVE:
			if (bInit) {
				GameObject objOjisan = PrefabManager.Instance.MakeObject ("prefab/PrefOjisanCheck", gameObject.transform.parent.parent.parent.parent.gameObject);
				m_ojisanCheck = objOjisan.GetComponent<CtrlOjisanCheck> ();

				//string strDisp = string.Format("HINT!\n\nスタッフを雇う場合は\n檻を選択してから\n配置を行うことができます"  );
				string strDisp = string.Format("\n\nHINT!\n\nオフィス購入後、各施設の「配置」タブからスタッフを雇えるよ！\n\n" );

				m_ojisanCheck.Initialize (strDisp, true);
			}
			if (m_ojisanCheck.IsYes ()) {
				Destroy (m_ojisanCheck.gameObject);
				m_eStep = STEP.IDLE;
			}
			break;
		case STEP.MAX:
		default:
			break;
		}

	
	}
}



























