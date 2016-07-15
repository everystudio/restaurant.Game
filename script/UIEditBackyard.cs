using UnityEngine;
using System.Collections;

public class UIEditBackyard : MonoBehaviour {

	private CtrlBackyardItem m_ctrlBackyardItem;

	void OnEnable(){
		m_ctrlBackyardItem = PrefabManager.Instance.MakeScript<CtrlBackyardItem> ("prefab/PrefBackyardItem", gameObject);
		m_ctrlBackyardItem.Initialize ();
	}

	void OnDisable(){
		Destroy (m_ctrlBackyardItem.gameObject);
	}

}
