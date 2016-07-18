using UnityEngine;
using System.Collections;

public class MasterItemShopParam : MasterItemParamBase{
	
	private string m_need_type;
	private int m_need_num;

	public string need_type { get{ return m_need_type;} set{m_need_type = value; } }
	public int need_num { get{ return m_need_num;} set{m_need_num = value; } }



}

public class MasterItemShop : CsvData<MasterItemShopParam> {

}
