using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;
using EveryStudioLibrary;
using Prime31;

public class InitialMain : MonoBehaviour {

	static public bool INITIALIZE_MAIN = false;
	public bool CONFIG_UPDATE = false;
	public enum STEP
	{
		NONE					= 0,

		CHECK_CONFIG			,
		CHECK_UPDATE			,
		UPDATE_DOWNLOAD			,
		DATA_DOWNLOAD			,
		UPDATE_ITEM_DATA		,
		UPDATE_MONSTER_DATA		,
		UPDATE_WORK_DATA		,

		DATAMANAGER_SETUP		,
		SOUND_LOAD				,
		ATTENTION_DISP_VISITOR	,
		REVIEW					,

		IDLE					,
		DB_SETUP				,
		INPUT_WAIT				,

		DB_BACKUP_NOEXIST		,
		DB_BACKUP_CHECK			,
		DB_BACKUP				,
		END						,
		NETWORK_ERROR			,
		MAX						,
	}
	public STEP m_eStep;
	public STEP m_eStepPre;

	public GameObject m_goRoot;
	public GameObject m_goStartButton;
	public ButtonBase m_btnStart;
	public ButtonBase m_btnBackup;
	public CtrlOjisanCheck m_ojisanCheck;
	public UITexture m_texBack;
	public UtilSwitchSprite m_SwitchSpriteBack;
	public List<SpreadSheetData> m_ssdSample;

	public ButtonBase m_btnTutorialReset;
	public ButtonBase m_btnCacheClear;
	public ButtonBase m_btnNetworkError;

	public CtrlReviewWindow m_reviewWindow;
	private int m_iNetworkSerial;

	public CtrlLoading m_csLoading;
	[SerializeField]
	private GameObject m_posDisplay;
	#region DB関係
	DataKvs m_dbKvs{
		get{ 
			return DataManager.Instance.kvs_data;
		}
	}
	DataWork m_dbWork {
		get{
			return DataManager.Instance.dataWork;
		}
	}
	DBItem m_dbItem;
	CsvMonster m_dbMonsterMaster{
		get{
			return DataManager.Instance.m_csvMonster;
		}
	}
	#endregion

	[SerializeField]
	private ButtonBase m_btnVisitorDisp;
	[SerializeField]
	private UILabel m_lbVisitorDisp;

