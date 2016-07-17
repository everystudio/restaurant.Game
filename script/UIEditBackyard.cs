using UnityEngine;
using System.Collections;

public class UIEditBackyard : CPanel {

	private CtrlBackyardItem m_ctrlBackyardItem;

	protected override void panelStart ()
	{
		m_ctrlBackyardItem = PrefabManager.Instance.MakeScript<CtrlBackyardItem> ("prefab/PrefBackyardItem", gameObject);
		m_ctrlBackyardItem.Initialize ();
	}

	protected override void panelEndStart ()
	{
		if (m_ctrlBackyardItem != null) {
			Destroy (m_ctrlBackyardItem.gameObject);
			m_ctrlBackyardItem = null;
		}
	}

}
