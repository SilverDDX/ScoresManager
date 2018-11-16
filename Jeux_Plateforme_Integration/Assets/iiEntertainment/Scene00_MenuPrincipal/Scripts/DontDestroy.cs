using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this.gameObject);
		if (FindObjectsOfType (GetType ()).Length > 1) {
			Destroy (this.gameObject);
		}
	}
}
