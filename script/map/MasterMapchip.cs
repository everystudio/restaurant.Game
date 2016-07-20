using UnityEngine;
using System.Collections;

[System.Serializable]
public class MasterMapchipParam : MasterMapchipBaseParam
{
	protected int m_comfortable;
	protected int m_decoration;

	public int comfortable { get{ return m_comfortable;} set{m_comfortable = value; } }
	public int decoration { get{ return m_decoration;} set{m_decoration = value; } }
}

public class MasterMapchip : CsvData<MasterMapchipParam> {
	public const string FILENAME = "master/mapchip";

	public MasterMapchipParam Get( int _itemId ){
		foreach (MasterMapchipParam param in list) {
			if (param.item_id == _itemId) {
				return param;
			}
		}
		return new MasterMapchipParam ();
	}


}
