using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
public class BtnStaffEmploy : MonoBehaviour {

	public enum EMPLOY_TYPE
	{
		NONE		= 0,
		NORMAL		,
		RARE		,
		RARE11		,
		MAX			,
	}

	[SerializeField]
	private CtrlUserParam m_dispCost;

	[SerializeField]
	private Text m_txtName;

	[SerializeField]
	private Text m_txtRarity;

	[SerializeField]
	private Image m_imgBack;

	private EMPLOY_TYPE m_eEmployType;
	private DataManager.USER_PARAM m_eCostType;
	private int m_iCostNum;

	private void OnClick(){
		if (m_eEmployType != EMPLOY_TYPE.NONE) {
			UIAssistant.main.ShowPage ("WindowStaffNewcomer");

		}
	}

	public void Initialize( EMPLOY_TYPE _eEmployType ){

		// これだけでもいいかも
		m_eEmployType = _eEmployType;

		switch (_eEmployType) {
		case EMPLOY_TYPE.NORMAL:
			m_txtName.text = "一般雇用";
			m_eCostType = DataManager.USER_PARAM.COIN;
			m_iCostNum = 2000;
			m_txtRarity.text = "1-3";
			m_imgBack.color = new Color ((255.0f / 255.0f), (235.0f / 255.0f), (182.0f / 255.0f));
			break;

		case EMPLOY_TYPE.RARE:
			m_txtName.text = "高レベル雇用";
			m_eCostType = DataManager.USER_PARAM.TICKET;
			m_iCostNum = 20;
			m_txtRarity.text = "3-5";
			m_imgBack.color = new Color ((182.0f / 255.0f), 1.0f, 222.0f / 255.0f);
			break;

		case EMPLOY_TYPE.RARE11:
			m_txtName.text = "お得11連雇用";
			m_eCostType = DataManager.USER_PARAM.TICKET;
			m_iCostNum = 200;
			m_txtRarity.text = "3-5";
			m_imgBack.color = new Color ((182.0f / 255.0f), 1.0f, 222.0f / 255.0f);
			break;
		}
		m_dispCost.SetNum (m_eCostType, m_iCostNum);

	}

	void Awake(){
		//Debug.LogError ("awake");
		gameObject.GetComponent<Button> ().onClick.AddListener (OnClick);
	}





}
