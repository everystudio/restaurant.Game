using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class MapChipRestaurant : MapChipBase<DataMapchipParam> {

	public UnityEvent OnBackyard = new UnityEvent ();
	public UnityEvent OnFix = new UnityEvent ();
	public UnityEvent OnSell = new UnityEvent ();

	//private UtilSwitchSprite m_switchSprite;
	private UI2DSprite m_sprImage;

	private EditMenuButtonRoot m_editMenuButtonRoot;
	private CtrlCharaCheck m_ctrlOjisanCheck;

	private void goBackyard(){
		OnBackyard.Invoke ();
		Destroy (m_ctrlOjisanCheck.gameObject);
		Destroy (gameObject);
	}
	private void cancelBackyard(){
		Destroy (m_ctrlOjisanCheck.gameObject);
		m_ctrlOjisanCheck = null;
	}

	private void pushBackyard(){
		m_ctrlOjisanCheck = PrefabManager.Instance.MakeScript<CtrlCharaCheck>( "prefab/UguiCharaCheck" , GameObject.Find("UIEditMove") );
		m_ctrlOjisanCheck.Initialize ("バックヤードに移動させますか？");
		m_ctrlOjisanCheck.btnYes.onClick.AddListener (goBackyard);
		m_ctrlOjisanCheck.btnNo.onClick.AddListener (cancelBackyard);
	}

	public void SetFlip(){
		//Debug.LogError ("flip");
		if (param.flip == 0) {
			param.flip = 1;
		} else {
			param.flip = 0;
		}
		DispFlip (param.flip);
	}

	public void SetFix(){
		//Debug.LogError ("fix MapChipRestaurant");
		if (m_bSetAble) {
			TweenColorAll (gameObject, 0.025f, Color.white);
			TweenAlphaAll (gameObject, 0.025f, 1.0f);
			Destroy (m_editMenuButtonRoot.gameObject);
			OnFix.Invoke ();
		}
	}

	public void SetSell(){
		//Debug.LogError ("buy");
		OnSell.Invoke ();
	}

	public void DispFlip( int _iFlipParam ){
		if (param.flip == 0) {
			m_sprImage.flip = UIBasicSprite.Flip.Nothing;
		} else {
			m_sprImage.flip = UIBasicSprite.Flip.Horizontally;
		}
	}

	public void ShowEditMenu( bool _bMenu ){
		m_editMenuButtonRoot = PrefabManager.Instance.MakeScript<EditMenuButtonRoot>( "prefab/PrefEditMenuButtonRoot" , gameObject);
		m_editMenuButtonRoot.m_btnBackyard.ClickButtonEvent.AddListener (pushBackyard);
		m_editMenuButtonRoot.m_btnFlip.ClickButtonEvent.AddListener (SetFlip);
		m_editMenuButtonRoot.m_btnFix.ClickButtonEvent.AddListener (SetFix);
		m_editMenuButtonRoot.m_btnBuy.ClickButtonEvent.AddListener (SetSell);
		m_editMenuButtonRoot.transform.localPosition = new Vector3 (0.0f, 200.0f, 0.0f);
	}

	private void change_sprite( UI2DSprite _spr , string _strName ){
		string strLoadImage = string.Format ("texture/item/{0}.png", _strName);
		//Debug.LogError (strLoadImage);
		_spr.sprite2D = SpriteManager.Instance.LoadSprite (strLoadImage);

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
		DispFlip (param.flip);
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


}
