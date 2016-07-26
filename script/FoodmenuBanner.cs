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
	private Image m_imgBack;

	[SerializeField]
	private CtrlRareStars m_ctrlRareStars;

	[SerializeField]
	private CtrlUserParam m_ctrlPrice;

	private MasterFoodmenuParam m_masterFoodmenuParam;

	public void Refresh(){
		if (DataManager.Instance.dataFoodmenu.IsProduced (m_masterFoodmenuParam.foodmenu_id) == false) {
			m_imgBack.color = new Color (0.5f, 0.5f, 0.5f);
		} else {
			m_imgBack.color = new Color (1.0f, 1.0f, 1.0f);
		}
		return;
	}

	public void Initialize( MasterFoodmenuParam _param ){
		m_masterFoodmenuParam = _param;
		m_imgIcon.sprite = SpriteManager.Instance.Load (MasterFoodmenu.GetIconFilename (_param.foodmenu_id));
		m_txtName.text = _param.name;
		m_ctrlPrice.SetNum (DataManager.USER_PARAM.COIN, _param.coin);
		m_ctrlRareStars.Initialize (_param.rarity);

		Refresh ();

		gameObject.GetComponent<Button> ().onClick.AddListener (
			()=>{
				OnSelect.Invoke(m_masterFoodmenuParam.foodmenu_id);
			}
		);
	}


}
