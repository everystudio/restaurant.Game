using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

[System.Serializable]
public class DataMonsterParam : CsvDataParam{

	public int m_monster_serial;
	public int m_monster_id;
	public int m_item_serial;
	public int m_condition;
	public string m_collect_time;
	public string m_meal_time;
	public string m_clean_time;

	public int monster_serial { get{ return m_monster_serial;} set{m_monster_serial = value; } }
	public int monster_id { get{ return m_monster_id;} set{m_monster_id = value; } }
	public int item_serial { get{ return m_item_serial;} set{m_item_serial = value; } }
	public int condition { get{ return m_condition;} set{m_condition = value; } }
	public string collect_time { get{ return m_collect_time;} set{m_collect_time = value; } }
	public string meal_time { get{ return m_meal_time;} set{m_meal_time = value; } }
	public string clean_time { get{ return m_clean_time;} set{m_clean_time = value; } }

	/*
	public void Set(Dictionary<string , string > _dict){

		foreach (string key in _dict.Keys) {
			Debug.LogError (key);
			PropertyInfo propertyInfo = GetType ().GetProperty (key);
			if (propertyInfo.PropertyType == typeof(int)) {
				int iValue = int.Parse (_dict [key]);
				propertyInfo.SetValue (this, iValue, null);
			} else if (propertyInfo.PropertyType == typeof(string)) {
				Debug.LogError (_dict [key].Replace ("\"", ""));
				propertyInfo.SetValue (this, _dict [key].Replace ("\"", ""), null);
			} else if (propertyInfo.PropertyType == typeof(double)) {
				propertyInfo.SetValue (this, double.Parse (_dict [key]), null);
			} else if (propertyInfo.PropertyType == typeof(float)) {
				propertyInfo.SetValue (this, float.Parse (_dict [key]), null);
			}
			else {
				Debug.LogError ("error type unknown");
			}
		}
	}
	*/

	public int GetCollect( bool _bCollect , out int _iCollectGold , out int _iCollectExp){
		double diffSec = TimeManager.Instance.GetDiffNow (collect_time).TotalSeconds * -1.0d;
		//Debug.Log (diffSec.ToString() + ":" + condition.ToString() );
		CsvMonsterParam csvMonster = DataManager.GetMonster (monster_id);
		double dCount = diffSec / csvMonster.revenew_interval;

		if (1 < dCount) {
			dCount = 1;
		}
		int iCollectGold = (int)dCount * csvMonster.revenew_coin;
		int iCollectExp = (int)dCount * csvMonster.revenew_exp;
		if (0 < dCount  &&_bCollect) {
			string strNow = TimeManager.StrNow ();
			Dictionary< string , string > dict = new Dictionary< string , string > ();
			dict.Add ("collect_time", "\"" + strNow + "\"");
			DataManager.Instance.dataMonster.Update (monster_serial, dict );
		}

		_iCollectGold = iCollectGold;
		_iCollectExp = iCollectExp;

		// 健全なのは1
		if ( (int)DefineOld.Monster.CONDITION.SICK  == condition ) {
			iCollectGold = 0;
		}
		return (int)iCollectGold;
	}

	public void GetConditions( ref int _iCleanLevel , ref int _iMealLevel ){

		_iCleanLevel = 0;
		_iMealLevel = 0;

		double d_clean_time = TimeManager.Instance.GetDiffNow (clean_time).TotalSeconds * -1.0d;
		double d_meal_time = TimeManager.Instance.GetDiffNow (meal_time).TotalSeconds * -1.0d;

		foreach (CsvTimeData time_data in DataManager.csv_time) {
			if (time_data.type == 1) {
				if (d_clean_time < time_data.delta_time) {
					if (_iCleanLevel < time_data.now) {
						_iCleanLevel = time_data.now;
					}
				}

			} else if (time_data.type == 2) {
				if (d_meal_time < time_data.delta_time) {
					if (_iMealLevel < time_data.now) {
						_iMealLevel = time_data.now;
					}
				}
			} else {
			}
		}
		return;
	}

