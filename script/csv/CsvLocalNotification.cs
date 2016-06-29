using UnityEngine;
using System.Collections;

public class CsvLocalNotificationParam : CsvDataParam
{
	public int id { get; private set; }
	public int type { get; private set; }
	public int second { get; private set; }
	public string title { get; private set; }
	public string message { get; private set; }
}

public class CsvLocalNotificationData : CsvData<CsvLocalNotificationParam> {

	private static readonly string FilePath = "csv/local_notification";
	public void Load() { Load(FilePath); }
}





