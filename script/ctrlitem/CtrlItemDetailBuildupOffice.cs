using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CtrlItemDetailBuildupOffice : CtrlItemDetailBuildupBase {

	protected override string GetMainText ()
	{
		return "empower_office";
		//return "この施設を増強しますか";
	}

	override protected void buildup(){
		GoogleAnalytics.Instance.Log (DataManager.Instance.GA_BUILDUP_OFFICE);

		int iCostPre = m_dataItemParam.cost_max;
		int iCostAfter = m_dataItemParam.cost_max;

		int iNextLevel = m_dataItemParam.level + 1;

		Dictionary< string , string > dict = new Dictionary< string , string > ();
		dict.Add( "level" , iNextLevel.ToString() ); 

		/*
		CsvItemDetailData itemdetail;
		foreach (CsvItemDetailData csvData in DataManager.csv_item_detail) {
			if (m_dataItemParam.item_id == csvData.item_id && iNextLevel == csvData.level) {
				iCostAfter = csvData.cost;
				dict.Add ("cost_max", csvData.cost.ToString ()); 
				dict.Add ("revenue", csvData.revenue_rate.ToString ()); 
			}
		}
		*/

		// 増えた分おコストを計算
		GameMain.Instance.m_iCostNokori += (iCostAfter - iCostPre);

		DataManager.Instance.m_dataItem.Update( m_dataItemParam.item_serial , dict );

		m_dataItemParam = DataManager.Instance.m_dataItem.Select (m_dataItemParam.item_serial);

		dispUpdate (m_dataItemParam, ref m_dataNext);
	}


}
