using UnityEngine;
using System.Collections;

public class EditMenuButtonRoot : MonoBehaviour {

	public ButtonBase m_btnBackyard;
	public ButtonBase m_btnFlip;
	public ButtonBase m_btnFix;
	public ButtonBase m_btnBuy;

	public void Initialize(){
		m_btnBackyard.TriggerClear ();
		m_btnFlip.TriggerClear ();
		m_btnFix.TriggerClear ();
		m_btnBuy.TriggerClear ();
	}
}
