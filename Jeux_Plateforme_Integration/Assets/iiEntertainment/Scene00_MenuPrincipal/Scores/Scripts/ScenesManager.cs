using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour {
	private ScoresManager ScoresManager;
	private ScoreScene currentScoreScene;
	// Use this for initialization
	void Awake(){
		
	}
	void Start () {

		//Destroys the GameObject if it already exists
		DontDestroyOnLoad (this.gameObject);
		if (FindObjectsOfType (GetType ()).Length > 1) {
			Destroy (this.gameObject);
		}
		ScoresManager = GetComponent<ScoresManager> ();
	}
	
	public void BackToMenu(){
		currentScoreScene = GameObject.Find("[Score_scene]").GetComponent<ScoreScene>();
		ScoresManager.SaveScore(SceneManager.GetActiveScene().buildIndex-1, currentScoreScene.score);
		if(SceneManager.GetActiveScene().buildIndex !=0)
		SceneManager.LoadScene(0);
	}

	void LoadScene(int index){
		SceneManager.LoadScene(index);
	}

	void Update(){
		
		if (Input.GetKeyDown(KeyCode.Z)) {
			Debug.Log ("backmenu");
			BackToMenu ();
		}

	
		if (Input.GetKeyDown(KeyCode.E)) {
			Debug.Log ("sc1");
			LoadScene (1);
		}

		if (Input.GetKeyDown(KeyCode.R)) {
			Debug.Log ("sc2");
			LoadScene (2);
		}


	}
}
