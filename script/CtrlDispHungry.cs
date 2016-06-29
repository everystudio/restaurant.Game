using UnityEngine;
using System.Collections;

public class CtrlDispHungry : MonoBehaviour {

	[SerializeField]
	private UI2DSprite[] m_sprTripeArr;

	public void Set( float _iMealLevel ){
		for (int i = 0; i < m_sprTripeArr.Length; i++) {
			if ( _iMealLevel < (i + 1) ) {
				m_sprTripeArr [i].sprite2D = SpriteManager.Instance.Load( string.Format( "texture/ui/{0}.png" , "icon_manpuku3"));
			}
		}
	}

}
