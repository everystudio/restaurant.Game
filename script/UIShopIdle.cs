using UnityEngine;
using System.Collections;

public class UIShopIdle : CPanel {

	[SerializeField]
	private CtrlShopList m_ctrlShopList;

	public string m_strDefaultCategory;
	public string m_strDefaultType;

	public string m_strChangeParamPre;

	protected override void panelStart ()
	{
		base.panelStart ();

		m_strDefaultCategory = "furniture";
		m_strDefaultType = "table";
		m_ctrlShopList.Initialize (m_strDefaultCategory, m_strDefaultType);

		m_strChangeParamPre = string.Format ("{0},{1}", m_strDefaultCategory, m_strDefaultType);
	}

	public void OnTabChange( string _strParam ){
		if (m_strChangeParamPre.Equals (_strParam)) {
		} else {
			m_ctrlShopList.ChangeTab (_strParam);
			m_strChangeParamPre = _strParam;
		}
	}

}
