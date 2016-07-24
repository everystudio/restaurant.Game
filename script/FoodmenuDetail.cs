using UnityEngine;
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

	private void OnPushedProduce(){
	}
	private void OnPushedRegister(){
	}

	public void Initialize( MasterFoodmenuParam _FoodmenuParam ){

		m_txtMenuName.text = _FoodmenuParam.name;
		m_imgMemuIcon.sprite = SpriteManager.Instance.Load (MasterFoodmenu.GetIconFilename (_FoodmenuParam.foodmenu_id));
		m_ctrlPrice.SetNum (DataManager.USER_PARAM.COIN, _FoodmenuParam.coin);

		m_foodElementVegetable.Initialize (Define.FOOD_ELEMENT.VEGETABLE, _FoodmenuParam.vegetable);
		m_foodElementMeet.Initialize (Define.FOOD_ELEMENT.MEAT, _FoodmenuParam.meat);
		m_foodElementFish.Initialize (Define.FOOD_ELEMENT.FISH, _FoodmenuParam.fish);
		m_foodElementSeasoning.Initialize (Define.FOOD_ELEMENT.SEASONING, _FoodmenuParam.seasoning);

		m_ctrlRareStars.Initialize (_FoodmenuParam.rarity);

		m_btnFoodProduce.onClick.AddListener (OnPushedProduce);
		m_btnFoodRegister.onClick.AddListener (OnPushedRegister);

	}

	public void Initialize( int _foodmenuId ){
		Initialize (DataManager.Instance.masterFoodmenu.Get (_foodmenuId));
	}


}
