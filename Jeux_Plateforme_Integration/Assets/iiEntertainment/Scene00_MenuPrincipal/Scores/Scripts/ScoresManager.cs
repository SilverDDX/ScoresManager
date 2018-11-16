using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoresManager : MonoBehaviour {

    // Use this for initialization
    public TextAsset scoreFile;
    public int nbScore = 8;
    public int nbGames = 11;
    public string arborescence = "/ScoresFile";
	private string container;
	public Text username;
	private string[] usernames;
   
   
    private int[,] scoresValues;
	private string[,] scoresNames;
    public string[] namesGames;
	string[] splitted, splittedScores, splittedNames;
	void Awake(){
		DontDestroyOnLoad (this.gameObject);
	}
	void Start () {
		
        arborescence = Application.dataPath + arborescence;
		scoresValues = new int[nbGames, nbScore];
		scoresNames = new string[nbGames, nbScore];
	
        //DefaultScores();
        ResetScores();
        //LoadScoresFromFile();
     }



	//Loads the registered scores from text file
    public void LoadScoresFromFile()
    {
		//Loads and split external file
		SplitScoreFile ();

		for (int i = 0; i < nbGames; i++)
		{	int[] scoreBuffer;
			string[] nameBuffer;

			//Reads values in splitted string arrays
				scoreBuffer = ReadValuesScores(i*nbScore);
				nameBuffer = ReadNamesScores(i*nbScore);

			Debug.Log (namesGames[i]);
			//Fills Names and Values matrices
            for (int j = 0; j < nbScore; j++)
			{
				scoresValues[i, j] = scoreBuffer[j];
				scoresNames[i, j] = nameBuffer[j];
				Debug.Log (scoresNames[i,j]);
				Debug.Log (scoresValues[i,j]);

            }
				
        }

    }
		
	//Fills default scores for testing purpose
	private void DefaultScores(){

		for (int i = 0; i < nbGames; i++)
		{
			namesGames[i] =  "name"+ i.ToString();

			for (int j = 0; j < nbScore; j++)
			{
				scoresValues[i, j] = j;
				scoresNames [i,j] = "score " + j;
			}


		}
	
	}

    //Resets Scores to 0
    private void ResetScores()
    {

        for (int i = 0; i < nbGames; i++)
        {

            for (int j = 0; j < nbScore; j++)
            {
                scoresValues[i, j] = 0;
                scoresNames[i, j] = "Jarvis";
            }


        }

    }

    //Overrides and saves Scores in external file
    private void WriteScoresInFile()
    {
        using (StreamWriter outputFile = new StreamWriter(Path.Combine(arborescence, "Scores.txt"), false))
        {
			
            for (int i = 0; i < nbGames; i++)
            {
				outputFile.WriteLine(namesGames[i]);
              
                for (int j = 0; j < nbScore; j++)
                {
					outputFile.WriteLine(scoresValues[i,j].ToString() + "," + scoresNames[i,j]);

                }
				outputFile.Write("\r\n", "\n");
              
            }
        }
    }
		

	//prints string array in console for testing
	void DebugStringArray(string[] array){
		foreach (string line in array) {
			Debug.Log (line);
		}
	}


	//splits the score text file twice, per lines and then with "," separating score from username
	private void SplitScoreFile(){
		
		//First split by line
		container = scoreFile.text;
		splitted = container.Split(new string[] {
			"\r\n", "\n",
		}, System.StringSplitOptions.None);

		//initialisasing arrays sizes
		splittedNames = new string[nbGames * nbScore];
		splittedScores = new string[nbGames*nbScore];


		//splitted line array
		string[] splittedLine = new string[2];
		//countdown value for correct indexing
		int l = 0;

		//Second split of each lines
		//Starting at 1 to skip first line
		for (int j = 1; j < 1 + (nbScore*nbGames) + 2*nbGames; j = j + nbScore + 2) {
			for (int k = 0; k < nbScore; k++) {
				splittedLine = splitted [j + k].Split (new string[] {
					","
				}, System.StringSplitOptions.None);
				splittedScores [l] = splittedLine [0];
                splittedNames[l] = splittedLine[1];
				l++;
				}
			}

		//Fills the array containing Games names
		int m = 0;
		for (int j = 0; j < (nbScore * nbGames) + 2 * nbGames; j = j + nbScore + 2) {
			namesGames [m] = splitted [j];
			m++;
		}

		//Debug for testing purpose

		/*Debug.Log (splittedNames.Length);
		Debug.Log (splittedScores.Length);
		DebugStringArray (splittedScores);
		DebugStringArray (splittedNames);*/
		}


	//Reads Highscores in external file according to index value of the game
    private int[] ReadValuesScores(int index)
    {
		int[] currentScores = new int[nbScore];

			for (int i = index; i < index + nbScore; i++) {
			int.TryParse (splittedScores [i], out currentScores [i - index]);
			}

        return currentScores;
    }

	//Reads Highscores Name of the Game in external file according to index value of the game
	private string[] ReadNamesScores(int index)
    {
		string[] currentName = new string[nbScore];

		for (int i = index; i < index + nbScore; i++) {
			currentName [i - index] = splittedNames [i];
		}

        return currentName;
    }
				
	public void SaveScore(int index, int score){
		bool overrided = false;
		for(int i = index ; i<(index+nbScore); i++){
			if((score >= scoresValues[index, i-index]) && (!overrided)){
                int saveValue;
                string saveName;

                int lastValue = scoresValues[index, i - index];
                string lastName = scoresNames[index, i - index];

                for (int j = i; j < index+nbScore -1; j++) {

                    saveValue = scoresValues[index, j + 1 - index];
                    saveName = scoresNames[index, j + 1 - index];     

                    if (j == i)
                    {
                        scoresValues[index, j + 1 - index] = scoresValues[index, j - index];
                        scoresNames[index, j + 1 - index] = scoresNames[index, j - index];
                    }
                    else {
                        scoresValues[index, j + 1 - index] = lastValue;
                        scoresNames[index, j + 1 - index] = lastName;
                    }

                    lastValue = saveValue;
                    lastName = saveName;

                }
				scoresValues [index, i-index] = score;
				scoresNames [index, i-index] = username.text.Replace("\n", "").Replace("\r\n", "");
				overrided = true;
				}
			}
		}

	
//When Quitting Saves Scores
private void OnApplicationQuit ()
	{
		if (SceneManager.GetActiveScene ().buildIndex != 0)
			SaveScore (SceneManager.GetActiveScene ().buildIndex-1, GameObject.Find ("[Score_scene]").GetComponent<ScoreScene> ().score);

		WriteScoresInFile ();
	}
}