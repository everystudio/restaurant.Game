using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MasterFoodmenuParam : CsvDataParam {

	protected int m_foodmenu_id;
	protected string m_food_category;
	protected string m_food_category_sub;
	protected string m_name;
	protected string m_description;
	protected int m_rarity;
	protected int m_coin;

	protected int m_vegetable;
	protected int m_meat;
	protected int m_fish;
	protected int m_seasoning;

	protected string m_season;
	protected string m_popular;
	protected string m_flavor;

	public int foodmenu_id { get{ return m_foodmenu_id;} set{m_foodmenu_id = value; } }
	public string food_category { get{ return m_food_category;} set{m_food_category = value; } }
	public string food_category_sub { get{ return m_food_category_sub;} set{m_food_category_sub = value; } }
	public string name { get{ return m_name;} set{m_name = value; } }
	public string description { get{ return m_description;} set{m_description = value; } }
	public int rarity { get{ return m_rarity;} set{m_rarity = value; } }
	public int coin { get{ return m_coin;} set{m_coin = value; } }

	public int vegetable { get{ return m_vegetable;} set{m_vegetable = value; } }
	public int meat { get{ return m_meat;} set{m_meat = value; } }
	public int fish { get{ return m_fish;} set{m_fish = value; } }
	public int seasoning { get{ return m_seasoning;} set{m_seasoning = value; } }

	public string season { get{ return m_season;} set{m_season = value; } }
	public string popular { get{ return m_popular;} set{m_popular = value; } }


}

public class MasterFoodmenu : CsvData<MasterFoodmenuParam > {
	public const string FILENAME = "master/foodmenu";
	Dictionary<int, MasterFoodmenuParam> dict = new Dictionary<int, MasterFoodmenuParam> ();

	public override bool LoadMulti (string _strFilename)
	{
		bool bRet = base.LoadMulti (_strFilename);
		dict.Clear ();
		if (bRet) {
			foreach (MasterFoodmenuParam param in list) {
				dict.Add (param.foodmenu_id, param);
			}
		}
		return bRet;
	}
	static public string GetIconFilename( int _foodmenuId ){
		string strFilename = "";
		strFilename = string.Format ("texture/food/foodmenu_{0:D4}_icon", _foodmenuId);
		return strFilename;
	}

	public MasterFoodmenuParam Get( int _foodmenuId ){
		MasterFoodmenuParam param;
		if (dict.TryGetValue (_foodmenuId, out param) == false ) {
			param = new MasterFoodmenuParam ();
		}
		return param;
	}
}

