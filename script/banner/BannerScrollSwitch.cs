﻿using UnityEngine;
using System.Collections;

public class BannerScrollSwitch : ButtonManager {

	public enum TYPE
	{
		NONE		= 0,
		COMPLETE	,
		BUY			,
		MAX			,
	}

	#region SerializeField設定
	// 配列かリストにする方がよかったかも
	[SerializeField]
	private UI2DSprite m_sprLeft;
	[SerializeField]
	private UIButton m_btnLeft;

	[SerializeField]
	private UI2DSprite m_sprRight;
	[SerializeField]
	private UIButton m_btnRight;
	#endregion

	public string m_strImageHeader;
	private int m_iSelectingIndex;
	public int SelectingIndex{
		get{ return m_iSelectingIndex; }
	}
	public void SetSelectingIndex( int _iIndex ){
		m_iSelectingIndex = _iIndex;
		return;
	}

	public void Init( string _strHeader , int _iIndex ){
		m_strImageHeader = _strHeader;
		m_iSelectingIndex = 99;
		SetIndex (_iIndex);

		ButtonInit ();

		if (_strHeader.Equals ("") == true) {
			m_btnLeft.gameObject.SetActive (false);
			m_btnRight.gameObject.SetActive (false);
		}

		return;
	}

	public void SetIndex( int _iIndex ){

		if (m_iSelectingIndex == _iIndex) {
			return;
		}

		string strLeft = m_strImageHeader + "1_off";
		string strRight = m_strImageHeader + "2_on";
		if (_iIndex == 0) {
			strLeft = m_strImageHeader + "1_off";
			strRight = m_strImageHeader + "2_on";
		} else {
			strLeft = m_strImageHeader + "1_on";
			strRight = m_strImageHeader + "2_off";
		}

		//Debug.Log (strLeft + " : " + strRight);
		m_sprLeft.sprite2D = SpriteManager.Instance.Load( string.Format( "texture/ui/{0}.png" , strLeft));;
		m_btnLeft.normalSprite = strLeft;
		m_sprRight.sprite2D = SpriteManager.Instance.Load( string.Format( "texture/ui/{0}.png" , strRight));;
		m_btnRight.normalSprite = strRight;
		m_iSelectingIndex = _iIndex;
		return;
	}

	// Update is called once per frame
	new void Update () {

		if (ButtonPushed) {
			SetIndex (Index);
			TriggerClearAll ();
		}
	
	}









}
