using UnityEngine;
using System.Collections;

public class CtrlIconStaff : CtrlIconBase {

	public void Initialize( UI2DSprite _sprite , DataStaffParam _dataStaff , int _iSize ){
		SetSize (_iSize);
		myTransform.localPosition = GetMovePos ();
		m_sprIcon = _sprite;
		m_dataStaff = _dataStaff;
		m_sprIcon.depth += 1;
		m_eStep = STEP.IDLE;
		m_eStepPre = STEP.MAX;

		// しょっぱなには掃除とかをする
		m_iCheckCount = 100;
		AnimationIdol (true);

		m_CsvStaffParam = DataManager.GetStaff (m_dataStaff.staff_id);
		m_fieldItem = GameMain.ParkRoot.GetFieldItem (m_dataStaff.item_serial);

	}
	override public void SetDepth( int _iDepth ){
		m_sprIcon.depth = _iDepth + 2;
	}

	public int m_iCheckCount;

	public CsvStaffParam m_CsvStaffParam;
	public CtrlFieldItem m_fieldItem;

	public override void update_idle (bool _bInit)
	{
		if (_bInit) {
			m_iCheckCount += 1;

			if (30 < m_iCheckCount) {
				m_iCheckCount = 0;
				if (m_CsvStaffParam.effect_param == (int)DefineOld.Staff.EFFECT_PARAM.CLEAN || m_CsvStaffParam.effect_param == (int)DefineOld.Staff.EFFECT_PARAM.MEAL_CLEAN) {
					m_fieldItem.Clean ();
				}
				if (m_CsvStaffParam.effect_param == (int)DefineOld.Staff.EFFECT_PARAM.MEAL || m_CsvStaffParam.effect_param == (int)DefineOld.Staff.EFFECT_PARAM.MEAL_CLEAN) {
					m_fieldItem.Meal ();
				}
			}
		}
		return;
	}

	override public void AnimationIdol(bool _bInit){
		if (_bInit) {
			string strName = "staff_icon" + m_dataStaff.staff_id.ToString ();
			//m_sprIcon.atlas = AtlasManager.Instance.GetAtlas (strName);
			//m_sprIcon.spriteName = strName;
			m_sprIcon.sprite2D = SpriteManager.Instance.Load( string.Format( "texture/staff/{0}.png" , strName ));
			m_sprIcon.width = (int)m_sprIcon.sprite2D.textureRect.width;
			m_sprIcon.height = (int)m_sprIcon.sprite2D.textureRect.height;
		}
	}

	override public void AnimationMove(bool _bInit){
		if (_bInit) {
			m_fAnimationTime = 0.0f;
			m_fAnimationInterval = 0.2f;
			m_iAnimationFrame = 0;
		}

		m_fAnimationTime += Time.deltaTime;
		if (m_fAnimationInterval < m_fAnimationTime) {
			m_fAnimationTime -= m_fAnimationInterval;

			m_iAnimationFrame += 1;
			m_iAnimationFrame %= 2;		// トータル２フレーム

			string strName = "staff_icon" + m_dataStaff.staff_id.ToString ();

			if (m_iAnimationFrame == 1) {
				strName += "_2";
			}
			m_sprIcon.sprite2D = SpriteManager.Instance.Load( string.Format( "texture/staff/{0}.png" , strName ));
			m_sprIcon.width = (int)m_sprIcon.sprite2D.textureRect.width;
			m_sprIcon.height = (int)m_sprIcon.sprite2D.textureRect.height;
		}
		return;
	}

	override public void AnimationEat(bool _bInit , int _iMealId ){
	}



}
