using UnityEngine;
using System.Collections;

public class DataSave : Singleton<DataSave> {

	public float m_fInterval;
	public const float INTERVAL = 10.0f;

	public bool m_bInitialized = false;

	public void save(){
		DataManager.Instance.DataSave ();
    }

	// 個人的には邪道なんだけど
	void Start(){
		//Debug.LogError ("start");
		Initialize ();
		/*
		m_dbItem.m_soDataItem.Load (DBItem.FILE_NAME);
		m_dbItemMaster.m_soCsvItemParam.Load (DBItemMaster.FILE_NAME);
		m_dbWork.m_soDataWork.Load (DBWork.FILE_NAME);
		m_dbMonster.m_soDataMonster.Load (DBMonster.FILE_NAME);
		m_dbMonsterMaster.m_soDataMonsterMaster.Load (DBMonsterMaster.FILE_NAME);
		m_dbKvs.m_soDataKvs.Load (DBKvs.FILE_NAME);
        m_dbStaff.m_soDataStaff.Load(DBStaff.FILE_NAME);
        */
	}

	void OnApplicationPause(bool pauseStatus) {
		///Debug.LogError ("here");
		Initialize ();
		if (pauseStatus) {
			save ();

		} else {
		}
	}
	#if UNITY_EDITOR
	void Update(){

		m_fInterval += Time.deltaTime;

		if (INTERVAL < m_fInterval) {
			m_fInterval -= INTERVAL;
			save ();
		}

	}
	#endif

}












