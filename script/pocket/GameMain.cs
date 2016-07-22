using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NendUnityPlugin.AD;
using UnityEngine.SceneManagement;

public class GameMain : MonoBehaviour {

	public bool TutorialInputLock;
	private int m_iTutorialIndex;
	public int TutorialIndex {
		get{ return m_iTutorialIndex; }
		set{ m_iTutorialIndex = value; }
	}

	public int SwitchItemSerial;
	public int SwitchClean;
	public int SwitchFood;
	public int SwitchSetting;
	public int SwitchClose;
	public bool bSwitchTab;
	public int SwitchTabIndex;
	public bool TutorialBuildup;
	public int TutorialMonster;
	public bool bOjisanCheck;
	public int OjisanCheckIndex;
	public int PreSettingItemId;

	#region DB関係
	#endregion

	#region SerializeField
	[SerializeField]
	private CtrlParkRoot m_ctrlParkRoot;
	static public CtrlParkRoot ParkRoot{
		get{ return Instance.m_ctrlParkRoot; }
	}

	[SerializeField]
	private GameObject m_goPanelFront;
	static public GameObject PanelFront{
		get{ return Instance.m_goPanelFront; }
	}

	[SerializeField]
	private Camera m_cameraUI;

	[SerializeField]
	private CtrlHeader m_header;
	[SerializeField]
	private CtrlCollectGold m_collectGold;
	[SerializeField]
	private CtrlPopupWork m_popupWork;
	[SerializeField]
	private CtrlFukidashiWork m_fukidashiWork;
	#endregion



	private int m_iPushedBuildingSerial;
	public int BuildingSerial{
		get{ return m_iPushedBuildingSerial; }
		set{ m_iPushedBuildingSerial = value; }
	}



	public void AddFukidashi( int _iWorkId , string _strMessage ){
		m_popupWork.AddCleardWorkId (_iWorkId);
		m_fukidashiWork.AddMessage (_strMessage);
		//m_fukidashiWork.m_MessageQueue.Enqueue ( string.Format( "[000000]{0}[-]" , _strMessage ) );
	}

	private bool m_bListRefresh;
	static public bool ListRefresh{
		get{ 
			bool bRet = Instance.m_bListRefresh;
			Instance.m_bListRefresh = false;
			return bRet;
		}
		set {
			Instance.m_bListRefresh = value;
		}
	}

	public enum TABLE_TYPE {
		NONE			= 0,
		WORK			,
		ITEM			,
		ITEM_MASTER		,
		MONSTER			,
		MONSTER_MASTER	,
		STAFF			,
		STAFF_MASTER	,
		MAX				,
	}

	protected static GameMain instance = null;
	protected static bool m_bInitialized = false;

	public static bool IsInstance(){
		return instance != null;
	}
	public static GameMain Instance
	{
		get
		{
			// InputManagerの唯一のインスタンスを生成
			if (instance == null)
			{
				GameObject obj = GameObject.Find("GameMain");

				if (obj == null)
				{
					obj = new GameObject("GameMain");
				}
				instance = obj.GetComponent<GameMain>();
				if (instance == null)
				{
					instance = obj.AddComponent<GameMain>();
				}
				instance.initialize();
			}
			if (m_bInitialized == false)
			{
				instance.initialize();
			}
			return instance;
		}
	}
	private void initialize(){

		if (InitialMain.INITIALIZE_MAIN == false) {
			SceneManager.LoadScene ("initial");

			return;
		}

		m_eMoveStatus = STATUS.NONE;
		m_iMoveTab = 0;

		string strUnityAdsAppId = "";
		#if UNITY_ANDROID
		strUnityAdsAppId = DataManager.Instance.config.Read( DataManager.Instance.KEY_UNITYADS_APP_ID_ANDROID );
		#elif UNITY_IOS
		strUnityAdsAppId = DataManager.Instance.config.Read( DataManager.Instance.KEY_UNITYADS_APP_ID_IOS );
		#endif
		UnityAdsSupporter.Instance.Initialize (strUnityAdsAppId);

		if (m_bInitialized == false) {
			//int iWidth = PlayerPrefs.GetInt (DefineOld.USER_WIDTH);
			//int iHeight= PlayerPrefs.GetInt (DefineOld.USER_HEIGHT);
			//DataManager.Instance.user.Initialize (iWidth, iHeight);

			foreach (PageBase2 page in m_PageList) {
				page.Close ();
			}
			m_eStatus = STATUS.NONE;
			m_eStep = STEP.DB_SETUP;

			/*
			m_csUniWebView.OnLoadComplete += OnLoadComplete;
			m_csUniWebView.OnReceivedMessage += OnReceivedMessage;
			//m_csUniWebView.OnEvalJavaScriptFinished += OnEvalJavaScriptFinished;

			m_csUniWebView.insets = new UniWebViewEdgeInsets( UniWebViewHelper.screenHeight - 50,0,0,UniWebViewHelper.screenWidth - 320);
			Debug.Log ("helper width :" + UniWebViewHelper.screenWidth );
			Debug.Log ("helper height:" + UniWebViewHelper.screenHeight );
			m_csUniWebView.HideToolBar (false);
			//m_csUniWebView.Load(DefineOld.strFooterAdUrl);
			m_csUniWebView.Load( "http://ad.xnosserver.com/apps/bokunodoubutsuen_ios/ad.html" );
			*/
			SetStatus (STATUS.PARK);
			AdsManager.Instance.Initialize ();

		}

		//Debug.Log (TutorialManager.Instance.m_eStep);

		m_bInitialized = true;
		return;
	}

