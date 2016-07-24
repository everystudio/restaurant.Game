using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
public class FoodmenuBanner : MonoBehaviour {

	public UnityEventInt OnSelect = new UnityEventInt();

	[SerializeField]
	private Text m_txtName;

	[SerializeField]
	private Image m_imgIcon;

	[SerializeField]
	private CtrlRareStars m_ctrlRareStars;

	[SerializeField]
	private CtrlUserParam m_ctrlPrice;

	private MasterFoodmenuParam m_masterFoodmenuParam;


	public void Initialize( MasterFoodmenuParam _param ){
		m_masterFoodmenuParam = _param;
		m_imgIcon.sprite = SpriteManager.Instance.Load (MasterFoodmenu.GetIconFilename (_param.foodmenu_id));
		m_txtName.text = _param.name;
		m_ctrlPrice.SetNum (DataManager.USER_PARAM.COIN, _param.coin);
		m_ctrlRareStars.Initialize (_param.rarity);

		gameObject.GetComponent<Button> ().onClick.AddListener (
			()=>{
				OnSelect.Invoke(m_masterFoodmenuParam.foodmenu_id);
			}
		);
	}


}
