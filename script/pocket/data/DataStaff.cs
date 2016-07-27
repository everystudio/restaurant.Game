using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class DataStaffParam : CsvDataParam {

	public int m_staff_serial;
	public int m_staff_id;
	public int m_office_serial;
	public int m_item_serial;

	public int m_level;
	public int m_exp;
	public int m_role;
	public int m_training_type;
	public int m_training_last;

	public int m_manner;
	public int m_footwork;
	public int m_cook;

	public string m_setting_time;
	public string m_create_time;

	public int staff_serial { get{ return m_staff_serial;} set{m_staff_serial = value; } }
	public int staff_id { get{ return m_staff_id;} set{m_staff_id = value; } }
	public int office_serial { get{ return m_office_serial;} set{m_office_serial = value; } }
	public int item_serial { get{ return m_item_serial;} set{m_item_serial = value; } }

	public int level { get{ return m_level;} set{m_level = value; } }
	public int exp { get{ return m_exp;} set{m_exp = value; } }

	public UnityEventInt UpdateRole = new UnityEventInt ();
	public int role{
		get{
			return m_role;
		}
		set{
			m_role = value;
			UpdateRole.Invoke (m_role);
		}
	}
	public int training_type { get{ return m_training_type;} set{m_training_type = value; } }
	public int training_last { get{ return m_training_last;} set{m_training_last = value; } }

	public int manner { get{ return m_manner;} set{m_manner = value; } }
	public int footwork { get{ return m_footwork;} set{m_footwork = value; } }
	public int cook { get{ return m_cook;} set{m_cook = value; } }

	public string setting_time { get{ return m_setting_time;} set{m_setting_time = value; } }
	public string create_time { get{ return m_create_time;} set{m_create_time = value; } }
}


public class DataStaff : CsvData<DataStaffParam>
{
	public const string FILENAME = "data/staff";

	public enum ROLE
	{
		NONE		= 0,
		FLOOR		,
		ENTRANCE	,
		CHEF		,
		MAX			,
	}
	public enum TRAINING
	{
		NONE		= 0,
		BRUSH		,
		DUMBBELL	,
		SPOON		,
		DRESS		,
		RIBBON		,
		PAN			,
		MAX			,
	}

	public DataStaffParam Select( int _iStaffSerial ){
		return SelectOne (string.Format (" staff_serial = {0}", _iStaffSerial));
	}

	public int Update( int _iStaffSerial , Dictionary<string , string > _dictUpdate ){
		return Update (_dictUpdate, string.Format (" staff_serial = {0} ", _iStaffSerial));
	}

	public DataStaffParam UpdateGet(int _iStaffSerial , Dictionary<string , string > _dictUpdate){

		Update (_iStaffSerial, _dictUpdate);
		return Select (_iStaffSerial);

	}

	public static string GetIconRole( int _iRole ){
		string strRet = "";
		strRet = string.Format ("texture/staff/staff_role{0:D2}", _iRole);
		//Debug.LogError (strRet);
		return strRet;
	}
	public static string GetIconRoleMiddle( int _iRole ){
		string strRet = "";
		strRet = string.Format ("texture/staff/staff_role{0:D2}_m", _iRole);
		//Debug.LogError (strRet);
		return strRet;
	}

	public DataStaffParam Insert( int _iStaffId , int _iOfficeItemSerial , int _iCageSerial ){
		string strNow = TimeManager.StrNow ();

		DataStaffParam insert_data = new DataStaffParam ();
		insert_data.staff_serial = list.Count + 1;
		insert_data.staff_id = _iStaffId;
		insert_data.office_serial = _iOfficeItemSerial;
		insert_data.item_serial = _iCageSerial;
		insert_data.setting_time = strNow;
		insert_data.create_time = strNow;
		list.Add (insert_data);
		return insert_data;

		//m_iSetStaffId , m_dataOffice.item_serial , m_dispOffice.SelectingCageSerial

	}

}

