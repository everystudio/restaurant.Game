using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class LocalNotificationManager : MonoBehaviour {

	private List<CsvLocalNotificationParam> m_localNotificationDataList = new List<CsvLocalNotificationParam>();

	public List<string> add_message_list = new List<string>();

	public List<int> id_list = new List<int>();
	#if UNITY_ANDROID
	static AndroidJavaObject m_plugin2 = null;
	#endif
	protected static LocalNotificationManager instance = null;
	public static LocalNotificationManager Instance {
		get {
			if (instance == null) {
				GameObject obj = GameObject.Find ("LocalNotificationManager");
				if (obj == null) {
					obj = new GameObject("LocalNotificationManager");
					//Debug.LogError ("Not Exist AtlasManager!!");
				}
				instance = obj.GetComponent<LocalNotificationManager> ();
				if (instance == null) {
					//Debug.LogError ("Not Exist AtlasManager Script!!");
					instance = obj.AddComponent<LocalNotificationManager>() as LocalNotificationManager;
				}
				instance.Initialize ();
			}
			return instance;
		}
	}



	private void Initialize(){
		DontDestroyOnLoad(gameObject);
		localnotificate_list.Clear ();
		add_message_list.Clear ();
		m_localNotificationDataList.Clear ();
		id_list.Clear ();
		#if UNITY_ANDROID && !UNITY_EDITOR
		// プラグイン名をパッケージ名+クラス名で指定する。
		m_plugin2 = new AndroidJavaObject( "com.everystudio.test001.TestLocalnotification" );
		#endif

	}

	public bool Add( CsvLocalNotificationParam _param ){

		bool bHit = false;
		foreach (CsvLocalNotificationParam param in m_localNotificationDataList) {
			if (param.id == _param.id) {
				bHit = true;
			}
		}
		if (bHit == false) {
			m_localNotificationDataList.Add (_param);
			add_message_list.Add (_param.message);

			//Debug.Log (string.Format ("second:{0} message{1}", _data.second, _data.message));
		}
		return bHit;
	}

	private List<int> localnotificate_list = new List<int> ();
	void OnApplicationPause(bool pauseStatus) {
		// ローカル通知用
		if (pauseStatus) {
			//TODO
			#if UNITY_IPHONE

			/*
			foreach( CsvLocalNotificationData data in m_localNotificationDataList ){
				ISN_LocalNotification local_notification = new ISN_LocalNotification (
					DateTime.Now.AddSeconds (data.second),
					data.message,
					false);

				id_list.Add( local_notification.Id );
				IOSNotificationController.Instance.ScheduleNotification (local_notification);
			}
			*/

			#elif UNITY_ANDROID

			int iTemp = 0;
			/*
			iTemp = EtceteraAndroid.scheduleNotification( DefineOld.iLocalNotifyMessageTime1, "どうぶつみっけ!", DefineOld.strLocalNotifyMessage1, DefineOld.strLocalNotifyMessage1, DefineOld.strLocalNotifyMessage1 );
			localnotificate_list.Add( iTemp );
			iTemp = EtceteraAndroid.scheduleNotification( DefineOld.iLocalNotifyMessageTime2, "どうぶつみっけ!", DefineOld.strLocalNotifyMessage2, DefineOld.strLocalNotifyMessage2, DefineOld.strLocalNotifyMessage2 );
			localnotificate_list.Add( iTemp );
			*/

			foreach( CsvLocalNotificationParam param in m_localNotificationDataList ){
				//Debug.LogError( string.Format( "add local notification:{0}" , data.message ));

				m_plugin2.Call ("sendNotification", (long)param.second , iTemp , param.title, param.message);
				iTemp += 1;
				//iTemp = EtceteraAndroid.scheduleNotification( data.second, data.title, data.message, data.message, data.message );
			}
			//iTemp = EtceteraAndroid.scheduleNotification( 20, "どうぶつみっけ!", DefineOld.strLocalNotifyMessage2, DefineOld.strLocalNotifyMessage2, DefineOld.strLocalNotifyMessage2 );
			#endif
		} else {
			#if UNITY_IPHONE
			// こっちの削除はなくてもいいらしい
			/*
			foreach( int set_id in id_list ){

				IOSNotificationController.Instance.CancelLocalNotificationById( set_id );
			}
			*/
			//IOSNotificationController.Instance.CancelAllLocalNotifications ();
			#elif UNITY_ANDROID && !UNITY_EDITOR
			for( int i = 0 ; i < m_localNotificationDataList.Count ; i++ ){
			//foreach( CsvLocalNotificationData data in m_localNotificationDataList ){
				m_plugin2.Call ("clearNotification", i );
			}
			#endif
		}




	}
}
