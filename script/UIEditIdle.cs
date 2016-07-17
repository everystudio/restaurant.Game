using UnityEngine;
using System.Collections;

public class UIEditIdle : CPanel {

	private DataMapChipRestaurantParam m_paramMove;

	private MapRootRestaurant mapRoot {
		get{
			return RestaurantMain.Instance.mapRoot;
		}
	}

	protected override void panelStart ()
	{
		//Debug.LogError ("UIEditIdle:panelStart");
		InputManager.Instance.Info.TouchUp = false;

	}

	protected override void panelEndStart ()
	{
		//Debug.LogError ("UIEditIdle:panelEnd");
	}

	void Update(){
		if (InputManager.Instance.Info.TouchUp) {
			InputManager.Instance.Info.TouchUp = false;
			int iGridX = 0;
			int iGridY = 0;

			if (mapRoot.GetGrid (InputManager.Instance.Info.TouchPoint, out iGridX, out iGridY)) {
				Debug.Log (string.Format ("grid({0},{1})", iGridX, iGridY));

				if (DataManager.Instance.dataMapChipRestaurant.GetExist (iGridX, iGridY, out m_paramMove)) {
					UIParam.Instance.m_iEditMapChipSerial = m_paramMove.mapchip_serial;
					UIAssistant.main.ShowPage ("EditMove");
				}

			} else {
				Debug.Log ("no hit");
			}
		}	}


}


