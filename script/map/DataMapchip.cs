using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class DataMapchipParam : DataMapchipBaseParam{

	public int m_sample_int;
	public float m_sample_float;
	public string m_sample_string;

	public int sample_int { get{ return m_sample_int;} set{m_sample_int = value; } }
	public float sample_float { get{ return m_sample_float;} set{m_sample_float = value; } }
	public string sample_string { get{ return m_sample_string;} set{m_sample_string = value; } }


}


public class DataMapchip : DataMapchipBase<DataMapchipParam> {
	public const string FILENAME = "data/mapchip_restaurant";

	public List<DataMapchipParam> GetActiveList (){
		List<DataMapchipParam> ret = new List<DataMapchipParam> ();
		foreach (DataMapchipParam param in list) {
			if (0 <= param.x) {
				ret.Add (param);
			}
		}
		return ret;
	}

	public override bool Load (string _strFilename)
	{
		if (base.Load (_strFilename) == false ) {
			return LoadResources (_strFilename);
		}
		return true;
	}
	
}
