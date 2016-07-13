using UnityEngine;
using System.Collections;

public class MapChipRestaurant : MapChipBase<DataMapChipRestaurantParam> {

	//private UtilSwitchSprite m_switchSprite;
	private UI2DSprite m_sprImage;

	private void change_sprite( UI2DSprite _spr , string _strName ){
		string strLoadImage = string.Format ("texture/item/{0}.png", _strName);
		//Debug.LogError (strLoadImage);
		_spr.sprite2D = SpriteManager.Instance.Load (strLoadImage);

		_spr.width =  (int)_spr.sprite2D.rect.width;
		_spr.height = (int)_spr.sprite2D.rect.height;

	}

	protected override void initialize (int _x, int _y, int _item_id)
	{
		m_sprImage = gameObject.AddComponent<UI2DSprite> ();
		m_sprImage.pivot = UIWidget.Pivot.Bottom;
		//m_switchSprite = gameObject.AddComponent<UtilSwitchSprite> ();

		string strName = "item" + string.Format( "{0:D2}_{1:D2}" , _item_id , 1 );
		change_sprite (m_sprImage, strName);
		SetPos (_x, _y);
	}

	public void SetPos( int _iX , int _iY ){

		//Debug.LogError (string.Format ("x={0} y={1}", _iX, _iY));
		myTransform.localPosition = (m_mapData.CELL_X_DIR.normalized * m_mapData.CELL_X_LENGTH * _iX) + (m_mapData.CELL_Y_DIR.normalized * m_mapData.CELL_Y_LENGTH * _iY);

		int iOffset = param.width - 1;
		if (iOffset < 0) {
			iOffset = 0;
		}

		int iDepth = 100 - (_iX + _iY) - iOffset;// + (m_dataItemParam.height-1));

		// 道路は一番した
		if (param.item_id == 0) {
			iDepth += m_mapData.DEPTH_ROAD-1000;
		}
		else if (param.item_id == -1 ) {
			iDepth += m_mapData.DEPTH_ROAD-500;
		}
		else if( IsRoad() ){
			iDepth += m_mapData.DEPTH_ROAD;
		} else {
			iDepth += m_mapData.DEPTH_ITEM;
		}
		/*
		if (m_bEditting) {
			iDepth += 10;		// こんだけ上なら
		}
		*/
		m_sprImage.depth = iDepth;
		return;
	}

	public void SetEditAble( bool _bFlag ){
		if (_bFlag) {
			TweenColorAll (gameObject, 0.025f, Color.green);
			TweenAlphaAll (gameObject, 0.025f, 0.75f);
		} else {
			TweenColorAll (gameObject, 0.025f, Color.red);
			TweenAlphaAll (gameObject, 0.025f, 0.75f);
		}
	}


}
