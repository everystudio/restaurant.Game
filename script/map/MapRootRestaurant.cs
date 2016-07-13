using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapRootRestaurant : MapRootBase<MapChipRestaurant,DataMapChipRestaurantParam> {

	public Camera m_setCamera;

	void Start(){

		// 画像読み込み
		SpriteManager.Instance.LoadAtlas ("atlas/ad001");
		SpriteManager.Instance.LoadAtlas ("atlas/back001");
		SpriteManager.Instance.LoadAtlas ("atlas/back002");
		SpriteManager.Instance.LoadAtlas ("atlas/item001");
		SpriteManager.Instance.LoadAtlas ("atlas/item002");
		SpriteManager.Instance.LoadAtlas ("atlas/item003");
		SpriteManager.Instance.LoadAtlas ("atlas/item004");
		SpriteManager.Instance.LoadAtlas ("atlas/item005");
		SpriteManager.Instance.LoadAtlas ("atlas/monster001");
		SpriteManager.Instance.LoadAtlas ("atlas/monster002");
		SpriteManager.Instance.LoadAtlas ("atlas/staff001");
		SpriteManager.Instance.LoadAtlas ("atlas/tutorial001");
		SpriteManager.Instance.LoadAtlas ("atlas/tutorial002");
		SpriteManager.Instance.LoadAtlas ("atlas/tutorial003");
		SpriteManager.Instance.LoadAtlas ("atlas/ui001");
		SpriteManager.Instance.LoadAtlas ("atlas/ui002");
		SpriteManager.Instance.LoadAtlas ("atlas/ui003");


		DataMapChipRestaurant mapchip_sample = new DataMapChipRestaurant ();
		mapchip_sample.Load ("data/mapchip_sample");

		List<DataMapChipRestaurantParam> param_list = mapchip_sample.list;

		Initialize (param_list,"data/mapdata_sample" , m_setCamera );

	}

	void Update(){

		if (InputManager.Instance.Info.TouchUp) {
			InputManager.Instance.Info.TouchUp = false;
			int iGridX = 0;
			int iGridY = 0;

			if (GetGrid (InputManager.Instance.Info.TouchPoint, out iGridX, out iGridY)) {
				Debug.Log (string.Format ("grid({0},{1})", iGridX, iGridY));

			} else {
				Debug.Log ("no hit");
			}
		}


		if (InputManager.Instance.Info.Swipe) {

			myTransform.localPosition += new Vector3 (InputManager.Instance.Info.SwipeAdd.x, InputManager.Instance.Info.SwipeAdd.y, 0.0f);

			float fMaxX = map_data.GetWidth() * map_data.CELL_X_DIR.x;
			float fMinX = fMaxX * -1.0f;

			if (myTransform.localPosition.x < fMinX) {
				myTransform.localPosition = new Vector3 (fMinX, myTransform.localPosition.y, myTransform.localPosition.z);
			} else if (fMaxX < myTransform.localPosition.x) {
				myTransform.localPosition = new Vector3 (fMaxX, myTransform.localPosition.y, myTransform.localPosition.z);
			} else {
			}
			float fMaxY = 0.0f;
			float fMinY = map_data.GetHeight()*2 * map_data.CELL_X_DIR.y * -1.0f;
			if (myTransform.localPosition.y < fMinY) {
				myTransform.localPosition = new Vector3 (myTransform.localPosition.x, fMinY , myTransform.localPosition.z);
			} else if (fMaxY < myTransform.localPosition.y) {
				myTransform.localPosition = new Vector3 (myTransform.localPosition.x, fMaxY , myTransform.localPosition.z);
			} else {
			}

			//m_eStep = STEP.SWIPE;
		}



	}



}
