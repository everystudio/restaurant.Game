using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DataFoodmenuParam : CsvDataParam {

	public enum STATUS
	{
		NONE		= 0,
		UNKNOWN		,
		UNPRODUCED	,
		PRODUCED	,
		REGISTERD	,

		MAX			,
	}

	protected int m_foodmenu_id;
	protected int m_status;
	protected int m_level;

	public int foodmenu_id { get{ return m_foodmenu_id;} set{m_foodmenu_id = value; } }
	public int status { get{ return m_status;} set{m_status = value; } }
	public int level { get{ return m_level;} set{m_level = value; } }
}

public class DataFoodmenu : CsvData<DataFoodmenuParam>{
	public const string FILENAME = "data/foodmenu";

	Dictionary<int, DataFoodmenuParam> dict = new Dictionary<int, DataFoodmenuParam> ();
	public override bool Load (string _strFilename)
	{
		bool bRet = base.Load (_strFilename);
		dict.Clear ();
		foreach (DataFoodmenuParam param in list) {
			dict.Add (param.foodmenu_id, param);
		}
		return bRet;
	}

	public DataFoodmenuParam Get( int _foodmenuId ){
		DataFoodmenuParam param;
		if (!dict.TryGetValue (_foodmenuId, out param)) {
			param = new DataFoodmenuParam ();
		}
		return param;
	}

	// 作られててかつステータスが０（unknown）じゃなければ登録済み
	// たぶん作られてて、のチェックは要らない
	public bool IsRegisterd(int _foodmenuId){
		bool bRet = false;

		DataFoodmenuParam param;
		if (dict.TryGetValue (_foodmenuId, out param)) {
			if (param.status == (int)DataFoodmenuParam.STATUS.REGISTERD) {
				bRet = true;
			}
		}
		return bRet;	// 実質false
	}

	// IDが存在する場合は作成済み
	public bool IsProduced(int _foodmenuId){
		bool bRet = false;

		DataFoodmenuParam param;
		if (dict.TryGetValue (_foodmenuId, out param)) {
			if ((int)DataFoodmenuParam.STATUS.PRODUCED <= param.status) {
				bRet = true;
			}
		}
		return bRet;	// 実質false
	}
	public void Produce( int _foodmenuId){

		DataFoodmenuParam param;
		if (dict.TryGetValue (_foodmenuId, out param)) {
			param.status = (int)DataFoodmenuParam.STATUS.PRODUCED;
			param.level += 1;
		} else {
			param = new DataFoodmenuParam ();
			param.foodmenu_id = _foodmenuId;
			param.status = (int)DataFoodmenuParam.STATUS.PRODUCED;
			param.level = 1;
			dict.Add (_foodmenuId, param);
			list.Add (param);				// 一応ね
		}


	}

}




