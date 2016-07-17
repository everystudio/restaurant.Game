using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Button))]
public class BtnBackyardStart : MonoBehaviour {

	void Awake(){
		gameObject.GetComponent<Button>().onClick.AddListener (onBackyardStart);
	}

	void onBackyardStart(){
		UIAssistant.main.ShowPage ("EditBackyard");
	}

}