	// Use this for initialization
	void Start () {
		INITIALIZE_MAIN = true;
		Application.targetFrameRate = 60;
		QualitySettings.vSyncCount = 0;

		m_eStep = STEP.IDLE;
		m_eStep = STEP.CHECK_CONFIG;
		m_eStepPre = STEP.MAX;

		//m_SwitchSpriteBack.SetSprite ("tutorial777");

		//SoundManager.Instance.PlayBGM ("farming" , "https://s3-ap-northeast-1.amazonaws.com/every-studio/app/sound/bgm");
		SoundManager.Instance.PlayBGM ( "maoudamashii_5_village01" , "https://s3-ap-northeast-1.amazonaws.com/every-studio/app/sound/bgm/maou");
		#if UNITY_ANDROID
		/*
		GoogleIAB.enableLogging (true);
		string key = "your public key from the Android developer portal here";
		key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAsqFXrg2t62dru/VFQYyxd2m1kORBbrAGxDxiSAkh3ybaXJtJWNcej/YAxKx7Orrtfq+pU965U2FnU3K54xddts2UGCI9O6TSU0AoKbwFYj+okfF21firsEqZd4aYtVYQ471flWj3ZEG9u2YpIzjGykUQadsxO4Y/OcRbdUn9289Mc0JAbdepmN9yRnvgBJWKZF/c0mBrM4ISfF5TVip2Tp+BXACqblOb+TQZjOB0OeVPxYpdy5k3eJTcQuwiLmYxgpEBL3tIT7grxVROgk8YYncncaZR7Q/wWlsFgFTNMRaF2bPI8apLiA7eIyKv5zbmhbE7YLBXUvkuoHbAqDQrLQIDAQAB";
		key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAoNpjSDejTWrxkCnuj5BQ8ozItBVBS2OhgRga4D2zgG42rKy/9C5nb32NDIl+N9xaVh2eMRDVdR9Hzznp0DIE3Xs89le26pzht5dK4/9s01qsVHmuEtecAcXp6ItCieayYSTn9oMgDwd5LWJMQf8+w5vm1qo6Vlo2vh0Lm70DGqisp3pee+6K+Zb+UfPrcvv9tmo3zCpq9EyiPaitw58nSWJYzDuLHzubUj5qeH5OwcAXi/scEkJrD5dJKmkmUgnDTQ2xSP/UAmtN8qAUULej3iOlQCqVIGlSRqL5kA9Qo9fKUX9PU0hcFz6vnuNj9SN3dk/ocAIvvujFKsQjNHvNHQIDAQAB";

		Debug.Log( key );
		//下はテスト用
		//key = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEArGLKSb92Imt43S40ArCXfTmQ31c+pFQTM0Dza3j/Tn4cqjwccjQ/jej68GgVyGXGC2gT/EtbcVVA+bHugXmyv73lGBgmQlzBL41WYTKolO8Z6pVWTeHBtsT7RcHKukoKiONZ7NiQ9P5t6CCPBB2sXQOp1y3ryVbv01xXlM+hB6HkkKxrT6lIjTbtiVXCHAJvqPexPbqVIfGjBaXH/oHKxEBxYDaa6PTUsU3OP3MTx63ycTEnEMsQlA1W6ZuTFIa5Jd3cVlfQI7uovEzAbIlUfwcnxVOUWASiYe81eQiD1BMl+JeCRhfd5e8D4n0LOA12rHm1F3fC9ZoIEjpNB+BRhwIDAQAB";
		GoogleIAB.init( key );
				*/
		#endif

		SpriteManager.Instance.LoadAtlas ("atlas/ad001");
		SpriteManager.Instance.LoadAtlas ("atlas/back001");
		SpriteManager.Instance.LoadAtlas ("atlas/back002");
		SpriteManager.Instance.LoadAtlas ("atlas/item001");
		SpriteManager.Instance.LoadAtlas ("atlas/item002");
		SpriteManager.Instance.LoadAtlas ("atlas/item003");
		SpriteManager.Instance.LoadAtlas ("atlas/item004");
		SpriteManager.Instance.LoadAtlas ("atlas/item005");
		SpriteManager.Instance.LoadAtlas ("atlas/monster001");
		SpriteManager.Instance.LoadAtlas ("atlas/monster002");
		SpriteManager.Instance.LoadAtlas ("atlas/staff001");
		SpriteManager.Instance.LoadAtlas ("atlas/tutorial001");
		SpriteManager.Instance.LoadAtlas ("atlas/tutorial002");
		SpriteManager.Instance.LoadAtlas ("atlas/tutorial003");
		SpriteManager.Instance.LoadAtlas ("atlas/ui001");
		SpriteManager.Instance.LoadAtlas ("atlas/ui002");
		SpriteManager.Instance.LoadAtlas ("atlas/ui003");
		//m_SwitchSpriteBack.SetSprite ("garalley_003");
		m_SwitchSpriteBack.SetSprite ("texture/back/bg001.png");

		bool bDispVisitor = true;
		if (DataManager.Instance.data_kvs.HasKey (DataManager.Instance.KEY_DISP_VISITOR)) {
			if (DataManager.Instance.data_kvs.ReadInt (DataManager.Instance.KEY_DISP_VISITOR) == 0) {
				bDispVisitor = false;
			}
		}
		if (bDispVisitor == false) {
			m_lbVisitorDisp.text = "非表示";
		}
	}
	
