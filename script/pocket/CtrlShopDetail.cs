using UnityEngine;
using System.Collections;

public class CtrlShopDetail : MonoBehaviour {

	#region SerializeField
	[SerializeField]
	private ButtonBase m_ButtonClose;

	[SerializeField]
	private ButtonBase m_ButtonCollect;

	[SerializeField]
	private UILabel m_lbName;
	[SerializeField]
	private UILabel m_lbUriage;
	[SerializeField]
	private UILabel m_lbExp;
	[SerializeField]
	private UILabel m_lbDescription;
	[SerializeField]
	private UI2DSprite m_sprItem;
	#endregion

	public bool m_bIsEnd;
	private int m_iItemSerial;
	private CtrlParkRoot m_csParkRoot;

	public DataItemParam m_dataItemParam;

	public void Init( DataItemParam _dataItem , CtrlParkRoot _parkRoot ){

		m_dataItemParam = _dataItem;
		m_csParkRoot = _parkRoot;
		m_iItemSerial = _dataItem.item_serial;

		m_bIsEnd = false;

		CsvItemParam master_data = DataManager.Instance.m_csvItem.Select (_dataItem.item_id);

		m_lbName.text = master_data.name;
		m_lbUriage.text = UtilString.GetSyuunyuu( master_data.revenue , master_data.revenue_interval );
		m_lbExp.text = "";
		m_lbDescription.text = master_data.description;

		m_sprItem.sprite2D = SpriteManager.Instance.Load( string.Format( "texture/item/item{0:D2}_01.png" ,_dataItem.item_id ));
		m_sprItem.width = (int)m_sprItem.sprite2D.textureRect.width;
		m_sprItem.height = (int)m_sprItem.sprite2D.textureRect.height;

		return;
	}

	public bool IsEnd() {
		return m_bIsEnd;
	}

	// Update is called once per frame
	void Update () {

		if (m_bIsEnd == false) {
			if (m_ButtonCollect.ButtonPushed) {

				// 消す予定のところに新しい土地を設置する
				for (int x = m_dataItemParam.x; x < m_dataItemParam.x + m_dataItemParam.width; x++) {
					for (int y = m_dataItemParam.y; y < m_dataItemParam.y + m_dataItemParam.height; y++) {
						GameObject obj = PrefabManager.Instance.MakeObject ("prefab/PrefFieldItem", GameMain.ParkRoot.gameObject);
						obj.name = "fielditem_" + x.ToString () + "_" + y.ToString ();
						CtrlFieldItem script = obj.GetComponent<CtrlFieldItem> ();
						script.Init (x, y, 0);
						GameMain.ParkRoot.m_fieldItemList.Add (script);
					}
				}
				// 取り下げ
				DataManager.Instance.m_dataItem.Update (m_iItemSerial, 0, 0, 0);


				int iRemoveIndex = 0;
				foreach (CtrlFieldItem item in GameMain.ParkRoot.m_fieldItemList) {
					if (item.m_dataItemParam.item_serial == GameMain.Instance.m_iSettingItemSerial) {
						item.Remove ();
						GameMain.ParkRoot.m_fieldItemList.RemoveAt (iRemoveIndex);
						break;
					}
					iRemoveIndex += 1;
				}
				/*
				foreach (CtrlFieldItem script in m_csParkRoot.m_fieldItemList) {
					if (script.m_dataItemParam.item_serial == m_iItemSerial) {

						m_csParkRoot.m_fieldItemList.Remove (script);
						Destroy (script.gameObject);
						break;
					}
				}
				*/
				// 仕事の確認
				DataWork.WorkCheck ();
				GameMain.Instance.HeaderRefresh ();

				m_csParkRoot.ConnectingRoadCheck ();

				m_bIsEnd = true;
				SoundManager.Instance.PlaySE (SoundName.BUTTON_CANCEL , "https://s3-ap-northeast-1.amazonaws.com/every-studio/app/sound/se");

			} else if (m_ButtonClose.ButtonPushed) {
				m_bIsEnd = true;
				SoundManager.Instance.PlaySE (SoundName.BUTTON_CANCEL, "https://s3-ap-northeast-1.amazonaws.com/every-studio/app/sound/se");

			} else {
			}
		}




	
	}
}














