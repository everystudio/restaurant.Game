using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MasterShopParam : MasterItemParam{
	
	private string m_need_type;
	private int m_need_num;

	public string need_type { get{ return m_need_type;} set{m_need_type = value; } }
	public int need_num { get{ return m_need_num;} set{m_need_num = value; } }

}

public class MasterShop : CsvData<MasterShopParam> {
	public const string FILENAME = "master/shop";

	public bool LoadMulti( string _strFilename , List<MasterItemParam> _itemList ){

		if (LoadMulti (_strFilename)) {
			foreach (MasterShopParam param in list) {
				foreach (MasterItemParam itemParam in _itemList) {
					if (itemParam.item_id == param.item_id) {
						param.SetParam (itemParam);
						break;
					}
				}
			}

			return true;
		}
		return false;

	}

}
