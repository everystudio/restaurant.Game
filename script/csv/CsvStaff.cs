using UnityEngine;
using System.Collections;

public class CsvStaffParam : CsvDataParam {
	public int staff_id { get; private set; }
	public string name { get; private set; }
	public string description { get; private set; }
	public int cost { get; private set; }
	public int coin { get; private set; }
	public int ticket { get; private set; }
	public int expenditure { get; private set; }
	public int expenditure_interval { get; private set; }
	public int effect_param { get; private set; }
	public int effect_num { get; private set; }

}

public class CsvStaffData : CsvData<CsvStaffParam>
{
	public const string FilePath = "csv/staff";
	public void Load() { Load(FilePath); }


}



//staff_id	name	description	cost	coin	ticket	expenditure	expenditure_interval	effect_param	effect_num