	public enum STEP {
		NONE			= 0,
		DB_SETUP		,
		IDLE			,
		REVIEW			,
		BACKUP_CHECK	,
		MAX			,
	}
	public STEP m_eStep;
	private STEP m_eStepPre;

	public float m_fBackupInterval;
	public float m_fBackupIntervalTimer;
	public CtrlReviewWindow m_reviewWindow;


	public enum STATUS{
		NONE			= 0,
		PARK			,
		BOOK			,
		WORK			,
		ITEM			,
		CAGE_DETAIL		,
		OFFICE_DETAIL	,
		MAX				,
	}

	public STATUS m_eStatus;
	private STATUS m_eStatusPre;

	public List<PageBase2> m_PageList = new List<PageBase2>();

	public bool IsIdle(){
		return (m_eStep == STEP.IDLE);
	}

	public void SetStatus( STATUS _eStatus ){

		if (m_eStatus != _eStatus) {

			m_PageList [(int)m_eStatus].Close ();
			m_PageList [(int)_eStatus].Initialize ();
			m_eStatus = _eStatus;

			if (m_eStatus != STATUS.PARK && !TutorialManager.Instance.IsTutorial ()) {
				AdsManager.Instance.CallInterstitial ();
			}

		}
		return;
	}

	// Use this for initialization
	void Start () {
		initialize ();
		AdsManager.Instance.ShowAdBanner (true);
		AdsManager.Instance.ShowIcon (true);
	}
	
