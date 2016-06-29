using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CsvWorkParam : CsvDataParam {

	public int work_id { get; set; }
	public string title { get; set; }
	public string description { get; set; }
	public int type { get; set; }
	public int level { get; set; }
	public int appear_work_id { get; set; }
	public int exp { get; set; }
	public string difficulty { get; set; }
	public int prize_ticket { get; set; }
	public int prize_coin{ get; set; }
	public int prize_monster { get; set; }
	public int mission_level{ get; set; }
	public int mission_monster{ get; set; }
	public int mission_staff{ get; set; }
	public int mission_item{ get; set; }
	public int mission_collect{ get; set; }
	public int mission_tweet{ get; set; }
	public int mission_login{ get; set; }
	public int mission_num{ get; set; }
	public string skit_id{ get; set; }
}

public class CsvWork : CsvData<CsvWorkParam>
{
	private static readonly string FilePath = "csv/work";
	public void Load() { Load(FilePath); }


	protected override CsvWorkParam makeParam (List<SpreadSheetData> _list, int _iSerial, int _iRow)
	{
		int index = 1;
		SpreadSheetData data_workid = SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		//SpreadSheetData data_status = SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_title = SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_description = SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_type= SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_level_= SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_appearworkid = SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_exp= SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_difficulty= SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_prizeticket= SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_prizecoin= SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_prizemonster= SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);

		SpreadSheetData data_missionlevel= SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_missionmonster= SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_missionstaff= SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_missionitem= SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_missioncollect= SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_missiontweet= SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_missionlogin= SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);
		SpreadSheetData data_missionnum= SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);

		SpreadSheetData data_skitid= SpreadSheetData.GetSpreadSheet( _list, _iRow , index++);


		CsvWorkParam retParam = new CsvWorkParam ();

		retParam.work_id = int.Parse(data_workid.param);
		//retParam.status;
		retParam.title = data_title.param;
		retParam.description = data_description.param;
		retParam.type= int.Parse(data_type.param);
		retParam.level= int.Parse(data_level_.param);
		retParam.appear_work_id= int.Parse(data_appearworkid.param);
		retParam.exp= int.Parse(data_exp.param);
		retParam.difficulty= data_difficulty.param;
		retParam.prize_ticket= int.Parse(data_prizeticket.param);
		retParam.prize_coin= int.Parse(data_prizecoin.param);
		retParam.prize_monster= int.Parse(data_prizemonster.param);

		retParam.mission_level= int.Parse(data_missionlevel.param);
		retParam.mission_monster= int.Parse(data_missionmonster.param);
		retParam.mission_staff= int.Parse(data_missionstaff.param);
		retParam.mission_item= int.Parse(data_missionitem.param);
		retParam.mission_collect= int.Parse(data_missioncollect.param);
		retParam.mission_tweet= int.Parse(data_missiontweet.param);
		retParam.mission_login= int.Parse(data_missionlogin.param);
		retParam.mission_num= int.Parse(data_missionnum.param);
		retParam.skit_id= data_skitid.param;

		return retParam;

	}

}



