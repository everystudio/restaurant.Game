using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class ItemShopIcon : ItemShopIconBase {

	[SerializeField]
	private Image m_imgItem;

	[SerializeField]
	private Text m_txtName;
	[SerializeField]
	private Text m_txtBackyardNum;
	[SerializeField]
	private Text m_txtCapacity;

	[SerializeField]
	private Text m_txtComfortable;
	[SerializeField]
	private Text m_txtDecoration;

	[SerializeField]
	private Slider m_slComfortable;
	[SerializeField]
	private Slider m_slDecoration;

	[SerializeField]
	private CtrlUserParam m_ctrlUserParam;


	protected override void initialize (MasterShopParam _paramShop , MasterMapchipParam _paramMaphip)
	{
		m_txtName.text = _paramShop.name;

		DataManager.USER_PARAM eUserParam = DataManager.USER_PARAM.NONE;
		switch (_paramShop.need_type) {
		case "coin":
			eUserParam = DataManager.USER_PARAM.COIN;
			break;
		case "ticket":
			eUserParam = DataManager.USER_PARAM.TICKET;
			break;
		case "money":
			eUserParam = DataManager.USER_PARAM.MONEY;
			break;
		default:
			Debug.LogError ("unknown type");
			break;
		}
		m_ctrlUserParam.SetNum (eUserParam,_paramShop.need_num);

		if (0 < _paramMaphip.capacity) {
			int iNum = 0;
			m_txtBackyardNum.text = iNum.ToString ();
			m_txtCapacity.text = string.Format ("{0}人", _paramMaphip.capacity.ToString ());
		} else {
			m_txtBackyardNum.text = "";
			m_txtCapacity.text = "";
		}
		/*
		m_txtComfortable.text = _paramMaphip.comfortable.ToString ();
		m_txtDecoration.text = _paramMaphip.decoration.ToString ();
		*/

		m_slComfortable.maxValue = 100.0f;
		m_slComfortable.value = (float)_paramMaphip.comfortable;

		m_slDecoration.maxValue = 100.0f;
		m_slDecoration.value = (float)_paramMaphip.decoration;

	}

	protected override bool purchased ()
	{
		bool bRet = false;

		int iRequire = m_masterShopParam.need_num * m_iSetNum;

		switch (m_masterShopParam.need_type) {
		case "coin":
			if (iRequire <= DataManager.Instance.user.coin) {
				bRet = true;
				DataManager.Instance.user.coin -= iRequire;


			}
			break;
		case "ticket":
			break;
		case "money":
			break;
		default:
			Debug.LogError ("unknown type");
			break;
		}


		return bRet;
	}




}



