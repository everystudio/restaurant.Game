﻿using UnityEngine;
using System.Collections;

public class CtrlIconFukidashi : MonoBehaviour {

	public enum STATUS {
		NONE		= 0,
		HUNGRY		,
		DUST		,
		SICK1		,
		SICK2		,
		MAX			,
	}

	[SerializeField]
	private UI2DSprite m_sprIcon;

	public STATUS m_eStatus;
	public STATUS m_eStatusPre;	// いらないかも

	public void SetStatus( STATUS _eStatus , int _iDepth ){

		if (m_eStatus != _eStatus) {
			change (_eStatus);
			m_eStatus = _eStatus;
		}
		m_sprIcon.depth = _iDepth - DataManager.Instance.DEPTH_MONSTER + DataManager.Instance.DEPTH_MONSTER_FUKIDASHI;
		return;
	}

	private void change( STATUS _eStatus ){
		string strName = "";
		bool bDisp = true;
		switch (_eStatus) {

		case STATUS.HUNGRY:
			strName = "icon_3";
			break;
		case STATUS.DUST:
			strName = "icon_2";
			break;

		case STATUS.SICK1:
			strName = "icon_-1";
			break;
		case STATUS.SICK2:
			strName = "icon_-1";
			break;
		default:
			bDisp = false;
			break;
		}

		m_sprIcon.sprite2D = SpriteManager.Instance.Load (string.Format ("texture/ui/{0}.png", strName));
		m_sprIcon.enabled = bDisp;

	}

	// Update is called once per frame
	void Update () {
	
	}
}
