using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[System.Serializable]
public class CsvMonsterParam : CsvDataParam
{
	public int m_monster_id;
	public string m_name;
	public string m_description_cell;
	public int m_cost;
	public int m_fill;
	public int m_dust;
	public int m_coin;
	public int m_ticket;
	public int m_revenew_coin;
	public int m_revenew_exp;
	public int m_revenew_interval;
	public int m_open_work_id;
	public string m_description_book;
	public int m_size;
	public int m_rare;
	public int m_status;


	public int monster_id { get{ return m_monster_id; } set{m_monster_id= value;} }
	public string name { get{ return m_name; } set{m_name= value;} }
	public string description_cell { get{ return m_description_cell; } set{m_description_cell= value;} }
	public int cost { get{ return m_cost; } set{m_cost= value;} }
	public int fill { get{ return m_fill; } set{m_fill= value;} }
	public int dust { get{ return m_dust; } set{m_dust= value;} }
	public int coin { get{ return m_coin; } set{m_coin= value;} }
	public int ticket { get{ return m_ticket; } set{m_ticket= value;} }
	public int revenew_coin { get{ return m_revenew_coin; } set{m_revenew_coin= value;} }
	public int revenew_exp { get{ return m_revenew_exp; } set{m_revenew_exp= value;} }
	public int revenew_interval { get{ return m_revenew_interval; } set{m_revenew_interval= value;} }
	public int open_work_id { get{ return m_open_work_id; } set{m_open_work_id= value;} }
	public string description_book { get{ return m_description_book; } set{m_description_book= value;} }
	public int size { get{ return m_size; } set{m_size= value;} }
	public int rare { get{ return m_rare; } set{m_rare= value;} }
	public int status { get{ return m_status;} set{m_status = value; } }





}


[System.Serializable]
public class CsvMonster : CsvData<CsvMonsterParam> {

	public const string FilePath = "csv/monster";
	public void Load() {
		Load (FilePath);
	}

	public CsvMonsterParam Select( int _iMonsterId ){
		return SelectOne (string.Format (" monster_id = {0} ", _iMonsterId));
	}

	public int Update( int _iMonsterId , Dictionary< string , string >  _dictUpdate ){
		return base.Update (_dictUpdate, string.Format (" monster_id = {0} ", _iMonsterId));
	}


	protected override CsvMonsterParam makeParam (List<SpreadSheetData> _list, int _iSerial, int _iRow)
	{
		int index = 1;
		SpreadSheetData data_monsterid = SpreadSheetData.GetSpreadSheet( _list,_iRow , index++ );
		SpreadSheetData data_name= SpreadSheetData.GetSpreadSheet( _list,_iRow , index++ );
		SpreadSheetData data_descriptioncell= SpreadSheetData.GetSpreadSheet( _list,_iRow , index++ );
		SpreadSheetData data_cost= SpreadSheetData.GetSpreadSheet( _list,_iRow , index++ );
		SpreadSheetData data_fill= SpreadSheetData.GetSpreadSheet( _list,_iRow , index++ );
		SpreadSheetData data_dust= SpreadSheetData.GetSpreadSheet( _list,_iRow , index++ );
		SpreadSheetData data_coin= SpreadSheetData.GetSpreadSheet( _list,_iRow , index++ );
		SpreadSheetData data_ticket= SpreadSheetData.GetSpreadSheet( _list,_iRow , index++ );
		SpreadSheetData data_revenewcoin= SpreadSheetData.GetSpreadSheet( _list,_iRow , index++ );
		SpreadSheetData data_revenewexp= SpreadSheetData.GetSpreadSheet( _list,_iRow , index++ );
		SpreadSheetData data_revenewinterval= SpreadSheetData.GetSpreadSheet( _list,_iRow , index++ );
		SpreadSheetData data_openworkid= SpreadSheetData.GetSpreadSheet( _list,_iRow , index++ );
		SpreadSheetData data_descriptionbook= SpreadSheetData.GetSpreadSheet( _list,_iRow , index++ );
		SpreadSheetData data_size= SpreadSheetData.GetSpreadSheet( _list,_iRow , index++ );
		SpreadSheetData data_rare= SpreadSheetData.GetSpreadSheet( _list,_iRow , index++ );
		SpreadSheetData data_status= SpreadSheetData.GetSpreadSheet( _list,_iRow , index++ );

		CsvMonsterParam retParam = new CsvMonsterParam ();

		retParam.m_monster_id = int.Parse(data_monsterid.param);
		retParam.m_name = data_name.param;
		retParam.m_description_cell =data_descriptioncell.param;
		retParam.m_cost = int.Parse(data_cost.param);
		retParam.m_fill = int.Parse(data_fill.param);
		retParam.m_dust = int.Parse(data_dust.param);
		retParam.m_coin = int.Parse(data_coin.param);
		retParam.m_ticket = int.Parse(data_ticket.param);
		retParam.m_revenew_coin = int.Parse(data_revenewcoin.param);
		retParam.m_revenew_exp = int.Parse(data_revenewexp.param);
		retParam.m_revenew_interval = int.Parse(data_revenewinterval.param);
		retParam.m_open_work_id = int.Parse(data_openworkid.param);
		retParam.m_description_book = data_descriptionbook.param;
		retParam.m_size = int.Parse(data_size.param);
		retParam.m_rare = int.Parse(data_rare.param);
		retParam.m_status = int.Parse(data_status.param);

		return retParam;

	}

}


