using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UtilSwitchSprite))]
public class NativeAdSetSprite : MonoBehaviourEx {

	public string m_strSpriteName;

	private UtilSwitchSprite m_utilSwitchSprite;
	// Use this for initialization
	void Start () {
		m_utilSwitchSprite = GetComponent<UtilSwitchSprite> ();
		m_utilSwitchSprite.SetSprite (m_strSpriteName);
	}
	
}
