using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Advertisements;

public class CtrlHelp : ButtonBase {

	public enum STEP
	{
		NONE				= 0,
		WAIT				,
		IDLE				,
		CHECK				,
		MOVIE				,
		RESULT_SUCCESS		,
		RESULT_SKIP			,
		RESULT_FAIL			,
		MAX			,
	}
	public STEP m_eStep;
	public STEP m_eStepPre;

	[SerializeField]
	private GameObject m_goPanelFront;

	[SerializeField]
	private UI2DSprite m_sprImage;

	public float m_fTimerCheck;
	private CtrlOjisanCheck m_ojisanCheck;

	public enum ACTION_TYPE
	{
		NONE		= 0,
		MOVIE		,
		TWITTER		,
		MAX			,
	}
	private ACTION_TYPE m_eActionType;

	private bool IsAppear(){
		bool bRet = false;

		/*
		// そもそもこれが出来てないなら出来ません
		if (UnityAdsSupporter.Instance.IsReady () == false ) {
			return false;
		}
		*/


		string strTime = DataManager.Instance.data_kvs.Read (DataManager.Instance.KEY_UNITYADS_LASTPLAY_TIME);
		TimeSpan time_span = TimeManager.Instance.GetDiff (strTime, TimeManager.StrGetTime ());
		/*
		Debug.Log (60 * 60);
		Debug.Log (time_span.TotalSeconds);
		*/

		if (60 * 60 < time_span.TotalSeconds) {
			bRet = true;
		}
		return bRet;
	}

