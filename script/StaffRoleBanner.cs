using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
public class StaffRoleBanner : MonoBehaviour {

	[SerializeField]
	private Image m_imgFrame;

	[SerializeField]
	private Image m_imgIcon;

	[SerializeField]
	private Text m_txtMessage;

	private int m_iRole;		// あえてintで持ちます（分かりにくいな）
	private DataStaffParam m_param;

	public void UpdateRole( int _iRole ){
		if (m_iRole == _iRole) {
			m_imgFrame.color = new Color (1.0f, 241.0f / 255.0f, 161.0f / 255.0f);
		} else {
			m_imgFrame.color = new Color (1.0f, 1.0f, 1.0f);
		}
	}

	public void ChangeRole(){
		if (m_param.role != m_iRole) {
			m_param.role = m_iRole;
		}
	}

	public void Initialize( DataStaff.ROLE _eRole , DataStaffParam _param ){

		m_param = _param;
		gameObject.GetComponent<Button> ().onClick.AddListener (ChangeRole);

		m_iRole = (int)_eRole;
		m_imgIcon.sprite = SpriteManager.Instance.LoadSprite( DataStaff.GetIconRoleMiddle((int)_eRole));

		switch (_eRole) {
		case DataStaff.ROLE.FLOOR:
			m_txtMessage.text = "フロアスタッフ\nフットワークが重要";
			break;
		case DataStaff.ROLE.ENTRANCE:
			m_txtMessage.text = "受付スタッフ\nお店の看板！";
			break;
		case DataStaff.ROLE.CHEF:
			m_txtMessage.text = "キッチンスタッフ\nおいしい料理を作ります！";
			break;
		default:
			m_txtMessage.text = "待機させます";
			break;
		}
		UpdateRole (_param.role);
		_param.UpdateRole.AddListener (UpdateRole);
	}




	//255,241,161



}
