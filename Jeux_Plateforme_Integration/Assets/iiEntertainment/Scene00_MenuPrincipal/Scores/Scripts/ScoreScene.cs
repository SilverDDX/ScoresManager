using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreScene : MonoBehaviour {
	
    public int index;
    public int score=0;

	private ScoresManager ScoresManager;
    //Adds entered value to the current score

	void Start(){
		ScoresManager = GameObject.Find("[Games_Manager]").GetComponent<ScoresManager>();
	}
    public void AddValue(int val)
    {
        score += val;
    }

    //Gets the value of the current score
    public int GetValue(){
        return score;
    }

	void Update(){

		if(Input.GetKeyDown(KeyCode.A)){
			AddValue(5);
		}
	}

	//When Quitting Saves Score eventually
	private void OnApplicationQuit()
	{

	}
}