	// Use this for initialization
	void Start () {
		m_sprImage.gameObject.SetActive (false);
		if (DataManager.Instance.data_kvs.HasKey (DataManager.Instance.KEY_UNITYADS_LASTPLAY_TIME) == false ) {
			DataManager.Instance.data_kvs.Write (DataManager.Instance.KEY_UNITYADS_LASTPLAY_TIME, TimeManager.StrGetTime (-1*60*50));
		}

		if (IsAppear ()) {
			m_eStep = STEP.IDLE;
		} else {
			m_eStep = STEP.WAIT;
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
		case STEP.WAIT:
			if (bInit) {
				m_fTimerCheck = 0.0f;
				m_sprImage.gameObject.SetActive (false);
			}
			m_fTimerCheck += Time.deltaTime;

			if (5.0f < m_fTimerCheck) {
				if (IsAppear ()) {
					m_eStep = STEP.IDLE;
				}
				m_fTimerCheck = 0.0f;
			}
			break;

		case STEP.IDLE:
			if (bInit) {
				m_sprImage.gameObject.SetActive (true);
				TriggerClear ();

				if (m_eActionType== ACTION_TYPE.NONE) {
					m_eActionType = DataManager.Instance.GetHelpActionType ();
				}
				//Debug.LogError (m_eActionType);
			}
			if (ButtonPushed) {
				m_eStep = STEP.CHECK;
			}
			break;

		case STEP.CHECK:
			if (bInit) {
				if (m_eActionType == ACTION_TYPE.MOVIE) {
					if (UnityAdsSupporter.Instance.IsReady () == false) {
						m_eActionType = ACTION_TYPE.TWITTER;
					}
				}
				string strAction = "";
				if (m_eActionType == ACTION_TYPE.MOVIE) {
					strAction = "動画を見ている間に";
				} else {
					strAction = "私の変わりに広報活動していただけると";
				}
				GameObject objOjisan = PrefabManager.Instance.MakeObject ("prefab/PrefOjisanCheck", m_goPanelFront);
				m_ojisanCheck = objOjisan.GetComponent<CtrlOjisanCheck> ();
				m_ojisanCheck.Initialize (string.Format( "{0}\n今いる動物の\n\n【[4169e1]エサやり・お掃除・病気の治療[-]】\n\nを私がやっておきますよ！" , strAction ));
			}
			if (m_ojisanCheck.IsYes ()) {
				Destroy (m_ojisanCheck.gameObject);

				if (m_eActionType == ACTION_TYPE.MOVIE) {
					if (UnityAdsSupporter.Instance.ShowRewardedAd ()) {
						m_eStep = STEP.MOVIE;
					} else {
						m_eStep = STEP.IDLE;
					}
				} else {
				
					// WebブラウザのTwitter投稿画面を開く
					string strMessage = "かわいい動物に会える！自分だけの動物園を作ろう！\n楽しい放置シミュレーションゲーム、【ポケット動物園】 #ポケット動物園";
					#if UNITY_ANDROID
					if( DataManager.Instance.config.HasKey( DataManager.Instance.KEY_SHARE_MESSAGE_ANDROID )){
						strMessage = DataManager.Instance.config.Read( DataManager.Instance.KEY_SHARE_MESSAGE_ANDROID);
					}
					#elif UNITY_IPHONE
					if( DataManager.Instance.config.HasKey( DataManager.Instance.KEY_SHARE_MESSAGE_IOS )){
					strMessage = DataManager.Instance.config.Read( DataManager.Instance.KEY_SHARE_MESSAGE_IOS);
					}
					#endif

					Application.OpenURL("http://twitter.com/intent/tweet?text=" + WWW.EscapeURL(strMessage));
					m_eStep = STEP.RESULT_SUCCESS;
				
				}
			} else if (m_ojisanCheck.IsNo ()) {
				Destroy (m_ojisanCheck.gameObject);
				m_eStep = STEP.IDLE;
			} else {
			}
			break;

		case STEP.MOVIE:
			ShowResult result = ShowResult.Finished;
			if (UnityAdsSupporter.Instance.IsShowed (out result)) {
				switch (result) {
				case ShowResult.Finished:
					m_eStep = STEP.RESULT_SUCCESS;
					break;
				case ShowResult.Skipped:
					m_eStep = STEP.RESULT_SKIP;
					break;
				case ShowResult.Failed:
				default:
					m_eStep = STEP.RESULT_FAIL;
					break;
				}
				DataManager.Instance.data_kvs.Write (DataManager.Instance.KEY_UNITYADS_LASTPLAY_TIME, TimeManager.StrGetTime ());
			}
			break;

		case STEP.RESULT_SUCCESS:
			if (bInit) {
				foreach (CtrlFieldItem field_item in GameMain.ParkRoot.m_fieldItemList) {
					field_item.Clean ();
					field_item.Meal ();
				}
				foreach (DataMonsterParam monster in DataManager.Instance.dataMonster.list) {
					monster.condition = (int)DefineOld.Monster.CONDITION.FINE;
				}
				GameObject objOjisan = PrefabManager.Instance.MakeObject ("prefab/PrefOjisanCheck", m_goPanelFront);
				m_ojisanCheck = objOjisan.GetComponent<CtrlOjisanCheck> ();
				m_ojisanCheck.Initialize ("ふぅ、お仕事終わらせましたよ\n引き続き園内の見回り\nお願いしますね！" , true );
			}
			if (m_ojisanCheck.IsYes ()) {
				Destroy (m_ojisanCheck.gameObject);
				m_eStep = STEP.WAIT;
			}
			break;
		case STEP.RESULT_SKIP:
			if (bInit) {
				foreach (CtrlFieldItem field_item in GameMain.ParkRoot.m_fieldItemList) {
					field_item.Clean ();
					field_item.Meal ();
				}
				GameObject objOjisan = PrefabManager.Instance.MakeObject ("prefab/PrefOjisanCheck", m_goPanelFront);
				m_ojisanCheck = objOjisan.GetComponent<CtrlOjisanCheck> ();
				m_ojisanCheck.Initialize ("すいません、少し時間が足りませんでした\nエサやりとお掃除は済ませたので\n【[4169e1]病気の治療[-]】はお願いしますね" , true );
			}
			if (m_ojisanCheck.IsYes ()) {
				Destroy (m_ojisanCheck.gameObject);
				m_eStep = STEP.WAIT;
			}
			break;
		case STEP.RESULT_FAIL:
			if (bInit) {
				GameObject objOjisan = PrefabManager.Instance.MakeObject ("prefab/PrefOjisanCheck", m_goPanelFront);
				m_ojisanCheck = objOjisan.GetComponent<CtrlOjisanCheck> ();
				m_ojisanCheck.Initialize ("\nエラーが発生しました\n" , true );
			}
			if (m_ojisanCheck.IsYes ()) {
				Destroy (m_ojisanCheck.gameObject);
				m_eStep = STEP.WAIT;
			}
			break;


		}

	}
}
