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
	protected override void initialize (MasterShopParam _paramShop , MasterMapchipParam _paramMaphip)
	{
		m_txtName.text = _paramShop.name;

		if (0 < _paramMaphip.capacity) {
			int iNum = 0;
			m_txtBackyardNum.text = iNum.ToString ();
			m_txtCapacity.text = string.Format ("{0}人", _paramMaphip.capacity.ToString ());
		} else {
			m_txtBackyardNum.text = "";
			m_txtCapacity.text = "";
		}

		m_txtComfortable.text = _paramMaphip.comfortable.ToString ();
		m_txtDecoration.text = _paramMaphip.decoration.ToString ();

		m_slComfortable.maxValue = 100.0f;
		m_slComfortable.value = (float)_paramMaphip.comfortable;

		m_slDecoration.maxValue = 100.0f;
		m_slDecoration.value = (float)_paramMaphip.decoration;

	}

	protected override void purchased ()
	{
		return;
	}




}



