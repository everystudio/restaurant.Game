using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DBMonster  {
	//テーブル名
	public const string TABLE_NAME = "monster";
	public const string FILE_NAME = "SODataMonster";
	//public SODataMonster m_soDataMonster;

	public List<DataMonster> data_list = new List<DataMonster>();

	public DBMonster( string _strAsyncName ){
		//m_soDataMonster = PrefabManager.Instance.PrefabLoadInstance (FILE_NAME).GetComponent<SODataMonster> ();
	}
	public string READ_ERROR_STRING = "sql_datamanager_read_error";

	/*
		ここのWriteは即書き込み
	*/

	//DBへ保存
	public void Replace(DataMonsterParam _replocalData)
	{
		// ここ、最初しか呼ばれてないのでもうチェックしない
		DataManager.Instance.dataMonster.list.Add (_replocalData);

		return;
		/*
		//データの上書きのコマンドを設定する　
		string strQuery = "replace into " + TABLE_NAME + " (monster_serial,monster_id,item_serial,condition,collect_time,meal_time,clean_time) values( '" +
		                  _replocalData.monster_serial.ToString () + "','" +
		                  _replocalData.monster_id.ToString () + "','" +
		                  _replocalData.item_serial.ToString () + "','" +
		                  _replocalData.condition.ToString () + "','" +
		                  _replocalData.collect_time.ToString () + "','" +
		                  _replocalData.meal_time.ToString () + "','" +
		                  _replocalData.clean_time.ToString () + "');";

		Debug.Log( "DBMonster:"+strQuery);

		//m_sqlDBはDBDataBaseのプロテクト変数
		SQLiteQuery query = new SQLiteQuery(m_sqlDB,strQuery);

		query.Step ();      //
		query.Release ();   //解放
		*/
	}

	public void Update( int _iSerial , Dictionary<string , string > _dict ){
		//Debug.LogError (_iSerial);
		foreach (DataMonsterParam data in DataManager.Instance.dataMonster.list) {
			//Debug.LogError (data.item_serial);
			if (data.monster_serial == _iSerial) {
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
		strQuery += " where monster_serial = " + _iSerial.ToString () + ";";
		Debug.Log( "DBMonster.Update:"+strQuery);
		SQLiteQuery query = new SQLiteQuery(m_sqlDB,strQuery);
		query.Step ();      //
		query.Release ();   //解放
		return;
		*/
	}


	public void Update( int _iMonsterSerial , int _iItemSerial , int _iCondition = -1 ){

		foreach (DataMonsterParam data in DataManager.Instance.dataMonster.list) {
			if (data.monster_serial == _iMonsterSerial) {
				data.item_serial = _iItemSerial;
				if (_iCondition != -1) {
					data.condition = _iCondition;
				}
			}
		}
		return;
		/*
		string strQuery = "update " + TABLE_NAME +
		                  " set item_serial = " + _iItemSerial.ToString ();
		if (_iCondition != -1) {
			strQuery += " , " + " condition = " + _iCondition.ToString ();
		}
		strQuery += " where monster_serial = " + _iMonsterSerial.ToString () + ";";
		Debug.Log( "DBMonster.Update:"+strQuery);
		//m_sqlDBはDBDataBaseのプロテクト変数
		SQLiteQuery query = new SQLiteQuery(m_sqlDB,strQuery);

		query.Step ();      //
		query.Release ();   //解放
		*/
	}

	// 新規購入の場合
	// とり得る数からシリアルを返すようにする
	public DataMonsterParam Insert( int _iMonsterId , int _iItemSerial ){

		int topIndex = DataManager.Instance.dataMonster.list.Count + 1;

		string strNow = TimeManager.StrNow ();
		int iStartCondition = (int)DefineOld.Monster.CONDITION.FINE;

		DataMonsterParam insert_data = new DataMonsterParam ();
		insert_data.monster_id = _iMonsterId;
		insert_data.monster_serial = topIndex;
		insert_data.item_serial = _iItemSerial;
		insert_data.condition = iStartCondition;
		insert_data.collect_time = strNow;
		insert_data.meal_time = strNow;
		insert_data.clean_time = strNow;
		DataManager.Instance.dataMonster.list.Add (insert_data);

		return insert_data;
		/*
		//データの上書きのコマンドを設定する　
		string strQuery = "insert into " + TABLE_NAME + " (monster_serial,monster_id,item_serial,condition,collect_time,meal_time,clean_time) values( '" +
		                  topIndex.ToString () + "','" +
		                  _iMonsterId.ToString () + "','" +
		                  _iItemSerial.ToString () + "','" +
		                  iStartCondition.ToString () + "','" +
		                  strNow + "','" +
		                  strNow + "','" +
		                  strNow + "');";

		Debug.Log ("DBMonster Insert : "+strQuery);

		SQLiteQuery query = new SQLiteQuery(m_sqlDB,strQuery);

		query.Step ();      //
		query.Release ();   //解放

		DataMonster retMonster = Select (topIndex);

		return retMonster;
		*/
	}

	public DataMonsterParam Select( int _iSerial ){
		foreach (DataMonsterParam data in DataManager.Instance.dataMonster.list) {
			if (data.monster_serial == _iSerial) {
				return data;
			}
		}
		return new DataMonsterParam ();
		/*
		DataMonster ret;
		string strQuery = "select * from " + TABLE_NAME + " where monster_serial = '" + _iSerial + "';";

		SQLiteQuery query = new SQLiteQuery(m_sqlDB , strQuery );
		if( query.Step() ){
			ret = MakeData (query);
		}
		else {
			ret = new DataMonster();
		}
		return ret;
		*/
	}


	//テーブル以下全て取ってくる
	public List<DataMonsterParam> SelectAll()
	{
		return DataManager.Instance.dataMonster.list;
		/*
		//データをクリア
		data_list.Clear ();

		string strQuery = "SELECT * FROM "+TABLE_NAME;

		SQLiteQuery query = new SQLiteQuery(m_sqlDB,strQuery);

		//テーブルからデータを取ってくる
		while (query.Step ()) {
			DataMonster data = MakeData (query);
			data_list.Add (data);
		}
		query.Release ();

		return data_list;
		*/
	}

	public List<DataMonsterParam> Select( string _strWhere = null ){
		List<DataMonsterParam> ret_list = new List<DataMonsterParam> ();

		foreach (DataMonsterParam data in DataManager.Instance.dataMonster.list) {
			if (data.Equals (_strWhere)) {
				ret_list.Add (data);
			}
		}
		return ret_list;
		/*
		string strQuery = "select * from " + TABLE_NAME;

		if (_strWhere != null) {
			strQuery += " where " + _strWhere;
		}
		//Debug.Log ("DBMonster SelectQuery : "+strQuery);
		//m_sqlDBはDBDataBaseのプロテクト変数
		SQLiteQuery query = new SQLiteQuery(m_sqlDB,strQuery);
		List<DataMonster> ret = new List<DataMonster> ();
		//テーブルからデータを取ってくる
		while (query.Step ()) {
			DataMonster data = MakeData (query);
			ret.Add (data);
		}
		query.Release ();
		return ret;
		*/
	}

	public List<DataMonsterParam> Select(List<string> _whereList ){

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


}



