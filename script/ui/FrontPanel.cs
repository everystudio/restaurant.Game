using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FrontPanel : Singleton<FrontPanel> {

	public enum STEP
	{
		NONE		= 0,
		IDLE		,
		EDIT		,

		MAX			,
	}
	[SerializeField]
	private STEP m_eStep;
	private STEP m_eStepPre;

	public override void SetDontDestroy (bool _bFlag)
	{
		;
	}


	// Update is called once per frame
	void Update () {
		bool bInit = false;
		if (m_eStepPre != m_eStep) {
			m_eStepPre  = m_eStep;
			bInit = true;
		}
		switch (m_eStep) {
		case STEP.MAX:
		default:
			if (bInit) {
			}
			break;
		}

	}
}
