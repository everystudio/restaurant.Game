using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CtrlRareStars : MonoBehaviour {

	[SerializeField]
	private Image[] m_imgStarArr;

	public void Initialize( int _iStarNum ){

		for (int i = 0; i < m_imgStarArr.Length; i++) {
			string strFilename = "";
			if (i < _iStarNum) {
				strFilename = "texture/ui/star_fill";
			} else {
				strFilename = "texture/ui/star_empty";
			}
			m_imgStarArr [i].sprite = SpriteManager.Instance.LoadSprite (strFilename);
		}
	}


}
