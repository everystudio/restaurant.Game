using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class CtrlBackyardItem : MonoBehaviourEx {

	[SerializeField]
	private GameObject m_objContent;

	private List<GameObject> m_objList = new List<GameObject>();

	public void Initialize ()
	{
		List<DataMapChipRestaurantParam> list = DataManager.Instance.dataMapChipRestaurant.list;

		foreach (DataMapChipRestaurantParam param in list) {
			if (param.x < 0) {
				EditSelectMapChip script = PrefabManager.Instance.MakeScript<EditSelectMapChip> ("prefab/EditSelectMapchip", m_objContent);
				script.Initialize (param);
				m_objList.Add (script.gameObject);
			}
		}
	}

}
