using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CsvItemParam : CsvDataParam {



	public int m_item_id;
	public int m_status;
	public string m_name;
	public int m_category;
	public int m_type;
	public int m_cell_type;
	public string m_description;
	public int m_need_coin;
	public int m_need_ticket;
	public int m_need_money;
	public int m_size;
	public int m_cost;
	public int m_area;
	public int m_revenue;
	public int m_revenue_interval;
	public int m_revenue_up;
	public int m_production_time;
	public int m_setting_limit;
	public int m_sub_parts_id;
	public int m_open_item_id;
	public int m_revenue_up2;
	public int m_add_coin;
	public int m_anim;

	public int item_id { get{ return m_item_id; } set{ m_item_id = value; } }
	public int status { get{ return m_status; } set{ m_status= value; } }
	public string name { get{ return m_name; } set{ m_name = value; } }
	public int category { get{ return m_category; } set{ m_category = value; } }
	public int type { get{ return m_type; } set{ m_type = value; } }
	public int cell_type { get{ return m_cell_type; } set{ m_cell_type = value; } }
	public string description { get{ return m_description; } set{ m_description = value; } }
	public int need_coin { get{ return m_need_coin; } set{ m_need_coin = value; } }
	public int need_ticket { get{ return m_need_ticket; } set{ m_need_ticket = value; } }
	public int need_money { get{ return m_need_money; } set{ m_need_money = value; } }
	public int size { get{ return m_size; } set{ m_size = value; } }
	public int cost { get{ return m_cost; } set{ m_cost = value; } }
	public int area { get{ return m_area; } set{ m_area = value; } }
	public int revenue { get{ return m_revenue; } set{ m_revenue = value; } }
	public int revenue_interval { get{ return m_revenue_interval; } set{ m_revenue_interval = value; } }
	public int revenue_up { get{ return m_revenue_up; } set{ m_revenue_up = value; } }
	public int production_time { get{ return m_production_time; } set{ m_production_time = value; } }
	public int setting_limit { get{ return m_setting_limit; } set{ m_setting_limit = value; } }
	public int sub_parts_id { get{ return m_sub_parts_id; } set{ m_sub_parts_id = value; } }
	public int open_item_id { get{ return m_open_item_id; } set{ m_open_item_id = value; } }
	public int revenue_up2 { get{ return m_revenue_up2; } set{ m_revenue_up2 = value; } }
	public int add_coin { get{ return m_add_coin; } set{ m_add_coin = value; } }
	public int anim { get{ return m_anim; } set{ m_anim = value; } }


	public void Copy( CsvItemParam _data ){
		item_id = _data.item_id;
		status = 0;			// 通常は利用できるとして扱う
		name = _data.name;
		category = _data.category;
		type = _data.type;
		cell_type = _data.cell_type;
		description = _data.description;
		need_coin = _data.need_coin;
		need_ticket = _data.need_ticket;
		need_money = _data.need_money;
		size = _data.size;
		cost = _data.cost;
		area = _data.area;
		revenue = _data.revenue;
		revenue_interval = _data.revenue_interval;
		revenue_up = _data.revenue_up;
		production_time = _data.production_time;
		setting_limit = _data.setting_limit;
		sub_parts_id = _data.sub_parts_id;
		open_item_id = _data.open_item_id;
		revenue_up2 = _data.revenue_up2;
		add_coin = _data.add_coin;
		item_id = _data.item_id;
		anim = _data.anim;

	}


}


[System.Serializable]
public class CsvItem : CsvData< CsvItemParam>
{

	public const string FilePath = "csv/item";

	public void Load() { Load(FilePath); }

	public CsvItemParam Select( int _iItemId ){
		return SelectOne( string.Format( " item_id = {0} " , _iItemId ));
	}

	public void Update( int _iItemId , Dictionary<string , string > _dict ){
		base.Update (_dict, string.Format (" item_id = {0}", _iItemId));
		return;
	}
	/*
	new public List<CsvItemParam> Select( string _strWhere ){
		if (_strWhere.Equals ("ticket_gold") == true) {
			List<CsvItemParam> ret_list = new List<CsvItemParam> ();
			foreach (CsvItemParam data in DataManager.Instance.m_csvItem.list) {
				if (data.status == 1 && (data.category == (int)DefineOld.Item.Category.TICKET || data.category == (int)DefineOld.Item.Category.GOLD)) {
					ret_list.Add (data);
				}
			}
			return ret_list;
		} else {
			return base.Select(_strWhere);
		}
	}
	*/

	protected override CsvItemParam makeParam (List<SpreadSheetData> _list, int _iSerial, int _iRow)
	{
		int index = 1;
		SpreadSheetData data_itemid = SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_status = SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_name = SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_category = SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_type = SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_celltype = SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_description = SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_needcoin = SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_needticket = SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_needmoney = SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_size = SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_cost = SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_area = SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_revenue = SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_revenueinterval = SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_revenueup = SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_productiontime = SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_settinglimit = SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_subpartsid = SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_openitemid = SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_revenueup2 = SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_addcoin = SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_anim = SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);

		CsvItemParam retParam = new CsvItemParam ();

		retParam.m_item_id = int.Parse (data_itemid.param);
		retParam.m_status = int.Parse(data_status.param);
		retParam. m_name = data_name.param;
		retParam.m_category = int.Parse(data_category.param);
		retParam.m_type = int.Parse(data_type.param);
		retParam.m_cell_type = int.Parse(data_celltype.param);
		retParam. m_description = data_description.param;
		retParam.m_need_coin = int.Parse(data_needcoin.param);
		retParam.m_need_ticket = int.Parse(data_needticket.param);
		retParam.m_need_money = int.Parse(data_needmoney.param);
		retParam.m_size = int.Parse(data_size.param);
		retParam.m_cost = int.Parse(data_cost.param);
		retParam.m_area = int.Parse(data_area.param);
		retParam.m_revenue = int.Parse(data_revenue.param);
		retParam.m_revenue_interval = int.Parse(data_revenueinterval.param);
		retParam.m_revenue_up = int.Parse(data_revenueup.param);
		retParam.m_production_time = int.Parse(data_productiontime.param);
		retParam.m_setting_limit = int.Parse(data_settinglimit.param);
		retParam.m_sub_parts_id = int.Parse(data_subpartsid.param);
		retParam.m_open_item_id = int.Parse(data_openitemid.param);
		retParam.m_revenue_up2 = int.Parse(data_revenueup2.param);
		retParam.m_add_coin = int.Parse(data_addcoin.param);
		retParam.m_anim = int.Parse(data_anim.param);

		return retParam;
	}


}


