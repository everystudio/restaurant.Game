using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CtrlShopList : MonoBehaviourEx {

	[SerializeField]
	private GameObject m_objContent;

	[SerializeField]
	private GameObject m_objUserParam;

	private CtrlUserParam m_upCoin;
	private CtrlUserParam m_upTicket;

	private List<GameObject> m_objList = new List<GameObject>();

	public void Initialize ( string _strCategory , string _strType )
	{
		if (m_upCoin != null) {
			Destroy (m_upCoin.gameObject);
			Destroy (m_upTicket.gameObject);
		}

		m_upCoin = PrefabManager.Instance.MakeScript<CtrlUserParam> ("prefab/PrefUserParamLong", m_objUserParam);
		m_upCoin.Initialize (DataManager.USER_PARAM.COIN);
		m_upTicket = PrefabManager.Instance.MakeScript<CtrlUserParam> ("prefab/PrefUserParamLong", m_objUserParam);
		m_upTicket.Initialize (DataManager.USER_PARAM.TICKET);

		string strParam = string.Format ("{0},{1}", _strCategory, _strType);
		ChangeTab (strParam);

	}

	public void ChangeTab( string _strParam ){
		string[] strArr = _strParam.Split (',');
		ChangeTab (strArr [0], strArr [1]); 
	}

	private void ChangeTab(string _strCategory , string _strType){
		if (0 < m_objList.Count) {
			foreach (GameObject obj in m_objList) {
				Destroy (obj);
			}
			m_objList.Clear ();
		}

		foreach (MasterShopParam param in DataManager.Instance.masterShop.list) {
			//Debug.LogError (string.Format ("{0}={1} && {2}={3}", param.category, _strCategory, param.type, _strType));
			if( param.category.Equals( _strCategory ) && param.type.Equals( _strType )){
				string strPrefabName = "prefab/ShopIconTall";
				if (_strCategory.Equals ("food")) {
					strPrefabName = string.Format ("{0}Food", strPrefabName);
				}
				ItemShopIconBase script = PrefabManager.Instance.MakeScript<ItemShopIconBase> (strPrefabName, m_objContent);
				script.Initialize (param);
				m_objList.Add (script.gameObject);
			}
		}
	}

}
