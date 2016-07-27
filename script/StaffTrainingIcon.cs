using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
public class StaffTrainingIcon : MonoBehaviour {

	[SerializeField]
	private Image m_imgIcon;

	[SerializeField]
	private CtrlUserParam m_cost;

	public void Initialize( DataStaff.TRAINING _eTrainingType , DataStaffParam _param ){

		m_imgIcon.sprite = SpriteManager.Instance.Load (string.Format ("texture/staff/staff_train_{0:D2}", (int)_eTrainingType));

		int iNeedNum = 0;
		DataManager.USER_PARAM eUserParam;
		switch (_eTrainingType) {
		case DataStaff.TRAINING.BRUSH:
		case DataStaff.TRAINING.DUMBBELL:
		case DataStaff.TRAINING.SPOON:
			eUserParam = DataManager.USER_PARAM.COIN;
			iNeedNum = 10000;
			break;
		case DataStaff.TRAINING.DRESS:
		case DataStaff.TRAINING.RIBBON:
		case DataStaff.TRAINING.PAN:
			eUserParam = DataManager.USER_PARAM.TICKET;
			iNeedNum = 20;
			break;
		default:
			eUserParam = DataManager.USER_PARAM.NONE;
			break;
		}
		m_cost.SetNum (eUserParam, iNeedNum);

		Button btn = gameObject.GetComponent<Button> ();
		if (_param.training_type == (int)DataStaff.TRAINING.NONE) {
			btn.interactable = true;
		} else if (_param.training_type == (int)_eTrainingType) {
			btn.interactable = true;
		} else {
			btn.interactable = false;
		}

	}

}
