using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapRootRestaurant : MapRootBase<MapChipRestaurant,DataMapchipParam> {

	public void SetLockSwipeMove(bool _bLock){
		m_bLockSwipeMove = _bLock;
	}
	private bool m_bLockSwipeMove = false;
	private bool IsLockSwipeMove(){
		return m_bLockSwipeMove;
	}
	void Update(){

		if ( IsLockSwipeMove() == false && InputManager.Instance.Info.Swipe) {
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
