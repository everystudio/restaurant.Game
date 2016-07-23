using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class ItemShopIconFood : ItemShopIconBase {

	[SerializeField]
	private Image m_imgItem;

	[SerializeField]
	private Text m_txtName;
	[SerializeField]
	private Text m_txtNum;
	[SerializeField]
	private Text m_txtMax;

	[SerializeField]
	private Text m_txtSetNum;
	[SerializeField]
	private Text m_txtPrice;

	[SerializeField]
	private Slider m_slValue;

	protected void ChangeSetNum(){
		if (m_iSetNum == 10) {
			m_iSetNum = 50;
		} else if (m_iSetNum == 50) {
			m_iSetNum = 100;
		} else {
			m_iSetNum = 10;
		}

		m_txtSetNum.text = string.Format ("{0}個セット", m_iSetNum);
		m_txtPrice.text = string.Format ("{0}G", m_iSetNum * m_masterShopParam.need_num);
	}

	protected override void initialize (MasterShopParam _paramShop , MasterMapchipParam _paramMaphip){
		m_iSetNum = 0;
		// 好きじゃないけど中でmember変数をinitialzie
		ChangeSetNum();
		gameObject.GetComponent<Button> ().onClick.AddListener (ChangeSetNum);
		m_txtName.text = _paramShop.name;
		m_slValue.maxValue = 999.0f;
		SetNum (0);
	}

	protected override bool purchased ()
	{
		return false;
	}

	public void SetNum( int _iNum ){
		m_txtNum.text = string.Format ("数量:{0}", _iNum);
		m_slValue.value = (float)_iNum;
	}




}



