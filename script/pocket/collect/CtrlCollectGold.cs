using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class CtrlCollectGold : Singleton<CtrlCollectGold> {

	public UnityEvent m_eventCollect = new UnityEvent ();

	#region SerializeField
	[SerializeField]
	private UILabel m_lbCollectGold;
	[SerializeField]
	private ButtonBase m_btnCollect;
	#endregion

	public float m_fTimer;
	public float m_fCheckInterval;
	public int m_iCollectGold;
	public int m_iCollectExp;

	public int m_iSearchIndex;

	public bool m_bInitialize;
	void Start(){
		m_bInitialize = false;
	}

	public void AddCollect( int _iGold , int _iExp ){
		m_iCollectGold += _iGold;
		SetCollectGold (m_iCollectGold);
		m_iCollectExp += _iExp;
	}

	private IEnumerator updateCollect(){
		int iCollectGold = 0;
		int iCollectExp = 0;
		List<DataItemParam> aaa = DataManager.Instance.m_dataItem.Select (" item_serial != 0 " );
		foreach (DataItemParam item in aaa) {
			int iTempGold = 0;
			int iTempExp = 0;

			item.GetCollect (false , out iTempGold , out iTempExp );
			iCollectGold += iTempGold;
			iCollectExp += iTempExp;
		}
		m_iCollectGold = iCollectGold;

		// 支出の計算
		int iShisyutsu = 0;
		List<DataStaffParam> staff_list = DataManager.Instance.dataStaff.Select (" office_serial != 0 ");
		foreach (DataStaffParam staff in staff_list) {
			iShisyutsu += staff.GetPayGold (true);
		}
		if (0 < iShisyutsu) {
			DataManager.Instance.user.AddGold (-1 * iShisyutsu);
		}
		yield return 0;
	}
	public override void Initialize ()
	{
		m_bInitialize = true;
		m_fTimer = 0.0f;
		m_fCheckInterval = 5.0f;
		m_btnCollect.TriggerClear ();
		//StartCoroutine (updateCollect ());
		m_iCollectGold = 0;
		m_iCollectExp = 0;
		//m_iCollectGold = DataManager.Instance.data_kvs.ReadInt (DataManager.Instance.KEY_COLLECT_GOLD);
		//m_iCollectExp =  DataManager.Instance.data_kvs.ReadInt (DataManager.Instance.KEY_COLLECT_EXP);
		SetCollectGold (m_iCollectGold);
	}

	public void SetCollectGold( int _iCollectGold ){
		m_iCollectGold = _iCollectGold;
		m_lbCollectGold.text = string.Format( "{0}G" , _iCollectGold );
	}
	
	// Update is called once per frame
	void Update () {
		if (m_bInitialize == false) {
			return;
		}

		m_fTimer += Time.deltaTime;
		if (m_fCheckInterval < m_fTimer) {
			//StartCoroutine (updateCollect ());
			m_fTimer -= m_fCheckInterval;
		}

		if (m_btnCollect.ButtonPushed) {
			//Debug.LogError ("here");
			m_btnCollect.TriggerClear ();

			//m_iCollectGold = 0;

			int iCollectGold = 0;
			int iCollectExp = 0;
			List<DataItemParam> aaa = DataManager.Instance.m_dataItem.Select (" item_serial != 0 ");
			foreach (DataItemParam item in aaa) {
				int iTempGold = 0;
				int iTempExp = 0;

				item.GetCollect (true ,out iTempGold , out iTempExp );
				iCollectGold += iTempGold;
				iCollectExp += iTempExp;
			}

			//m_iCollectGold = iCollectGold;
			if (0 < m_iCollectGold) {
				SoundManager.Instance.PlaySE ("se_cash", "https://s3-ap-northeast-1.amazonaws.com/every-studio/app/sound/se");

				DataManager.Instance.user.AddCollect ();
				DataManager.Instance.user.AddGold (m_iCollectGold);
				DataManager.Instance.user.AddSyakkin (-1 * m_iCollectGold);
				DataManager.Instance.user.AddExp (m_iCollectExp);
				m_iCollectExp = 0;
				SetCollectGold (0);

				m_eventCollect.Invoke ();


				// ここで仕事のチェックしますか
				List<DataWorkParam> check_work_list = DataManager.Instance.dataWork.Select (" status = 1 ");
				foreach (DataWorkParam work in check_work_list) {
					if (work.ClearCheck ()) {
						work.MissionClear ();
					}
				}
				GoogleAnalytics.Instance.Log (DataManager.Instance.GA_COLLECT_SUCCESS);
			} else {
				GoogleAnalytics.Instance.Log (DataManager.Instance.GA_COLLECT_FAIL);

			}
		}
		return;
	
	}
}












