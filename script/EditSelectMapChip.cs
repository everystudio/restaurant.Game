using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class EditSelectMapChip : MonoBehaviourEx {

	public UnityEventInt Selected = new UnityEventInt ();

	[SerializeField]
	private Button m_btn;

	[SerializeField]
	private Image m_imgItem;

	[SerializeField]
	private Text m_txtCapacity;
	[SerializeField]
	private Text m_txtNum;

	DataMapchipParam m_param;

	public void Initialize( DataMapchipParam _param ){
		m_param = _param;
		m_txtCapacity.text = "";
		m_txtNum.text = "";

		m_imgItem.sprite = SpriteManager.Instance.LoadSprite (DataManager.Instance.MakeItemSpriteNameLoad (m_param.item_id));

		m_btn.onClick.AddListener (OnSelected);
	}

	public void OnSelected(){
		Selected.Invoke (m_param.mapchip_serial);
	}
}
