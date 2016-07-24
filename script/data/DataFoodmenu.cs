using UnityEngine;
using System.Collections;

public class DataFoodmenuParam : CsvDataParam {

	protected int m_foodmenu_id;
	protected int m_status;
	protected int m_level;

	public int foodmenu_id { get{ return m_foodmenu_id;} set{m_foodmenu_id = value; } }
	public int status { get{ return m_status;} set{m_status = value; } }
	public int level { get{ return m_level;} set{m_level = value; } }
}

public class DataFoodmenu : CsvData<DataFoodmenuParam>{
	public const string FILENAME = "data/foodmenu";

	// 作られててかつステータスが０（unknown）じゃなければ登録済み
	// たぶん作られてて、のチェックは要らない
	public bool IsRegisterd(int _foodmenuId){
		bool bRet = false;
		foreach (DataFoodmenuParam param in list) {
			if (param.foodmenu_id == _foodmenuId && param.status != 0) {
				return true;
			}
		}
		return bRet;	// 実質false
	}

	// IDが存在する場合は作成済み
	public bool IsProduced(int _foodmenuId){
		bool bRet = false;
		foreach (DataFoodmenuParam param in list) {
			if (param.foodmenu_id == _foodmenuId) {
				return true;
			}
		}
		return bRet;	// 実質false
	}
}