	// Update is called once per frame
	void Update () {

		bool bInit = false;
		if (m_eStepPre != m_eStep) {
			m_eStepPre  = m_eStep;
			bInit = true;
		}

		switch (m_eStep) {
		case STEP.DB_SETUP:
			if (bInit) {
				/*
				m_dbItem = new DBItem (DefineOld.DB_TABLE_ASYNC_ITEM);
				m_dbItemMaster = new DBItemMaster (DefineOld.DB_TABLE_ASYNC_ITEM_MASTER);
				m_dbWork = new DBWork (DefineOld.DB_TABLE_ASYNC_WORK);
				m_dbMonster = new DBMonster (DefineOld.DB_TABLE_ASYNC_MONSTER);
				m_dbMonsterMaster = new DBMonsterMaster (DefineOld.DB_TABLE_ASYNC_MONSTER_MASTER);
				m_dbStaff = new DBStaff (DefineOld.DB_TABLE_ASYNC_STAFF);
				if (m_dbKvs == null) {
					m_dbKvs = new DBKvs (DefineOld.DB_TABLE_ASYNC_KVS);
				}
				m_dbItem.Open (DefineOld.DB_NAME_DOUBTSUEN, DefineOld.DB_FILE_DIRECTORY, "");
				m_dbItemMaster.Open (DefineOld.DB_NAME_DOUBTSUEN, DefineOld.DB_FILE_DIRECTORY, "");
				m_dbWork.Open (DefineOld.DB_NAME_DOUBTSUEN, DefineOld.DB_FILE_DIRECTORY, "");
				m_dbMonster.Open (DefineOld.DB_NAME_DOUBTSUEN, DefineOld.DB_FILE_DIRECTORY, "");
				m_dbMonsterMaster.Open (DefineOld.DB_NAME_DOUBTSUEN, DefineOld.DB_FILE_DIRECTORY, "");
				m_dbStaff.Open (DefineOld.DB_NAME_DOUBTSUEN, DefineOld.DB_FILE_DIRECTORY, "");
				m_tkKvsOpen = m_dbKvs.Open (DefineOld.DB_NAME_DOUBTSUEN, DefineOld.DB_FILE_DIRECTORY, ""); // DefineOld.DB_PASSWORD
				*/
			}
			//if (m_tkKvsOpen.Completed) {
			if (true) {
				//DataManager.itemMaster = m_dbItemMaster.SelectAll ();

				m_header.Initialize ();
				m_header.RefleshNum ();
				m_collectGold.Initialize ();
				m_fukidashiWork.Initialize ();


				m_eStep = STEP.IDLE;
			}
			break;
		case STEP.IDLE:
			if (bInit) {
				m_fBackupInterval = 10.0f;
				m_fBackupIntervalTimer = 0.0f;
			}
			/*
			m_fBackupIntervalTimer += Time.deltaTime;
			if (m_fBackupInterval < m_fBackupIntervalTimer) {
				m_fBackupIntervalTimer -= m_fBackupInterval;

				m_eStep = STEP.BACKUP_CHECK;
			}
			*/
			if (TutorialManager.Instance.IsTutorial () == false  && ReviewManager.Instance.IsReadyReview()) {
				m_eStep = STEP.REVIEW;
			}
			break;

		case STEP.REVIEW:
			if (bInit) {
				GameObject obj = PrefabManager.Instance.MakeObject ("prefab/CtrlReviewWindow", m_goPanelFront);
				m_reviewWindow = obj.GetComponent<CtrlReviewWindow> ();
				m_reviewWindow.Initialize ();

			}
			if (m_reviewWindow.IsEnd ()) {
				Destroy (m_reviewWindow.gameObject);;
				m_eStep = STEP.IDLE;
			}
			break;

		case STEP.BACKUP_CHECK:
			/*
			if (bInit) {

				string sourceDB = System.IO.Path.Combine (Application.persistentDataPath, DefineOld.DB_NAME_DOUBTSUEN );
				string backup2DB = System.IO.Path.Combine (Application.persistentDataPath, DefineOld.DB_NAME_DOUBTSUEN_BK2 );
				System.IO.File.Copy (sourceDB, backup2DB, true);

				m_dbItemBackup = new DBItem (DefineOld.DB_NAME_DOUBTSUEN_BK2);
				//Debug.Log ("STEP.BACKUP_CHECK");

			}
			//Debug.Log ("frame");

				m_eStep = STEP.IDLE;
				try
				{
					// DBおかしくなってたらここでThrowされる
					List<DataItemParam> check = m_dbItemBackup.SelectAll ();

					//Debug.Log( "Copy" );
					//string sourcePath = System.IO.Path.Combine (Application.streamingAssetsPath, DefineOld.DB_FILE_DIRECTORY + DefineOld.DB_NAME_DOUBTSUEN );
					string backupDB = System.IO.Path.Combine (Application.persistentDataPath, DefineOld.DB_NAME_DOUBTSUEN_BK );
					string backup2DB = System.IO.Path.Combine (Application.persistentDataPath, DefineOld.DB_NAME_DOUBTSUEN_BK2 );
					if (System.IO.File.Exists (backup2DB)) {
						System.IO.File.Copy (backup2DB, backupDB, true);
					}
				}catch{
					//Debug.LogError ("まずー");
				}
				*/
			break;

		default:
			break;
		}
	}

	public bool m_bStartSetting;
	public int m_iSettingItemId;
	public int m_iSettingItemSerial;
	public STATUS m_eMoveStatus;
	public int m_iMoveTab;
	public void SettingItem( int _iItemId , int _iItemSerial ){
		m_bStartSetting = true;
		m_iSettingItemId = _iItemId;
		m_iSettingItemSerial = _iItemSerial;
		return;
	}
	public int m_iCostNow;
	public int m_iCostMax;
	public int m_iCostNokori;

	public void HeaderRefresh( bool _bForce = false ){

		ParkRoot.ConnectingRoadCheck ();

		// 一時間あたりの売上
		int iUriagePerHour = 0;
		List<DataItemParam> item_list = DataManager.Instance.m_dataItem.Select (" item_serial != 0 ");
		foreach (DataItemParam item in item_list) {
			iUriagePerHour += item.GetUriagePerHour ();
		}
		DataManager.Instance.kvs_data.WriteInt (DefineOld.USER_URIAGE_PAR_HOUR, iUriagePerHour);

		// 一時間あたりの支出
		int iShisyutsuHour = 0;
		foreach (DataItemParam item in item_list) {
			iShisyutsuHour += item.GetShiSyutsuPerHour ();
		}
		DataManager.Instance.kvs_data.WriteInt (DefineOld.USER_SHISYUTU_PAR_HOUR, iShisyutsuHour);

		m_header.RefleshNum (_bForce);
	}

