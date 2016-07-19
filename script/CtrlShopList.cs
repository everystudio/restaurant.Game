using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CtrlShopList : MonoBehaviourEx {

	[SerializeField]
	private GameObject m_objContent;

	private List<GameObject> m_objList = new List<GameObject>();

	public void Initialize ( string _strCategory , string _strType )
	{
		if (0 < m_objList.Count) {
			foreach (GameObject obj in m_objList) {
				Destroy (obj);
			}
			m_objList.Clear ();
		}

		foreach (MasterShopParam param in DataManager.Instance.masterShop.list) {
			//Debug.LogError (string.Format ("{0}={1} && {2}={3}", param.category, _strCategory, param.type, _strType));
			if( param.category.Equals( _strCategory ) && param.type.Equals( _strType )){
				EditSelectMapChip script = PrefabManager.Instance.MakeScript<EditSelectMapChip> ("prefab/ShopIconTall", m_objContent);
				m_objList.Add (script.gameObject);
			}
		}
	}

}
