﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DBPurchase {

	//テーブル名
    public const string TABLE_NAME = "iap";

	//テーブルデータ
	public class Data{
        public int id;
        public string spid;         //プロダクトID
        public string receipt;      //レシート情報
        public int status;          //課金ステータス 0:未検証,1:エラー,2:正常終了
        public string create_time;  //作成日
        public string update_time;  //更新日
	}

	//リスト
	public List<Data> data_list = new List<Data> ();
	//public List<TUser> user_data_list = new List<TUser>();

	//DBDataBaseのAsyncに直接代入
    public DBPurchase( string _strAsyncName ){}


    //DBへ保存
    public void Replace(Data _replocalData)
    {
        //データの上書きのコマンドを設定する　
        string strQuery = "REPLACE INTO " + TABLE_NAME + " (spid,receipt,status,create_time,update_time) VALUES( '" +
                          _replocalData.spid.ToString () + "','" +
                          _replocalData.receipt.ToString () + "'," +
                          _replocalData.status.ToString () + ",'" +
                          _replocalData.create_time.ToString () + "','" +
                          _replocalData.update_time.ToString ()   + "');";

        Debug.Log( "PurchaseReplaceQuery:"+strQuery);

    }

    //DBへ保存
    public void ReplaceStatus(int _status,int _id)
    {
        //ステータスの上書きのコマンドを設定する　
        string strQuery = "UPDATE " + TABLE_NAME + " SET status = "+ _status.ToString() + " WHERE id = " + _id;

        Debug.Log ("PurchaseReplaceStatusQuery : "+strQuery);

        //m_sqlDBはDBDataBaseのプロテクト変数
    }

    public List<Data> Select(Dictionary<string,object> _where = null){
	
		//データをクリア
		//user_data_list.Clear ();

		//テーブルからデータを調べる為のコマンドを設定
		string strQuery = "SELECT * FROM " + TABLE_NAME;
		if (_where != null)
		{
			strQuery += " WHERE ";	//コマンドをループ設定にする
			int loopCount = 0;
			int keyCount = _where.Keys.Count;
			foreach(var key in _where.Keys)
			{
				strQuery += key + " = ";		//条件追加
              //変数の型を調べる
                if (_where [key].GetType () == typeof(string)) {
                    strQuery+= "\""+(string)_where [key]+"\"";
                } else if (_where [key].GetType () == typeof(int)) {
                    strQuery+= ""+(int)_where [key];
                } else if (_where [key].GetType () == typeof(double)) {
                    strQuery+= ""+(double)_where [key];
                }
              else {
                  Debug.LogError (" [string] [int] [double]以外のタイプです : " + _where [key].GetType ());
              }
				loopCount++;
				if (keyCount != loopCount) {
					strQuery += "AND ";			//AND追加
				}
			}
		}

        Debug.Log ("PurchaseSelectQuery : "+strQuery);

		//m_sqlDBはDBDataBaseのプロテクト変数
//		if (_where != null)
//		{
//			foreach (var key in _where.Keys)
//			{
//				Debug.Log ("key : "+key);
//				//変数の型を調べる
//				if (_where [key].GetType () == typeof(string))		{ query.Bind ((string)_where [key]); } 
//				else if (_where [key].GetType () == typeof(int)) 	{ query.Bind ((int)_where [key]); } 
//				else if (_where [key].GetType () == typeof(double)) { query.Bind ((double)_where [key]); } 
//				else {
//					Debug.LogError (" [string] [int] [double]以外のタイプです : " + _where [key].GetType ());
//				}
//			}
//		}

        data_list.Clear ();


        return data_list;
	}

	//テーブル以下全て取ってくる
    public List<Data> SelectAll()
	{
		//データをクリア
        data_list.Clear ();

        //string strQuery = "SELECT * FROM "+TABLE_NAME;


        return data_list;
	}

    public List<Data> SelectUnverifiedData(){

        //データをクリア
		//user_data_list.Clear ();

        //テーブルからデータを調べる為のコマンドを設定
        string strQuery = "SELECT * FROM " + TABLE_NAME + " WHERE status != 2";

        Debug.Log ("PurchaseSelectQuery : "+strQuery);


        return data_list;
    }

}
