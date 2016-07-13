using UnityEngine;
using System.Collections;

public class RestaurantIdle : RestaurantPageBase {

	public enum STEP{
		NONE		= 0,
		IDLE		,
		END			,
		MAX			,
	}
	public STEP m_eStep;
	public STEP m_eStepPre;

	[SerializeField]
	private ButtonBase m_btnMenuEdit;


	protected override void initialize ()
	{
		m_eStep = STEP.NONE;
		m_eStepPre = STEP.MAX;
		m_btnMenuEdit.TriggerClear ();
	}

	protected override void pageStart ()
	{
		m_eStep = STEP.IDLE;
		m_eStepPre = STEP.MAX;
	}

	protected override void pageEnd ()
	{
		;
	}
	public override bool IsEnd ()
	{
		if (m_eStep == STEP.END) {
			return true;
		}
		return false;
	}

	void Update(){
		bool bInit = false;
		if (m_eStepPre != m_eStep) {
			m_eStepPre  = m_eStep;
			bInit = true;
		}
		switch (m_eStep) {

		case STEP.IDLE:
			if (bInit) {
				m_btnMenuEdit.TriggerClear ();
			}
			if (m_btnMenuEdit.ButtonPushed) {
				m_eStep = STEP.END;
			}
			break;

		case STEP.END:
			break;

		case STEP.MAX:
		default:
			break;
		}

	}
}
