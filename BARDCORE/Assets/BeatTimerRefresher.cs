using UnityEngine;
using System.Collections;

public class BeatTimerRefresher : MonoBehaviour {
	public bool isPhoenixRunning;
	public GameObject beatsByJames;

	// Use this for initialization
	void Start () {
		//Instantiate (beatsByJames);
		//BeatTimer.SetActive = true;
		isPhoenixRunning = false;

	}


	// Update is called once per frame
	void Update () {


		if (isPhoenixRunning == false) {
			StartCoroutine("Phoenix");		
		}

	}

	
	IEnumerator Phoenix(){
		isPhoenixRunning = true;
		yield return new WaitForSeconds(11f);

		beatsByJames.SetActive (false);
		yield return new WaitForSeconds(1f);
		beatsByJames.SetActive (true);
		BeatTimer.counter = 1;

	
		isPhoenixRunning = false;
	}
}
