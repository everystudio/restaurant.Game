using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

abstract public class ItemShopIconBase : MonoBehaviour {

	protected int m_iSetNum;
	protected MasterShopParam m_masterShopParam;
	protected MasterMapchipParam m_masterMapchipParam;
	[SerializeField]
	protected Button m_btnPurchase;

	public void Initialize (MasterShopParam _param){
		m_iSetNum = 1;
		m_masterShopParam = _param;
		m_masterMapchipParam = DataManager.Instance.masterMapchip.Get (_param.item_id);
		m_btnPurchase.onClick.AddListener (Purchase);
		initialize (m_masterShopParam, m_masterMapchipParam);


	}
	abstract protected void initialize (MasterShopParam _paramShop , MasterMapchipParam _paramMaphip);
	abstract protected bool purchased ();

	public void Purchase(){
		//Debug.LogError (string.Format ("purchase:item_id={0}", m_masterShopParam.item_id));
		if (purchased () == false) {
			Debug.LogError ("error purchase");
		}
	}

}
