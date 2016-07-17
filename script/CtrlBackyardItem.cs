using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class CtrlBackyardItem : MonoBehaviourEx {

	public void MapChipSelected( int _iMapChipSerial ){
		Debug.LogError( string.Format("selected mapchipserial = {0}" , _iMapChipSerial ));

		UIParam.Instance.m_iEditMapChipSerial = _iMapChipSerial;
		UIAssistant.main.ShowPage( "EditMove" );
	}

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
				script.Selected.AddListener (MapChipSelected);
				m_objList.Add (script.gameObject);
			}
		}
	}

}