	new public bool Equals( string _strWhere ){

		//Debug.Log (_strWhere);
		string[] test = _strWhere.Trim().Split (' ');

		bool bRet = true;

		for (int i = 0; i < test.Length; i+=4 ) {
			//Debug.Log (test [i]);
			PropertyInfo propertyInfo = GetType ().GetProperty (test [i]);
			if (propertyInfo.PropertyType == typeof(int)) {
				int intparam = (int)propertyInfo.GetValue (this, null);
				string strJudge = test [i + 1];
				int intcheck = int.Parse(test[i+2]);
				if (strJudge.Equals ("=")) {
					if (intparam != intcheck) {
						bRet = false;
					}
				} else if (strJudge.Equals ("!=")) {
					if (intparam == intcheck) {
						bRet = false;
					}
				} else {
				}
			}
		}
		return bRet;
	}

	/*
	 * たぶんいらんので消す
	public void Set(Dictionary<string , string > _dict){

		foreach (string key in _dict.Keys) {
			PropertyInfo propertyInfo = GetType ().GetProperty (key);
			if (propertyInfo.PropertyType == typeof(int)) {
				int iValue = int.Parse (_dict [key]);
				propertyInfo.SetValue (this, iValue, null);
			} else if (propertyInfo.PropertyType == typeof(string)) {
				propertyInfo.SetValue (this, _dict [key].Replace ("\"", ""), null);
			} else if (propertyInfo.PropertyType == typeof(double)) {
				propertyInfo.SetValue (this, double.Parse (_dict [key]), null);
			} else if (propertyInfo.PropertyType == typeof(float)) {
				propertyInfo.SetValue (this, float.Parse (_dict [key]), null);
			}
			else {
				Debug.LogError ("error type unknown");
			}
		}
	}



	public bool Equals( string _strWhere ){
		//Debug.Log (_strWhere);
		string[] test = _strWhere.Trim().Split (' ');
		int count = 0;
		foreach (string check in test) {
			//Debug.Log (string.Format ("{0}:{1}", count, check));
			count += 1;
		}

		bool bRet = true;

		for (int i = 0; i < test.Length; i+=4 ) {
			//Debug.Log (test [i]);
			PropertyInfo propertyInfo = GetType ().GetProperty (test [i]);
			if (propertyInfo.PropertyType == typeof(int)) {
				int intparam = (int)propertyInfo.GetValue (this, null);
				string strJudge = test [i + 1];
				int intcheck = int.Parse(test[i+2]);
				if (strJudge.Equals ("=")) {
					if (intparam != intcheck) {
						bRet = false;
					}
				} else if (strJudge.Equals ("!=")) {
					if (intparam == intcheck) {
						bRet = false;
					}
				} else {
				}
			}
		}
		return bRet;
	}

	*/
}


[System.Serializable]
public class DataMonster : CsvData<DataMonsterParam> {
	public const string FILENAME = "data/monster";

	public DataMonsterParam Select( int _iSerial ){
		foreach (DataMonsterParam param in list) {
			if (param.monster_serial == _iSerial) {
				return param;
			}
		}
		return new DataMonsterParam ();
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
	}

	//テーブル以下全て取ってくる
	public List<DataMonsterParam> SelectAll()
	{
		return DataManager.Instance.dataMonster.list;
	}

	new public List<DataMonsterParam> Select( string _strWhere = null ){
		List<DataMonsterParam> ret_list = new List<DataMonsterParam> ();
		foreach (DataMonsterParam data in DataManager.Instance.dataMonster.list) {
			if (data.Equals (_strWhere)) {
				ret_list.Add (data);
			}
		}
		return ret_list;
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

	public void Update( int _iMonsterSerial , int _iItemSerial , int _iCondition = -1 ){

		foreach (DataMonsterParam data in list) {
			if (data.monster_serial == _iMonsterSerial) {
				data.item_serial = _iItemSerial;
				if (_iCondition != -1) {
					data.condition = _iCondition;
				}
			}
		}
		return;
	}


	public void Update( int _iSerial , Dictionary<string , string > _dict ){
		//Debug.LogError (_iSerial);
		foreach (DataMonsterParam data in list) {
			//Debug.LogError (data.item_serial);
			if (data.monster_serial == _iSerial) {
				data.Set (_dict);
			}
		}
		return;
	}
}










