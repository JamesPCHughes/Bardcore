using UnityEngine;
using System.Collections;

public class fader : MonoBehaviour {

	public float targetAlphaValue = .5f;
	public float fadingRate = .01f;
	float currentAlphaValue = 0;
	bool alphaUp = false;
	//Material myMaterial;
	Color myColor;

	// Use this for initialization
	void Start () {

		myColor = GetComponent<Renderer>().material.color;
	}
	
	// Update is called once per frame
	void Update () {
		if(alphaUp && currentAlphaValue < targetAlphaValue){
			currentAlphaValue += fadingRate;
			if (currentAlphaValue > targetAlphaValue){
				currentAlphaValue = targetAlphaValue;
			}
		}

	if(!alphaUp && currentAlphaValue > 0){
			currentAlphaValue -= fadingRate;
			//currentAlphaValue = 0;
			if(currentAlphaValue<0){
				currentAlphaValue = 0;
			}

		}
		//Debug.Log("alpha value is: " +currentAlphaValue);
		myColor.a = currentAlphaValue;
		gameObject.GetComponent<Renderer>().material.color = myColor;


		if(Input.GetKeyDown(KeyCode.C)){
			toggleFading();
		}

	}

	public void toggleFading(){
		alphaUp = !alphaUp;
	}
}
