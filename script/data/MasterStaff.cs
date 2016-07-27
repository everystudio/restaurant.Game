using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MasterStaffParam : CsvDataParam{

	protected int m_staff_id;
	protected string m_name;
	protected int m_rarity;

	public int m_manner;
	public int m_footwork;
	public int m_cook;

	public int m_pay;

	public int staff_id { get{ return m_staff_id;} set{m_staff_id = value; } }
	public string name { get{ return m_name;} set{m_name = value; } }
	public int rarity { get{ return m_rarity;} set{m_rarity = value; } }

	public int manner { get{ return m_manner;} set{m_manner = value; } }
	public int footwork { get{ return m_footwork;} set{m_footwork = value; } }
	public int cook { get{ return m_cook;} set{m_cook = value; } }

	public int pay { get{ return m_pay;} set{m_pay = value; } }



}

public class MasterStaff : CsvData<MasterStaffParam> {
	public const string FILENAME = "master/staff";
	Dictionary<int, MasterStaffParam> dict = new Dictionary<int, MasterStaffParam> ();

	public override bool LoadMulti (string _strFilename)
	{
		bool bRet = base.LoadMulti (_strFilename);
		dict.Clear ();
		foreach (MasterStaffParam param in list) {
			dict.Add (param.staff_id, param);
		}
		return bRet;
	}

	public MasterStaffParam Get( int _iStaffId ){
		MasterStaffParam param;
		if (dict.TryGetValue (_iStaffId, out param) == false) {
			param = new MasterStaffParam ();
		}
		return param;
	}

	public static string GetIconName( int _iStaffId ){
		string strRet = "";
		strRet = string.Format ("texture/staff/staff_{0:D7}", _iStaffId);
		//Debug.LogError (strRet);
		return strRet;
	}

	public static int GetNextExp( int _iLevelNow ){
		if (_iLevelNow < 10) {
			return 30 + (_iLevelNow * 10);
		} else {
			return _iLevelNow * _iLevelNow;
		}
	}

}
