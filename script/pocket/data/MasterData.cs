using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MasterData : MonoBehaviour {

	static MasterData instance = null;

	public static MasterData Instance {
		get {
			if (instance == null) {
				GameObject obj = GameObject.Find ("MasterData");
				if (obj == null) {
					obj = new GameObject("MasterData");
					//Debug.LogError ("Not Exist AtlasManager!!");
				}
				instance = obj.GetComponent<MasterData> ();
				if (instance == null) {
					//Debug.LogError ("Not Exist AtlasManager Script!!");
					instance = obj.AddComponent<MasterData>() as MasterData;
				}
				instance.initialize ();
			}
			return instance;
		}
	}

	private void initialize(){
		DontDestroyOnLoad(gameObject);
		return;
	}

}












