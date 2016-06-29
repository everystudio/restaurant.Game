﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class DBItem  {
	//テーブル名
	public const string TABLE_NAME = "item";
	//public const string FILE_NAME = "SODataItem";
	public const string FILE_NAME = "NameChange";

	public List<DataItem> data_list = new List<DataItem>();

	public DBItem( string _strAsyncName ){
		//m_soDataItem = PrefabManager.Instance.PrefabLoadInstance ("NameChange").GetComponent<SODataItem> ();
		//m_soDataItem = PrefabManager.Instance.PrefabLoadInstance (FILE_NAME).GetComponent<SODataItem> ();
		//m_soDataItem = new SODataItem ();
	}
	public string READ_ERROR_STRING = "sql_datamanager_read_error";

	/*
		ここのWriteは即書き込み
	*/
	public void Update( int _iSerial , int _iStatus , int _iX , int _iY ){
		/*
		foreach (DataItem data in m_soDataItem.list) {
			if (data.item_serial == _iSerial) {
				data.status = _iStatus;
				data.x = _iX;
				data.y = _iY;
			}
		}
		*/
		return;
		/*
		string strQuery = "update " + TABLE_NAME + 
		                  " set status = " + _iStatus.ToString() + " , " +
		                  " x = " + _iX.ToString() + " , " +
		                  " y = " + _iY.ToString() + 
		                  " where item_serial = " + _iSerial.ToString () + ";";
		Debug.Log( "DBPark.Update:"+strQuery);
		//m_sqlDBはDBDataBaseのプロテクト変数
		SQLiteQuery query = new SQLiteQuery(m_sqlDB,strQuery);

		query.Step ();      //
		query.Release ();   //解放
		*/
	}

	public void Update( int _iSerial , Dictionary<string , string > _dict , bool _bDebugLog = true){

		foreach (DataItemParam data in DataManager.Instance.m_dataItem.list) {
			if (data.item_serial == _iSerial) {
				data.Set (_dict);
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
		strQuery += " where item_serial = " + _iSerial.ToString () + ";";
		if (_bDebugLog) {
			Debug.Log ("DBItem.Update:" + strQuery);
		}
		SQLiteQuery query = new SQLiteQuery(m_sqlDB,strQuery);
		query.Step ();      //
		query.Release ();   //解放
		*/
	}

	public int Insert( CsvItemParam _itemMaster , int _iStatus , int _iX , int _iY ){
		//データの上書きのコマンドを設定する　
		string strCreateTime = TimeManager.StrNow ();
		string strOpenTime =TimeManager.StrGetTime (_itemMaster.production_time);

		DataItemParam insert_data = new DataItemParam ();
		insert_data.item_serial = DataManager.Instance.m_dataItem.list.Count + 1;		// 事情があって+1
		insert_data.item_id = _itemMaster.item_id;
		insert_data.category = _itemMaster.category;
		insert_data.level = 1;
		insert_data.status = _iStatus;
		insert_data.x = _iX;
		insert_data.y = _iY;
		insert_data.width = _itemMaster.size;
		insert_data.height = _itemMaster.size;
		insert_data.collect_time = strOpenTime;
		insert_data.create_time = strCreateTime;

		DataManager.Instance.m_dataItem.list.Add (insert_data);

		bool bHit = false;
		foreach (DataItemParam data in DataManager.Instance.m_dataItem.list) {
			if (data.item_serial == insert_data.item_serial) {
				bHit = true;
				//Debug.LogError( string.Format( "serial{0} x={1} y={2}",data.item_serial,data.x,data.y ));
			}
		}

		if (bHit == false) {
			//Debug.LogError ("no hit");
		}

		return insert_data.item_serial;
		/*
		string strQuery = "replace into " + TABLE_NAME + " (item_id,category,level,status,x,y,width,height,collect_time,create_time) values( '" +
		                  _itemMaster.item_id.ToString () + "','" +
		                  _itemMaster.category.ToString () + "','" +
		                  1.ToString () + "','" +
		                  _iStatus.ToString () + "','" +
		                  _iX.ToString () + "','" +
		                  _iY.ToString () + "','" +
		                  _itemMaster.size.ToString () + "','" +
		                  _itemMaster.size.ToString () + "','" +
		                  strOpenTime + "','" +
		                  strCreateTime + "');";
		Debug.Log( "DBPark.Insert:"+strQuery);
		//m_sqlDBはDBDataBaseのプロテクト変数
		SQLiteQuery query = new SQLiteQuery(m_sqlDB,strQuery);

		query.Step ();      //
		query.Release ();   //解放

		List<DataItem> insert_item = Select (" status = " + _iStatus.ToString () + " and x=" + _iX.ToString () + " and y=" + _iY.ToString ());

		if (insert_item.Count == 1) {
			return insert_item [0].item_serial;
		
		} else {
			return 0;
		}
		*/
	}


	//DBへ保存
	public void Replace(DataItemParam _replocalData)
	{
		DataManager.Instance.m_dataItem.list.Add (_replocalData);
		return;
		/*
		//データの上書きのコマンドを設定する　
		string strQuery = "replace into " + TABLE_NAME + " (item_serial,item_id,category,level,status,x,y,width,height,collect_time,create_time) values( '" +
		                  _replocalData.item_serial.ToString () + "','" +
		                  _replocalData.item_id.ToString () + "','" +
		                  _replocalData.category.ToString () + "','" +
		                  _replocalData.level.ToString () + "','" +
		                  _replocalData.status.ToString () + "','" +
		                  _replocalData.x.ToString () + "','" +
		                  _replocalData.y.ToString () + "','" +
		                  _replocalData.width.ToString () + "','" +
		                  _replocalData.height.ToString () + "','" +
		                  _replocalData.collect_time.ToString () + "','" +
		                  _replocalData.create_time.ToString () + "');";

		Debug.Log( "DBPark:"+strQuery);

		//m_sqlDBはDBDataBaseのプロテクト変数
		SQLiteQuery query = new SQLiteQuery(m_sqlDB,strQuery);

		query.Step ();      //
		query.Release ();   //解放
		*/
	}

	public List<DataItemParam > Select ( string _strWhere ){
		List<DataItemParam> ret = new List<DataItemParam> ();
		foreach (DataItemParam data in DataManager.Instance.m_dataItem.list) {
			if (data.Equals (_strWhere) == true) {
				ret.Add (data);
			}
		}
		return ret;
	}

	public List<DataItemParam> Select( DefineOld.WHERE_PATTERN _ePattern , List<int> _iList = null ){

		List<DataItemParam> ret = new List<DataItemParam> ();

		foreach (DataItemParam data in DataManager.Instance.m_dataItem.list) {
			if (data.Equals (_ePattern , _iList) == true) {
				ret.Add (data);
			}
		}
		return ret;
		/*
		string strQuery = "select * from " + TABLE_NAME;

		if (_strWhere != null) {
			strQuery += " where " + _strWhere;
		}

		//Debug.Log ("DBWork SelectQuery : "+strQuery);

		//m_sqlDBはDBDataBaseのプロテクト変数
		SQLiteQuery query = new SQLiteQuery(m_sqlDB,strQuery);

		List<DataItem> ret = new List<DataItem> ();

		if (_bUse) {
			Debug.Log (strQuery);
		}
		//テーブルからデータを取ってくる
		while (query.Step ()) {
			DataItem data = MakeData (query);
			ret.Add (data);
		}
		query.Release ();

		return ret;
		*/
	}

	public List<DataItemParam> Select(List<string> _whereList ){

		string strWhere = "";

		int iWhereCount = 0;
		if (_whereList != null) {
			foreach (string temp in _whereList) {
				if (0 < iWhereCount ) {
					strWhere += " and ";
				}
				strWhere += temp;
			}
		}
		return Select( strWhere );
	}

	public DataItemParam Select( int _iSerial ){

		foreach (DataItemParam data in DataManager.Instance.m_dataItem.list) {
			if (data.item_serial == _iSerial) {
				return data;
			}
		}
		return new DataItemParam();
		/*
		DataItem ret;
		string strQuery = "select * from " + TABLE_NAME + " where item_serial = '" + _iSerial + "';";

		SQLiteQuery query = new SQLiteQuery(m_sqlDB , strQuery );
		if( query.Step() ){
			ret = MakeData (query);
		}
		else {
			ret = new DataItem();
		}
		return ret;
		*/
	}


	//テーブル以下全て取ってくる
	public List<DataItemParam> SelectAll()
	{
		return DataManager.Instance.m_dataItem.list;

		/*
		//データをクリア
		data_list.Clear ();

		string strQuery = "SELECT * FROM "+TABLE_NAME;

		SQLiteQuery query = new SQLiteQuery(m_sqlDB,strQuery);

		//テーブルからデータを取ってくる
		while (query.Step ()) {
			DataItem data = MakeData (query);
			data_list.Add (data);
		}
		query.Release ();

		return data_list;
		*/
	}

	public void TestThrow(){
		throw new Exception( "Throw Test Exception ");

	}


}



