using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFunctions : MonoBehaviour {
	public InputField input;
	public Text currentName;
	// Use this for initialization
	void Start () {
	//Keeps UI over scenes
		DontDestroyOnLoad (this.gameObject);
		if (FindObjectsOfType (GetType ()).Length > 1) {
			Destroy (this.gameObject);
		}
	}
	
	public void SetUserName(){
		currentName.text = input.text;
	}
}
