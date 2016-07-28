using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class FoodmenuDetail : MonoBehaviour {

	[SerializeField]
	private Text m_txtMenuName;

	[SerializeField]
	private Image m_imgMemuIcon;

	[SerializeField]
	private CtrlUserParam m_ctrlPrice;

	[SerializeField]
	private IconFoodElement m_foodElementVegetable;
	[SerializeField]
	private IconFoodElement m_foodElementMeet;
	[SerializeField]
	private IconFoodElement m_foodElementFish;
	[SerializeField]
	private IconFoodElement m_foodElementSeasoning;

	[SerializeField]
	private CtrlRareStars m_ctrlRareStars;

	[SerializeField]
	private Button m_btnFoodProduce;
	[SerializeField]
	private Button m_btnFoodRegister;

	[SerializeField]
	private Image m_imgBtnFoodProduce;
	[SerializeField]
	private Image m_imgBtnFoodRegister;

	private MasterFoodmenuParam m_masterFoodmenu;
	public CtrlCharaCheck m_ctrlOjisanCheck;

	public UnityEvent RefreshBanner = new UnityEvent();

	private void Refresh(){
		if (DataManager.Instance.dataFoodmenu.IsProduced (m_masterFoodmenu.foodmenu_id)) {
			m_btnFoodRegister.interactable = true;
			if (DataManager.Instance.dataFoodmenu.IsRegisterd (m_masterFoodmenu.foodmenu_id)) {
				m_imgBtnFoodRegister.sprite = SpriteManager.Instance.Load ("texture/food/btn_food_cancel");
			} else {
				m_imgBtnFoodRegister.sprite = SpriteManager.Instance.Load ("texture/food/btn_food_register");
			}

		} else {
			m_btnFoodRegister.interactable = false;
		}
	}
	private void OnProduce(){

		m_ctrlOjisanCheck.SelfDestroy ();

		DataManager.Instance.dataFoodmenu.Produce (m_masterFoodmenu.foodmenu_id);
		RefreshBanner.Invoke ();
		Refresh();

	}
	private void OnRegister(){
	}


	private void OnPushedProduce(){

		m_ctrlOjisanCheck = PrefabManager.Instance.MakeScript<CtrlCharaCheck> ("prefab/UguiCharaCheck", gameObject.transform.parent.gameObject);

		string strMessage = "";
		if (DataManager.Instance.dataFoodmenu.IsProduced (m_masterFoodmenu.foodmenu_id) == false) {
			strMessage = "このメニューを新規開発しますか？";
		} else {
			strMessage = "このメニューを改良しますか？";
		}
		m_ctrlOjisanCheck.Initialize (strMessage);
		m_ctrlOjisanCheck.btnYes.onClick.AddListener (OnProduce);
		m_ctrlOjisanCheck.btnNo.onClick.AddListener (OnRegister);

	}


	private void OnPushedRegister(){
		DataFoodmenuParam param = DataManager.Instance.dataFoodmenu.Get (m_masterFoodmenu.foodmenu_id);

		if (DataManager.Instance.dataFoodmenu.IsRegisterd (m_masterFoodmenu.foodmenu_id)) {
			param.status = (int)DataFoodmenuParam.STATUS.PRODUCED;
		} else {
			param.status = (int)DataFoodmenuParam.STATUS.REGISTERD;
		}
		Refresh ();
		RefreshBanner.Invoke ();
	}

	public void Initialize( MasterFoodmenuParam _FoodmenuParam ){
		m_masterFoodmenu = _FoodmenuParam;
		m_txtMenuName.text = _FoodmenuParam.name;
		m_imgMemuIcon.sprite = SpriteManager.Instance.Load (MasterFoodmenu.GetIconFilename (_FoodmenuParam.foodmenu_id));
		m_ctrlPrice.SetNum (DataManager.USER_PARAM.COIN, _FoodmenuParam.coin);

		m_foodElementVegetable.Initialize (Define.FOOD_ELEMENT.VEGETABLE, _FoodmenuParam.vegetable);
		m_foodElementMeet.Initialize (Define.FOOD_ELEMENT.MEAT, _FoodmenuParam.meat);
		m_foodElementFish.Initialize (Define.FOOD_ELEMENT.FISH, _FoodmenuParam.fish);
		m_foodElementSeasoning.Initialize (Define.FOOD_ELEMENT.SEASONING, _FoodmenuParam.seasoning);

		m_ctrlRareStars.Initialize (_FoodmenuParam.rarity);

		Refresh ();

	}


	public void Initialize( int _foodmenuId ){
		Initialize (DataManager.Instance.masterFoodmenu.Get (_foodmenuId));
	}

	public void Awake(){
		m_btnFoodProduce.onClick.AddListener (OnPushedProduce);
		m_btnFoodRegister.onClick.AddListener (OnPushedRegister);
	}


}
