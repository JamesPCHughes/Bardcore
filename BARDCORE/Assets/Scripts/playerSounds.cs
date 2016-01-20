using UnityEngine;
using System.Collections;

public class playerSounds : MonoBehaviour {
	public AudioClip chargeSound;
	// Use this for initialization
	int counter;
	public int chargeSoundThreshold = 5;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		checkForCharge();
	}

	void checkForCharge(){
	if(Input.GetKey(KeyCode.RightShift)){
			counter++;
			if(counter==chargeSoundThreshold){
				GetComponent<AudioSource>().clip = chargeSound;
				GetComponent<AudioSource>().Play();
			}
		}
	if(Input.GetKeyUp(KeyCode.RightShift)){
			counter = 0;
			GetComponent<AudioSource>().Stop();
		}
	}
}
