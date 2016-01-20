using UnityEngine;
using System.Collections;

public class BeatTimer : MonoBehaviour {

	const float Beat_time = 60f/120f; //seconds per beat. Murder = 168 BPM, +2 to click track ie click track = 120, put 122 here.
	private float time = Beat_time;
	public float margin = 0f; 
	public bool onBeat; 
	public static float counter;
	public bool countCheck;
	public bool comboTime;


	// Use this for initialization
	void Start () {
		counter = 1;
		countCheck = true;
		comboTime = false;
		onBeat = true;
		gameObject.transform.localScale = new Vector3 (7, 7, 7);
	}
	
	// Update is called once per frame
	void Update () {
		if (counter == 1) {
			gameObject.transform.localScale = new Vector3(7,7,7);
			onBeat = true;
		}
		if (counter == 2) {
			gameObject.transform.localScale = new Vector3(1,1,1);
			onBeat = false;
		}
		if (counter == 3) {
			gameObject.transform.localScale = new Vector3(2,2,2);
			onBeat = false;
		}
		if (counter == 4) {
			gameObject.transform.localScale = new Vector3(3,3,3);
			onBeat = true;
		}
		
		if (counter > 4) {
			counter = 1;
		}
				time -= Time.deltaTime;

				if (time <= 0f) {
						// do whatever
						time = Beat_time;
			print ("resetting time to "+time);
				}
		if ((time >= Beat_time - margin) || (time <= 0f + margin)) {
			//onBeat = true; switched with comboTime 
			comboTime = true;
			print ("time is "+time+"; onBeat is true");
			//gameObject.transform.localScale = new Vector3(3,3,3);
			//time = Beat_time;
			if (countCheck == true) {
				counter++;
				countCheck = false;
				}

		}
		//else { onBeat = true; }
		else { 
			//onBeat = false; switched with comboTime
			comboTime = false;
			countCheck = true;
			print ("time is "+time+"; onBeat is false");
			//gameObject.transform.localScale = new Vector3(1,1,1);
		}



	}
}


//bool onBeat(float m) {
	//return 
	//	time <= m || Beat_time - m;
	//time > 0 && time < Margin;
//}
