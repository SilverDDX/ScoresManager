using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour {
	public float fadeSpeed;
	public Image[] SpriteElements;
	private bool working;
	public GameObject LoadingUI;

	//Launches fading animation according to type 0 = fade in / 1 = fade out
	public void Fade(int type){
		//if fadeIn
		if(type==0){
			LoadingUI.SetActive (true);
			//resets elements colors to 0 to fade in from 0
			foreach(Image Element in SpriteElements){
				Element.color = new Color(Element.color.r, Element.color.g, Element.color.b, 0);
			}
			StartCoroutine (FadeIn ());
	
		} else if(type==1){
			
			//resets elements colors to 255 to fade out from 255
			foreach(Image Element in SpriteElements){
				Element.color = new Color(Element.color.r, Element.color.g, Element.color.b, 1);
			}
		

			//starts animation

			StartCoroutine (FadeOut());
		}

	}

	public IEnumerator FadeIn(){
		//Avoids conflicts between fadeIn and fadeOut
		if (!working) {
			working = true;
			//while elements aren't faded in keeps fading them
			while (SpriteElements [0].color.a <= 2) {

				foreach (Image Element in SpriteElements) {
					Element.color = new Color (Element.color.r, Element.color.g, Element.color.b, Element.color.a + fadeSpeed);
				}

		
				yield return null;
			}

		}
		working = false;
	
	}

	public IEnumerator FadeOut(){
		if (!working) {
			working = true;
			//while elements aren't faded in keeps fading them
			while (SpriteElements [0].color.a >= 0) {
				foreach (Image Element in SpriteElements) {
					Element.color = new Color (Element.color.r, Element.color.g, Element.color.b, Element.color.a - fadeSpeed);
				}
	
				yield return null;
			}
		}

		LoadingUI.SetActive (false);
		working = false;

	}

}