	// Update is called once per frame
	void Update () {

		bool bInit = false;
		if (m_eStepPre != m_eStep) {
			m_eStepPre  = m_eStep;
			bInit = true;
		}

		switch (m_eStep) {

		case STEP.CHECK_CONFIG:
			if (bInit) {
				m_iNetworkSerial = CommonNetwork.Instance.RecieveSpreadSheet (
					DataManager.Instance.SPREAD_SHEET,
					DataManager.Instance.SPREAD_SHEET_CONFIG_SHEET);
			}
			if (m_csLoading != null) {
				m_csLoading.ViewPercent ("ネットワーク接続中", 0.0f);
			}

			if (CommonNetwork.Instance.IsConnected (m_iNetworkSerial)) {
				// 一度終了に向かうように設定
				m_eStep = STEP.DATAMANAGER_SETUP;
				TNetworkData data = EveryStudioLibrary.CommonNetwork.Instance.GetData (m_iNetworkSerial);
				Debug.Log (data.m_strData);
				//Debug.Log (data.m_dictRecievedData);
				m_ssdSample = EveryStudioLibrary.CommonNetwork.Instance.ConvertSpreadSheetData (data.m_dictRecievedData);
				CsvConfig config_data = new CsvConfig ();
				config_data.Input (m_ssdSample);

				// 毎回更新させる
				DataManager.Instance.config.WriteInt(CsvConfig.KEY_CONFIG_VERSION , 0);
				DataManager.Instance.kvs_data.WriteInt (DataManager.Instance.KEY_ITEM_VERSION, 0);
				DataManager.Instance.kvs_data.WriteInt (DataManager.Instance.KEY_MONSTER_VERSION , 0 );
				DataManager.Instance.kvs_data.WriteInt (DataManager.Instance.KEY_WORK_VERSION , 0 );

				if (false == config_data.Read (CsvConfig.KEY_CONFIG_VERSION).Equals (DataManager.Instance.config.Read (CsvConfig.KEY_CONFIG_VERSION)) || CONFIG_UPDATE == true) {
					config_data.Save (CsvConfig.FILE_NAME);
					DataManager.Instance.config.Load (CsvConfig.FILE_NAME);
					m_eStep = STEP.CHECK_UPDATE;



				}
			} else if (CommonNetwork.Instance.IsError (m_iNetworkSerial ) ) {
				m_eStep = STEP.NETWORK_ERROR;
			} else {
			}

			break;


		case STEP.CHECK_UPDATE:
			if (m_csLoading != null) {
				m_csLoading.ViewPercent ("更新データ確認中", 0.0f);
			}

			if (false == DataManager.Instance.config.Read (FileDownloadManager.KEY_DOWNLOAD_VERSION).Equals (DataManager.Instance.kvs_data.Read (FileDownloadManager.KEY_DOWNLOAD_VERSION))) {
				m_eStep = STEP.UPDATE_DOWNLOAD;
			} else if (false == DataManager.Instance.config.Read (DataManager.Instance.KEY_ITEM_VERSION).Equals (DataManager.Instance.kvs_data.Read (DataManager.Instance.KEY_ITEM_VERSION))) {
				m_eStep = STEP.UPDATE_ITEM_DATA;
			} else if (false == DataManager.Instance.config.Read (DataManager.Instance.KEY_MONSTER_VERSION).Equals (DataManager.Instance.kvs_data.Read (DataManager.Instance.KEY_MONSTER_VERSION))) {
				m_eStep = STEP.UPDATE_MONSTER_DATA;
			} else if (false == DataManager.Instance.config.Read (DataManager.Instance.KEY_WORK_VERSION).Equals (DataManager.Instance.kvs_data.Read (DataManager.Instance.KEY_WORK_VERSION))) {
				m_eStep = STEP.UPDATE_WORK_DATA;
			} else {
				m_eStep = STEP.DATAMANAGER_SETUP;
			}
			break;

		case STEP.UPDATE_DOWNLOAD:
			if (bInit) {
				m_iNetworkSerial = CommonNetwork.Instance.RecieveSpreadSheet (
					DataManager.Instance.SPREAD_SHEET ,
					DataManager.Instance.config.Read ("download"));
			}
			if (m_csLoading != null) {
				m_csLoading.ViewPercent ("ダウンロードリスト更新中", 0.0f);
			}

			if (CommonNetwork.Instance.IsConnected (m_iNetworkSerial)) {
				TNetworkData data = EveryStudioLibrary.CommonNetwork.Instance.GetData (m_iNetworkSerial);
				m_ssdSample = EveryStudioLibrary.CommonNetwork.Instance.ConvertSpreadSheetData (data.m_dictRecievedData);
				CsvDownload download_list = new CsvDownload();
				download_list.Input (m_ssdSample);
				download_list.Save (FileDownloadManager.FILENAME_DOWNLOAD_LIST);
				m_eStep = STEP.DATA_DOWNLOAD;
			}
			break;

		case STEP.DATA_DOWNLOAD:
			if (bInit) {
				CsvDownload download_list = new CsvDownload ();
				download_list.Load (FileDownloadManager.FILENAME_DOWNLOAD_LIST);
				Debug.Log (TimeManager.StrGetTime ());
				FileDownloadManager.Instance.Download ( DataManager.Instance.config.ReadInt( FileDownloadManager.KEY_DOWNLOAD_VERSION) , download_list.list);
			}
			if (m_csLoading != null) {
				m_csLoading.ViewPercent ("データダウンロード中", 0.0f);
			}

			if (FileDownloadManager.Instance.IsIdle ()) {
				m_eStep = STEP.CHECK_UPDATE;
				DataManager.Instance.kvs_data.WriteInt (FileDownloadManager.KEY_DOWNLOAD_VERSION, DataManager.Instance.config.ReadInt (FileDownloadManager.KEY_DOWNLOAD_VERSION));
				DataManager.Instance.kvs_data.Save (DataKvs.FILE_NAME);
				DataManager.Instance.AllLoad ();
			}
			break;

		case STEP.UPDATE_ITEM_DATA:
			if (bInit) {
				m_iNetworkSerial = CommonNetwork.Instance.RecieveSpreadSheet (
					DataManager.Instance.SPREAD_SHEET,
					DataManager.Instance.config.Read ("item"));
			}
			if (m_csLoading != null) {
				m_csLoading.ViewPercent ("アイテムデータ更新中", 0.0f);
			}

			if (CommonNetwork.Instance.IsConnected (m_iNetworkSerial)) {
				TNetworkData data = EveryStudioLibrary.CommonNetwork.Instance.GetData (m_iNetworkSerial);
				m_ssdSample = EveryStudioLibrary.CommonNetwork.Instance.ConvertSpreadSheetData (data.m_dictRecievedData);

				if (0 < DataManager.Instance.m_csvItem.list.Count) {
					CsvItem item_master = new CsvItem ();
					item_master.Input (m_ssdSample);
					//item_master.Load ("csv/master/InitialCsvItem");
					foreach (CsvItemParam param in item_master.list) {
						CsvItemParam temp = DataManager.Instance.m_csvItem.Select (param.item_id);
						if (temp.item_id != 0) {
							param.status = temp.status;
						} else {
							item_master.list.Add (param);
						}
					}
					item_master.Save (CsvItem.FilePath);
				}
				DataManager.Instance.kvs_data.WriteInt (DataManager.Instance.KEY_ITEM_VERSION, DataManager.Instance.config.ReadInt (DataManager.Instance.KEY_ITEM_VERSION));
				DataManager.Instance.kvs_data.Save (DataKvs.FILE_NAME);
				DataManager.Instance.AllLoad ();
				m_eStep = STEP.CHECK_UPDATE;
			}
			break;
		case STEP.UPDATE_MONSTER_DATA:
			if (bInit) {
				m_iNetworkSerial = CommonNetwork.Instance.RecieveSpreadSheet (
					DataManager.Instance.SPREAD_SHEET,
					DataManager.Instance.config.Read ("monster"));
			}
			if (m_csLoading != null) {
				m_csLoading.ViewPercent ("キャラデータ更新中", 0.0f);
			}

			if (CommonNetwork.Instance.IsConnected (m_iNetworkSerial)) {
				TNetworkData data = EveryStudioLibrary.CommonNetwork.Instance.GetData (m_iNetworkSerial);
				m_ssdSample = EveryStudioLibrary.CommonNetwork.Instance.ConvertSpreadSheetData (data.m_dictRecievedData);

				if (0 < DataManager.Instance.m_csvMonster.list.Count) {
					CsvMonster monster_master = new CsvMonster ();
					monster_master.Input (m_ssdSample);
					//monster_master.Load ("csv/master/InitialCsvMonster");
					foreach (CsvMonsterParam param in monster_master.list) {
						CsvMonsterParam temp = DataManager.Instance.m_csvMonster.Select (param.monster_id);
						if (temp.monster_id != 0) {
							param.status = temp.status;
						} else {
							monster_master.list.Add (param);
						}
					}
					monster_master.Save (CsvMonster.FilePath);
				}
				DataManager.Instance.kvs_data.WriteInt (DataManager.Instance.KEY_MONSTER_VERSION, DataManager.Instance.config.ReadInt (DataManager.Instance.KEY_MONSTER_VERSION));
				DataManager.Instance.kvs_data.Save (DataKvs.FILE_NAME);
				DataManager.Instance.AllLoad ();
				m_eStep = STEP.CHECK_UPDATE;
			}
			break;

		case STEP.UPDATE_WORK_DATA:
			if (bInit) {
				m_iNetworkSerial = CommonNetwork.Instance.RecieveSpreadSheet (
					DataManager.Instance.SPREAD_SHEET,
					DataManager.Instance.config.Read ("work"));
			}
			if (m_csLoading != null) {
				m_csLoading.ViewPercent ("お仕事データ更新中", 0.0f);
			}

			if (CommonNetwork.Instance.IsConnected (m_iNetworkSerial)) {
				TNetworkData data = EveryStudioLibrary.CommonNetwork.Instance.GetData (m_iNetworkSerial);
				m_ssdSample = EveryStudioLibrary.CommonNetwork.Instance.ConvertSpreadSheetData (data.m_dictRecievedData);

				if (0 < DataManager.Instance.dataWork.list.Count) {
					CsvWork work_master = new CsvWork ();
					work_master.Input (m_ssdSample);
					//work_master.Load ("csv/master/InitialCsvWork");
					foreach (CsvWorkParam param in work_master.list) {
						DataWorkParam temp = DataManager.Instance.dataWork.SelectOne (string.Format ("work_id = {0}", param.work_id));
						if (temp.work_id != 0) {
							temp.Copy (param, temp.status);
						} else {
							DataManager.Instance.dataWork.list.Add (new DataWorkParam (param));
						}
					}
					DataManager.Instance.dataWork.Save (DataWork.FILENAME);
				}
				DataManager.Instance.kvs_data.WriteInt (DataManager.Instance.KEY_WORK_VERSION, DataManager.Instance.config.ReadInt (DataManager.Instance.KEY_WORK_VERSION));
				DataManager.Instance.kvs_data.Save (DataKvs.FILE_NAME);
				DataManager.Instance.AllLoad ();
				m_eStep = STEP.CHECK_UPDATE;
			}
			break;

		case STEP.DATAMANAGER_SETUP:
			if (bInit) {
			}

			if (SpriteManager.Instance.IsIdle ()) {
				m_goRoot.SetActive (true);
				m_btnStart.gameObject.SetActive (false);
				m_eStep = STEP.SOUND_LOAD;
			}
			if (m_csLoading != null) {
				m_csLoading.ViewPercent (0.0f);
			}
			break;
		case STEP.SOUND_LOAD:
			m_btnStart.gameObject.SetActive (true);
			m_eStep = STEP.IDLE;

			if (ReviewManager.Instance.IsReadyReview () && 3 < DataManager.Instance.kvs_data.ReadInt (DefineOld.USER_LEVEL)) {
				m_eStep = STEP.REVIEW;
			}
			break;

		case STEP.REVIEW:
			if (bInit) {
				GameObject obj = PrefabManager.Instance.MakeObject ("prefab/CtrlReviewWindow", m_goRoot.transform.parent.gameObject);
				m_reviewWindow = obj.GetComponent<CtrlReviewWindow> ();
				m_reviewWindow.Initialize ();

				m_goRoot.SetActive (false);

			}
			if (m_reviewWindow.IsEnd ()) {
				m_goRoot.SetActive (true);
				Destroy (m_reviewWindow.gameObject);;
				m_eStep = STEP.IDLE;
			}
			break;

		case STEP.IDLE:
			if (bInit) {
				m_btnStart.TriggerClear ();
				m_btnBackup.TriggerClear ();
			}
			if (m_btnStart.ButtonPushed) {
				m_eStep = STEP.DB_SETUP;
				SoundManager.Instance.PlaySE ("se_cleanup" , "https://s3-ap-northeast-1.amazonaws.com/every-studio/app/sound/se");

				GoogleAnalytics.Instance.Log ("push_start");

				/*
				PlayerPrefs.SetInt (DefineOld.USER_WIDTH, 45);
				PlayerPrefs.SetInt (DefineOld.USER_HEIGHT, 45);
				PlayerPrefs.Save ();
				*/

			} else if (m_btnBackup.ButtonPushed) {

				string backupDB = System.IO.Path.Combine (Application.persistentDataPath, DefineOld.DB_NAME_DOUBTSUEN_BK );
				if (System.IO.File.Exists (backupDB) == false ) {
					m_eStep = STEP.DB_BACKUP_NOEXIST;
				} else {
					m_eStep = STEP.DB_BACKUP_CHECK;
				}
			} else {
			}

			break;

		case STEP.DB_SETUP:
			//Debug.LogError (STEP.DB_SETUP); 
			if (bInit) {
			}
			if (true) {
				//if (m_tkKvsOpen.Completed) {

				if (DataManager.Instance.m_csvItem.list.Count == 0) {
					CsvItem initial_csv_item = new CsvItem ();
					initial_csv_item.Load ("csv/master/InitialCsvItem");
					foreach (CsvItemParam param in initial_csv_item.list) {
						DataManager.Instance.m_csvItem.list.Add (param);
					}
				}
				if (DataManager.Instance.m_csvMonster.list.Count == 0) {
					CsvMonster initial_csv_monster = new CsvMonster ();
					initial_csv_monster.Load ("csv/master/InitialCsvMonster");
					foreach (CsvMonsterParam param in initial_csv_monster.list) {
						DataManager.Instance.m_csvMonster.list.Add (param);
					}
				}

				if (DataManager.Instance.m_dataItem.list.Count == 0) {
					DataManager.Instance.data_kvs.WriteInt (DefineOld.USER_SYAKKIN,300000000);
					DataManager.Instance.data_kvs.WriteInt (DefineOld.USER_TICKET,5);
					DataManager.Instance.data_kvs.WriteInt (DefineOld.USER_SYOJIKIN,10000);

					DataItem initial_data_item = new DataItem ();
					initial_data_item.Load ("csv/master/InitialDataItem");
					foreach (DataItemParam param in initial_data_item.list) {
						DataManager.Instance.m_dataItem.list.Add (param);
					}
				}

				if (DataManager.Instance.dataMonster.list.Count == 0) {
					DataMonster initial_data_monster = new DataMonster ();
					initial_data_monster.Load ("csv/master/InitialDataMonster");
					foreach (DataMonsterParam param in initial_data_monster.list) {
						DataManager.Instance.dataMonster.list.Add (param);
					}
				}
				List<DataWorkParam> data_work_list = m_dbWork.All;
				if (data_work_list.Count == 0) {
					CsvWork initial_csv_work = new CsvWork ();
					initial_csv_work.Load ("csv/master/InitialCsvWork");

					foreach (CsvWorkParam csv_work_data in initial_csv_work.list) {
						DataWorkParam data = new DataWorkParam (csv_work_data);
						// 最初に出現していいのはappear_work_id== 0とlevel<=1のものだけ
						if (data.appear_work_id == 0 && data.level <= 1 ) {
							data.status = 1;
						}
						m_dbWork.list.Add(data);
					}
					m_dbWork.Save (DataWork.FILENAME);
				}
				/*
				List<DataMonsterParam> data_monster_list = DataManager.Instance.dataMonster.list;
				//Debug.LogError( string.Format( "data_monster_list.Count:{0}" , data_monster_list.Count ));
				if (data_monster_list.Count == 0) {
					DataMonsterParam monster = new DataMonsterParam ();
					monster.monster_serial = 1;
					monster.monster_id = 1;
					monster.item_serial = 12;
					monster.condition = (int)DefineOld.Monster.CONDITION.FINE;
					monster.collect_time = TimeManager.StrNow ();

					string strHungry = TimeManager.StrGetTime (-1 * 60 * 30);
					monster.meal_time = strHungry;
					monster.clean_time = strHungry;
					m_dbMonster.Replace (monster);
				}
				*/

				/*
				List<CsvMonsterParam> data_monster_master_list = DataManager.Instance.m_csvMonster.list;
				if (data_monster_master_list.Count == 0) {
					var csvMonsterMaster = new CsvMonster ();
					csvMonsterMaster.Load ();
					foreach (CsvMonsterData csv_monster_master_data in csvMonsterMaster.All) {
						DataMonster.Replace (csv_monster_master_data);
					}
				}
				*/

				/*
				//マスターデータの生成用ですが、状況的にこれはおこらないようにする
				//List<CsvItemParam> data_item_master = m_dbItemMaster.SelectAll ();
				List<CsvItemParam> data_item_master = DataManager.Instance.m_csvItem.list;
				//Debug.LogError (string.Format ("count:{0}", data_item_master.Count));
				if (data_item_master.Count == 0) {
					foreach (CsvItemParam csv_item_data in csvItem.All) {
						CsvItemParam data = new CsvItemParam (csv_item_data);
						if (data.open_item_id == 0) {
							data.status = 1;
						}
						m_dbItemMaster.Replace (data);
					}
				}
				*/
				m_eStep = STEP.INPUT_WAIT;
			}

			break;

		case STEP.INPUT_WAIT:
			if (bInit) {
				m_btnStart.TriggerClear ();
			}
			if (true) {

				// とりあえず全部調べる
				List<DataWorkParam> cleared_work_list = m_dbWork.Select ( string.Format(" status = {0} " , (int)DefineOld.Work.STATUS.CLEARD ));
				foreach (DataWorkParam work in cleared_work_list) {
					List<CsvMonsterParam> list_monster = m_dbMonsterMaster.Select ( string.Format(" status = 0 and open_work_id = {0} " , work.work_id ));
					foreach (CsvMonsterParam data_monster_master in list_monster) {
						Dictionary< string , string > monster_master_dict = new Dictionary< string , string > ();
						monster_master_dict.Add ("status", "1");

						m_dbMonsterMaster.Update (monster_master_dict , string.Format( "monster_id = {0}" , data_monster_master.monster_id ) );
					}

				}
				m_btnStart.TriggerClear ();
				SceneManager.LoadScene ("park_main");
				//Application.LoadLevel ("park_main");
			}
			break;

		case STEP.DB_BACKUP_NOEXIST:
			if (bInit) {
				GameObject objOjisan = PrefabManager.Instance.MakeObject ("prefab/PrefOjisanCheck", m_posDisplay);
				m_ojisanCheck = objOjisan.GetComponent<CtrlOjisanCheck> ();
				m_ojisanCheck.Initialize ("バックアップファイルが\n存在しません", true);
			}
			if (m_ojisanCheck.IsYes ()) {
				Destroy (m_ojisanCheck.gameObject);
				m_eStep = STEP.IDLE;
			}
			break;
		case STEP.DB_BACKUP_CHECK:
			if (bInit) {
				GameObject objOjisan = PrefabManager.Instance.MakeObject ("prefab/PrefOjisanCheck", m_posDisplay );
				m_ojisanCheck = objOjisan.GetComponent<CtrlOjisanCheck> ();
				m_ojisanCheck.Initialize ("自動保存されたデータ\nを利用して\nバックアップを行います\n\nよろしいですか");
			}
			if (m_ojisanCheck.IsYes ()) {
				SoundManager.Instance.PlaySE (SoundName.BUTTON_PUSH , "https://s3-ap-northeast-1.amazonaws.com/every-studio/app/sound/se");
				Destroy (m_ojisanCheck.gameObject);
				m_eStep = STEP.DB_BACKUP;
			} else if (m_ojisanCheck.IsNo ()) {
				SoundManager.Instance.PlaySE (SoundName.BUTTON_PUSH , "https://s3-ap-northeast-1.amazonaws.com/every-studio/app/sound/se");
				Destroy (m_ojisanCheck.gameObject);
				m_eStep = STEP.IDLE;
			} else {
			}
			break;
		case STEP.DB_BACKUP:
			/*
			if (bInit) {
				GameObject objOjisan = PrefabManager.Instance.MakeObject ("prefab/PrefOjisanCheck", m_posDisplay);
				m_ojisanCheck = objOjisan.GetComponent<CtrlOjisanCheck> ();
				m_ojisanCheck.Initialize ("完了しました\nゲームをスタートしてください", true);

				string sourceDB = System.IO.Path.Combine (Application.persistentDataPath, DefineOld.DB_NAME_DOUBTSUEN );
				string dummyDB = System.IO.Path.Combine (Application.persistentDataPath, DefineOld.DB_NAME_DOUBTSUEN + "." + TimeManager.StrGetTime() );
				string backupDB = System.IO.Path.Combine (Application.persistentDataPath, DefineOld.DB_NAME_DOUBTSUEN_BK );
				string backup2DB = System.IO.Path.Combine (Application.persistentDataPath, DefineOld.DB_NAME_DOUBTSUEN_BK2 );
				if (System.IO.File.Exists (sourceDB)) {
					System.IO.File.Copy (sourceDB, dummyDB, true);
				}
				if (System.IO.File.Exists (backupDB)) {
					System.IO.File.Copy (backupDB, sourceDB, true);
				}
			}
			if (m_ojisanCheck.IsYes ()) {
				Destroy (m_ojisanCheck.gameObject);
				m_eStep = STEP.IDLE;
			}
			*/
			break;
		case STEP.NETWORK_ERROR:
			if (bInit) {
				m_btnNetworkError.gameObject.SetActive (true);
				m_btnNetworkError.TriggerClear ();
			}
			if (m_btnNetworkError.ButtonPushed) {
				m_btnNetworkError.gameObject.SetActive (false);
				m_eStep = STEP.CHECK_CONFIG;
			}
			break;
		default:
			break;
		}

		if (m_btnTutorialReset.ButtonPushed) {
			PlayerPrefs.DeleteAll ();

			string full_path = System.IO.Path.Combine (Application.persistentDataPath , DefineOld.DB_NAME_DOUBTSUEN );
			System.IO.File.Delete( full_path );

			m_btnTutorialReset.TriggerClear ();
		}
		if (m_btnCacheClear.ButtonPushed) {
			Caching.CleanCache();
			m_btnCacheClear.TriggerClear ();
		}
	
	}
}

























