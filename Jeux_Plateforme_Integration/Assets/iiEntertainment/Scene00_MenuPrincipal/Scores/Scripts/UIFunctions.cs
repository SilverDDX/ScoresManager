using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFunctions : MonoBehaviour {
	public InputField input;
	public Text currentName;
	public ScoresManager ScoresManager;
	public Vector3 startPos;
	public GameObject ScoreList;
	public float posOffset;
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

	public void PrintScores(int index){
		
		int[] currentGameScores = new int[ScoresManager.nbScore];
		currentGameScores = ScoresManager.ReadValuesScores (index-1);

		string[] currentGameNames = new string[ScoresManager.nbScore];
		currentGameNames = ScoresManager.ReadNamesScores (index-1);


		for (int i = 0; i < ScoresManager.nbGames; i++) {
			GameObject score = ScoreList.transform.GetChild (i).gameObject;
			score.GetComponent<Text>().text = (i+1).ToString() + ". " + currentGameNames [i] + " : " + currentGameScores [i].ToString();
		 
		}


	}

	void Update(){
	
		if (Input.GetKeyDown (KeyCode.B)) {
			PrintScores (1);
		}
	
	}
}
