using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
public class BtnNextPage : MonoBehaviour {

	[SerializeField]
	public string m_strNextPage;
	private Button m_btn;

	// Use this for initialization
	void Start () {
		m_btn = gameObject.GetComponent<Button> ();
		m_btn.onClick.AddListener (
			() => {
				UIAssistant.main.ShowPage( m_strNextPage);
			}
		);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
