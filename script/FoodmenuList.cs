using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FoodmenuList : MonoBehaviour {

	[SerializeField]
	private FoodmenuDetail m_ctrlFoodmenuDetail;

	[SerializeField]
	private GameObject m_goContentsBannerRoot;

	private List<FoodmenuBanner> m_scriptFoodmenuBanner= new List<FoodmenuBanner>();

	private string m_strPreType = "";

	[SerializeField]
	private Button m_btnAll;
	[SerializeField]
	private Button m_btnRegisterd;
	[SerializeField]
	private Button m_btnUnproduced;

	public enum FOODMENU_LIST_TYPE
	{
		NONE		= 0,
		ALL			,
		REGISTERD	,
		UNPRODUCED	,
		MAX			,
	}

	private void bannerSelecte( int _foodmenuId ){
		m_ctrlFoodmenuDetail.Initialize (_foodmenuId);
	}

	private void refreshList(string _strType){

		if (m_strPreType.Equals (_strType)) {
			return;
		} else {
			m_strPreType = _strType;
		}

		FOODMENU_LIST_TYPE eType = FOODMENU_LIST_TYPE.NONE;
		switch (_strType) {
		case "all":
			eType = FOODMENU_LIST_TYPE.ALL;
			break;
		case "registerd":
			eType = FOODMENU_LIST_TYPE.REGISTERD;
			break;
		case "unproduced":
			eType = FOODMENU_LIST_TYPE.UNPRODUCED;
			break;
		default:
			break;
		}

		foreach (FoodmenuBanner script in m_scriptFoodmenuBanner) {
			Destroy (script.gameObject);
		}
		m_scriptFoodmenuBanner.Clear ();

		int iFirstFoodmenuId = 0;
		foreach (MasterFoodmenuParam param in DataManager.Instance.masterFoodmenu.list) {
			bool bEntry = false;
			switch (eType) {
			case FOODMENU_LIST_TYPE.ALL:
				bEntry = true;
				break;
			case FOODMENU_LIST_TYPE.REGISTERD:
				bEntry = DataManager.Instance.dataFoodmenu.IsRegisterd (param.foodmenu_id);
				break;
			case FOODMENU_LIST_TYPE.UNPRODUCED:
					// ここ、結果を反転してますのでご注意
				bEntry = !DataManager.Instance.dataFoodmenu.IsProduced (param.foodmenu_id);
				break;
			default:
				break;
			}
			if (bEntry) {
				FoodmenuBanner script = PrefabManager.Instance.MakeScript<FoodmenuBanner> ("prefab/PrefFoodmenuBanner", m_goContentsBannerRoot);
				script.Initialize (param);
				script.OnSelect.AddListener (bannerSelecte);

				if (iFirstFoodmenuId == 0) {
					iFirstFoodmenuId = param.foodmenu_id;
				}
				m_scriptFoodmenuBanner.Add (script);
			}
		}
		bannerSelecte (iFirstFoodmenuId);
	}

	private void refreshListAll(){
		refreshList ("all");
	}
	private void refreshListRegisterd(){
		refreshList ("registerd");
	}
	private void refreshListUnproduced(){
		refreshList ("unproduced");
	}
	void Start(){
		m_strPreType = "";
		m_btnAll.onClick.AddListener (refreshListAll );
		m_btnRegisterd.onClick.AddListener (refreshListRegisterd );
		m_btnUnproduced.onClick.AddListener (refreshListUnproduced );
	}

	public void Initialize(){
		refreshListAll ();
	}



}






