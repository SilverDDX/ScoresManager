using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VotreScript : MonoBehaviour {
	private ScoreScene ScoreScene;

	// Use this for initialization
	void Start () {
		ScoreScene = GameObject.Find ("[ScoreScene]").GetComponent<ScoreScene> ();
	}
	
	// Update is called once per frame
	void Update () {
		

	}
}


/*

Fonctions de modification du score :

 	ScoreScene.AddValue(int val); ==> ajoute une valeur définie au score. Peut être négative (malus).

	int scoreValue;
	scoreValue = ScoreScene.GetValue(); ==> récupère la valeur du score.

	Lorsque l'on retourne au menu principal, le score est automatiquement comparé avec les highscores (qui sont au nombre de 8).
	et vient remplacer le premier qu'il dépasse.

*/