using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IconFoodElement : MonoBehaviour {

	private Image m_imgIcon;
	[SerializeField]
	private Text m_txtNum;

	public void SetNum( int _iNum ){
		m_txtNum.text = string.Format ("x{0}", _iNum);
	}

	public void Initialize( Define.FOOD_ELEMENT _eElement , int _iNum ){
		m_imgIcon = gameObject.GetComponent<Image> ();
		m_imgIcon.sprite = SpriteManager.Instance.Load (string.Format ("texture/food/icon_food_element{0:D3}", (int)_eElement));
		SetNum (_iNum);
	}
}
