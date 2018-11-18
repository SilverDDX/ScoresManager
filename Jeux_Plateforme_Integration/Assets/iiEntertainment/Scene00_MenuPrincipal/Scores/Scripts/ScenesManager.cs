using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour {
	private ScoresManager ScoresManager;
	private ScoreScene currentScoreScene;
	public Loading Loading;
	public Slider[] LoadingValues;
	// Use this for initialization
	void Awake() {
		DontDestroyOnLoad (this.gameObject);
		if (FindObjectsOfType (GetType ()).Length > 1) {
			Destroy (this.gameObject);
		}
	}
	void Start () {
		ScoresManager = GetComponent<ScoresManager> ();
	}

	//Gets back to menu and saves the score
	public void BackToMenu(){
		//if we're not already in the menu
		if (SceneManager.GetActiveScene ().buildIndex != 0) {
			currentScoreScene = GameObject.Find("[Score_scene]").GetComponent<ScoreScene>();
			ScoresManager.SaveScore(SceneManager.GetActiveScene().buildIndex-1, currentScoreScene.score);

			LoadScene(0);
		}
	}

	void LoadScene(int index){
		//loads the scene
		StartCoroutine (LoadAsynchronously(index));
	}

	IEnumerator LoadAsynchronously(int index){
		//fading animation IN
		Loading.Fade (0);
		AsyncOperation operation = SceneManager.LoadSceneAsync(index);
		while (!operation.isDone) {
			float progress = Mathf.Clamp01 (operation.progress/.9f);
			Debug.Log ("current : " + progress);
			foreach(Slider LoadingValue in LoadingValues){
				LoadingValue.value = progress;
			}
			yield return null;
		}
		//fading animation OUT
		Loading.Fade(1);
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