	static public bool GetGrid( GameObject _goRoot , Vector2 _inputPoint , out int _iX , out int _iY , Camera _camera ){
		_iX = 0;
		_iY = 0;

		if (GameMain.instance.TutorialInputLock == true ) {
			return false;
		}

		if (DebugRoot.Instance.IsDebugMode ()) {
			return false;
		}

		bool bRet = false;
		RaycastHit hit;

		//Debug.Log (_camera);
		Ray ray = _camera.ScreenPointToRay (new Vector3 (_inputPoint.x, _inputPoint.y, 0.0f));
		float fDistance = 100.0f;

		_iX = 0;
		_iY = 0;

		//レイを投射してオブジェクトを検出
		if (Physics.Raycast (ray, out hit, fDistance)) {
			Debug.Log (hit.collider.gameObject.name);
			if (hit.collider.gameObject.name.Equals (DataManager.Instance.KEY_TOUCHABLE_FIELD_NAME)) {
				GameObject objPoint = new GameObject ();
				objPoint.transform.position = hit.point;
				objPoint.transform.parent = _goRoot.transform;

				// ここの計算式は後で見直します
				int calc_x = Mathf.FloorToInt ((objPoint.transform.localPosition.x + (objPoint.transform.localPosition.y * 2.0f)) / 160.0f);
				int calc_y = Mathf.FloorToInt (((objPoint.transform.localPosition.y * 2.0f) - objPoint.transform.localPosition.x) / 160.0f);
				//Debug.Log ("calc_x=" +  calc_x.ToString () + " calc_y=" +  calc_y.ToString ());

				bRet = true;
				_iX = calc_x;
				_iY = calc_y;

				//Debug.LogError (string.Format ("x:{0} y:{1} posx{2} posy{3}", calc_x, calc_y, objPoint.transform.localPosition.x, objPoint.transform.localPosition.y));

				Destroy (objPoint);
			}
		}
		return bRet;
	}

	static public bool GetGrid( GameObject _goRoot , Vector2 _inputPoint , out int _iX , out int _iY ){
		return GetGrid (_goRoot, _inputPoint, out _iX, out _iY, Instance.m_cameraUI );
	}
	static public bool GetGrid( Vector2 _inputPoint , out int _iX , out int _iY ){
		return GetGrid (ParkRoot.gameObject, _inputPoint, out _iX, out _iY );
	}
	static public bool GridHit( int _iX , int _iY , int _iItemX , int _iItemY , int _iItemWidth , int _iItemHeight , out int _iOffsetX , out int _iOffsetY ){
		_iOffsetX = 0;
		_iOffsetY = 0;

		//Debug.Log ("x:" + _dataItem.x.ToString () + " y:" + _dataItem.y.ToString () + " w:" + _dataItem.width.ToString () + " h:" + _dataItem.height.ToString ());

		bool bHit = false;
		for (int x = _iItemX; x < _iItemX + _iItemWidth; x++) {
			for (int y = _iItemY; y < _iItemY + _iItemHeight; y++) {
				if (_iX == x && _iY == y) {

					_iOffsetX = x - _iItemX;
					_iOffsetY = y - _iItemY;
					bHit = true;
					break;
				}
			}
		}
		return bHit;
	}

	static public bool GridHit( int _iX , int _iY , DataItemParam _dataItem , out int _iOffsetX , out int _iOffsetY ){

		return GridHit (_iX, _iY, _dataItem.x, _dataItem.y, _dataItem.width, _dataItem.height, out _iOffsetX, out _iOffsetY);
		/*
		_iOffsetX = 0;
		_iOffsetY = 0;

		//Debug.Log ("x:" + _dataItem.x.ToString () + " y:" + _dataItem.y.ToString () + " w:" + _dataItem.width.ToString () + " h:" + _dataItem.height.ToString ());

		bool bHit = false;
		for (int x = _dataItem.x; x < _dataItem.x + _dataItem.width; x++) {
			for (int y = _dataItem.y; y < _dataItem.y + _dataItem.height; y++) {
				if (_iX == x && _iY == y) {

					_iOffsetX = x - _dataItem.x;
					_iOffsetY = y - _dataItem.y;
					bHit = true;
					break;
				}
			}
		}
		return bHit;
		*/
	}

	static public bool GridHit( int _iX , int _iY , DataItemParam _dataItem ){

		int iOffsetX = 0;
		int iOffsetY = 0;

		return GridHit (_iX, _iY, _dataItem, out iOffsetX, out iOffsetY);
	}



}










