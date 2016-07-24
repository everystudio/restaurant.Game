using UnityEngine;
using System.Collections;

public class WindowFoodMenu : CPanel {

	[SerializeField]
	private FoodmenuList m_foodmenuList;

	protected override void panelStart ()
	{
		base.panelStart ();
		m_foodmenuList.Initialize ();
	}

}
