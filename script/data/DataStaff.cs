using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class DataStaffParam : CsvDataParam {

	public int m_staff_serial;
	public int m_office_serial;
	public int m_staff_id;
	public int m_item_serial;
	public string m_setting_time;
	public string m_create_time;

	public int staff_serial { get{ return m_staff_serial;} set{m_staff_serial = value; } }
	public int office_serial { get{ return m_office_serial;} set{m_office_serial = value; } }
	public int staff_id { get{ return m_staff_id;} set{m_staff_id = value; } }
	public int item_serial { get{ return m_item_serial;} set{m_item_serial = value; } }
	public string setting_time { get{ return m_setting_time;} set{m_setting_time = value; } }
	public string create_time { get{ return m_create_time;} set{m_create_time = value; } }


	public int GetShisyutsuParHour(){
		int iRet = 0;
		CsvStaffParam staff_csv = DataManager.GetStaff (staff_id);

		int iCount = 3600 / staff_csv.expenditure_interval;
		iRet += staff_csv.expenditure * iCount;

		return iRet;
	}


	public int GetPayGold( bool _bCollect ){

		CsvStaffParam csv_staff = DataManager.GetStaff (staff_id);

		double diffSec = TimeManager.Instance.GetDiffNow (setting_time).TotalSeconds * -1.0d;

		double dCount = diffSec / csv_staff.expenditure_interval;

		double dRet = dCount * csv_staff.expenditure;
		if (_bCollect && 1 < dRet ) {

			int iAmari = (int)diffSec % csv_staff.expenditure_interval;

			//Debug.LogError( string.Format( "amari:{0} diffSec:{1} interval:{2}" , iAmari , diffSec, csv_staff.expenditure_interval));
			string strResetTime = TimeManager.StrGetTime (iAmari * -1);
			strResetTime = TimeManager.StrGetTime ();
			//Debug.LogError (TimeManager.Instance.GetOffsetTime (setting_time, iAmari));

			Dictionary< string , string > dict = new Dictionary< string , string > ();
			dict.Add ("setting_time", "\"" + strResetTime + "\"");
			DataManager.Instance.dataStaff.Update (staff_serial, dict);
			/*
			foreach (DataStaffParam param in DataManager.Instance.dataStaff.list) {
				Debug.LogError (string.Format ("staff_serial:{0} setting_time:{1} strResetTime:{2}", param.staff_serial, param.setting_time , strResetTime));
			}
			*/
		}
		//int iCount = 

		return (int)dRet;
	}
}


public class DataStaff : CsvData<DataStaffParam>
{
	public const string FILENAME = "data/staff";

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

