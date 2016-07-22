using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
public class BtnSubmenu : MonoBehaviourEx {

	private bool m_bIsOpen = false;

	private float m_fWidth;

	[SerializeField]
	private RectTransform m_rtSubmenuRoot;

	private Button m_btn;

	[SerializeField]
	private float m_fButtonMoveTime = 0.1f;

	public void OnClick(){
		Vector3 targetPos;
		if (m_bIsOpen) {
			targetPos = new Vector3 (m_fWidth, 0.0f, 0.0f);
		} else {
			targetPos = Vector3.zero;
		}
		TweenPosition.Begin (m_rtSubmenuRoot.gameObject, m_fButtonMoveTime, targetPos);
		m_bIsOpen = !m_bIsOpen;
		return;
	}

	void Start () {
		m_btn = gameObject.GetComponent<Button> ();
		m_bIsOpen = false;
		m_fWidth = m_rtSubmenuRoot.rect.width;

		m_rtSubmenuRoot.localPosition = new Vector3(m_fWidth, 0.0f,0.0f);
		m_btn.onClick.AddListener (OnClick);

		/*
		m_btn.onClick.AddListener (
			() => {
				Vector3 targetPos;
				if (m_bIsOpen) {
					targetPos = new Vector3 (m_fWidth, 0.0f, 0.0f);
				} else {
					targetPos = Vector3.zero;
				}
				TweenPosition.Begin (m_rtSubmenuRoot.gameObject, 0.5f, targetPos);
				m_bIsOpen = !m_bIsOpen;
			}
		);
		*/


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
