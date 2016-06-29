﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DBItemMaster {
	//テーブル名
	public const string TABLE_NAME = "item_master";
	public const string FILE_NAME = "SOCsvItemParam";

	//public SOCsvItemParam m_soCsvItemParam;

	public List<CsvItemParam> data_list = new List<CsvItemParam>();

	public DBItemMaster( string _strAsyncName ){
		//m_soCsvItemParam = PrefabManager.Instance.PrefabLoadInstance (FILE_NAME).GetComponent<SOCsvItemParam> ();
	}
	public string READ_ERROR_STRING = "sql_datamanager_read_error";

	/*
		ここのWriteは即書き込み
	*/

	//DBへ保存
	public void Replace(CsvItemParam _replocalData)
	{
		foreach (CsvItemParam data in DataManager.Instance.m_csvItem.list) {
			if (data.item_id == _replocalData.item_id) {
				data.Copy (_replocalData);
				return;
			}
		}
		DataManager.Instance.m_csvItem.list.Add (_replocalData);

		/*


		//データの上書きのコマンドを設定する　
		string strQuery = "replace into " + TABLE_NAME + " (item_id,status,name,category,type,cell_type,description,need_coin,need_ticket,need_money,size,cost,area,revenue,revenue_interval,revenue_up,production_time,setting_limit,sub_parts_id,open_item_id,revenue_up2,add_coin) values( '" +
		                  _replocalData.item_id.ToString () + "','" +
		                  _replocalData.status.ToString () + "',\"" +
		                  _replocalData.name.ToString () + "\",'" +
		                  _replocalData.category.ToString () + "','" +
		                  _replocalData.type.ToString () + "','" +
		                  _replocalData.cell_type.ToString () + "',\"" +
		                  _replocalData.description.ToString () + "\",'" +
		                  _replocalData.need_coin.ToString () + "','" +
		                  _replocalData.need_ticket.ToString () + "','" +
		                  _replocalData.need_money.ToString () + "','" +
		                  _replocalData.size.ToString () + "','" +
		                  _replocalData.cost.ToString () + "','" +
		                  _replocalData.area.ToString () + "','" +
		                  _replocalData.revenue.ToString () + "','" +
		                  _replocalData.revenue_interval.ToString () + "','" +
		                  _replocalData.revenue_up.ToString () + "','" +
		                  _replocalData.production_time.ToString () + "','" +
		                  _replocalData.setting_limit.ToString () + "','" +
		                  _replocalData.sub_parts_id.ToString () + "','" +
		                  _replocalData.open_item_id.ToString () + "','" +
		                  _replocalData.revenue_up2.ToString () + "','" +
		                  _replocalData.add_coin.ToString () + "');";

		Debug.Log( "DBPark:"+strQuery);

		//m_sqlDBはDBDataBaseのプロテクト変数
		SQLiteQuery query = new SQLiteQuery(m_sqlDB,strQuery);

		query.Step ();      //
		query.Release ();   //解放
		*/
	}

	public void Update( int _iItemId , Dictionary<string , string > _dict , bool _bDebugLog = true){
		foreach (CsvItemParam data in DataManager.Instance.m_csvItem.list) {
			if (data.item_id == _iItemId) {
				data.Set (_dict);
				return;
			}
		}
		return;
		/*
		string strQuery = "update " + TABLE_NAME + " set ";

		bool bHead = true;
		foreach (string key in _dict.Keys) {

			if (bHead) {
				strQuery += "";		// なにもしません
				bHead = false;
			} else {
				strQuery += ",";		// なにもしません
			}
			strQuery += key + " = " + _dict[key] + " ";
		}
		strQuery += " where item_id = " + _iItemId.ToString () + ";";
		if (_bDebugLog) {
			Debug.Log ("DBItem.Update:" + strQuery);
		}
		SQLiteQuery query = new SQLiteQuery(m_sqlDB,strQuery);
		query.Step ();      //
		query.Release ();   //解放
		*/
	}


	public List<CsvItemParam> Select( string _strWhere = null ){

		List<CsvItemParam> ret_list = new List<CsvItemParam> ();

		if (_strWhere.Equals ("ticket_gold") == true) {
			foreach (CsvItemParam data in DataManager.Instance.m_csvItem.list) {
				if( data.status == 1 &&( data.category==(int)DefineOld.Item.Category.TICKET || data.category== (int)DefineOld.Item.Category.GOLD)){
					ret_list.Add (data);
				}
			}
			return ret_list;
		}


		foreach (CsvItemParam data in DataManager.Instance.m_csvItem.list) {
			if (data.Equals (_strWhere) == true) {
				ret_list.Add (data);
			}
		}
		return ret_list;
		/*
		string strQuery = "select * from " + TABLE_NAME;

		if (_strWhere != null) {
			strQuery += " where " + _strWhere;
		}

		//Debug.Log ("DBWork SelectQuery : "+strQuery);

		//m_sqlDBはDBDataBaseのプロテクト変数
		SQLiteQuery query = new SQLiteQuery(m_sqlDB,strQuery);

		List<CsvItemParam> ret = new List<CsvItemParam> ();

		//テーブルからデータを取ってくる
		while (query.Step ()) {
			CsvItemParam data = MakeData (query);
			ret.Add (data);
		}
		query.Release ();

		return ret;
		*/
	}

	public CsvItemParam Select( int _iItemId ){

		foreach (CsvItemParam data in DataManager.Instance.m_csvItem.list) {
			if (data.item_id == _iItemId) {
				return data;
			}
		}
		return new CsvItemParam ();
		/*
		return new CsvItemParam();;

		CsvItemParam ret;
		string strQuery = "select * from " + TABLE_NAME + " where item_id = '" + _iItemId + "';";

		SQLiteQuery query = new SQLiteQuery(m_sqlDB , strQuery );
		if( query.Step() ){
			ret = MakeData (query);
		}
		else {
			ret = new CsvItemParam();
		}
		return ret;
		*/
	}


	//テーブル以下全て取ってくる
	public List<CsvItemParam> SelectAll()
	{
		return DataManager.Instance.m_csvItem.list;
		//return m_soCsvItemParam.list;
		/*
		//データをクリア
		data_list.Clear ();

		string strQuery = "SELECT * FROM "+TABLE_NAME;

		SQLiteQuery query = new SQLiteQuery(m_sqlDB,strQuery);

		//テーブルからデータを取ってくる
		while (query.Step ()) {
			CsvItemParam data = MakeData (query);
			data_list.Add (data);
		}
		query.Release ();

		return data_list;
		*/
	}


	public CsvItemParam MakeData( ){
		/*
		SQLiteQuery qr = _qr;
		CsvItemParam data = new CsvItemParam();

		if( m_bDebugLog ){
			//Debug.Log( "key  :" + strKey );
			//Debug.Log( "value:" + strValue );
		}
		data.item_id = qr.GetInteger("item_id");
		data.status = qr.GetInteger("status");
		data.name = qr.GetString("name");
		data.category = qr.GetInteger("category");
		data.type = qr.GetInteger("type");
		data.cell_type = qr.GetInteger("cell_type");
		data.description = qr.GetString("description");
		data.need_coin = qr.GetInteger("need_coin");
		data.need_ticket = qr.GetInteger("need_ticket");
		data.need_money = qr.GetInteger("need_money");
		data.size = qr.GetInteger("size");
		data.cost = qr.GetInteger("cost");
		data.area = qr.GetInteger("area");
		data.revenue = qr.GetInteger("revenue");
		data.revenue_interval = qr.GetInteger("revenue_interval");
		data.revenue_up = qr.GetInteger("revenue_up");
		data.production_time = qr.GetInteger("production_time");
		data.setting_limit = qr.GetInteger("setting_limit");
		data.sub_parts_id = qr.GetInteger("sub_parts_id");
		data.open_item_id = qr.GetInteger("open_item_id");
		data.revenue_up2 = qr.GetInteger("revenue_up2");
		data.add_coin = qr.GetInteger("add_coin");

		return data;
		*/
		return new CsvItemParam ();
	}


}



