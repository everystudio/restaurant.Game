using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
public class StaffTrainingIcon : MonoBehaviour {

	[SerializeField]
	private Image m_imgIcon;

	[SerializeField]
	private CtrlUserParam m_cost;

	private int m_iTrainingType;
	public UnityEventInt OnClickTraining = new UnityEventInt();

	public void OnClick(){
		OnClickTraining.Invoke (m_iTrainingType);
	}

	public void UpdateTrainingType( int _iTrainingType ){

		Button btn = gameObject.GetComponent<Button> ();
		if (_iTrainingType == (int)DataStaff.TRAINING_TYPE.NONE) {
			btn.interactable = true;
		} else if (m_iTrainingType ==_iTrainingType) {
			btn.interactable = true;
		} else {
			btn.interactable = false;
		}

	}

	public void Initialize( DataStaff.TRAINING_TYPE _eTrainingType , DataStaffParam _param ){
		m_iTrainingType = (int)_eTrainingType;
		m_imgIcon.sprite = SpriteManager.Instance.Load (DataStaff.GetIconTraining ((int)_eTrainingType));

		int iNeedNum = 0;
		DataManager.USER_PARAM eUserParam;
		switch (_eTrainingType) {
		case DataStaff.TRAINING_TYPE.BRUSH:
		case DataStaff.TRAINING_TYPE.DUMBBELL:
		case DataStaff.TRAINING_TYPE.SPOON:
			eUserParam = DataManager.USER_PARAM.COIN;
			iNeedNum = 10000;
			break;
		case DataStaff.TRAINING_TYPE.DRESS:
		case DataStaff.TRAINING_TYPE.RIBBON:
		case DataStaff.TRAINING_TYPE.PAN:
			eUserParam = DataManager.USER_PARAM.TICKET;
			iNeedNum = 20;
			break;
		default:
			eUserParam = DataManager.USER_PARAM.NONE;
			break;
		}
		m_cost.SetNum (eUserParam, iNeedNum);

		Button btn = gameObject.GetComponent<Button> ();
		btn.onClick.AddListener (OnClick);

		UpdateTrainingType (m_iTrainingType);
		_param.UpdateTrainingType.AddListener (UpdateTrainingType);
	}

}
