using UnityEngine;
using System.Collections;

public class UIMainHeader : CPanel {

	[SerializeField]
	private CtrlUserParam m_upCoin;
	[SerializeField]
	private CtrlUserParam m_upTicket;
	[SerializeField]
	private CtrlUserParam m_upPopularity;

	// Use this for initialization
	void Start () {
		m_upCoin.Initialize (DataManager.USER_PARAM.COIN);
		m_upTicket.Initialize (DataManager.USER_PARAM.TICKET);
		m_upPopularity.Initialize (DataManager.USER_PARAM.POPULARITY);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
