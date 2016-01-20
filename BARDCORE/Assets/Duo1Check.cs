using UnityEngine;
using System.Collections;

public class Duo1Check : MonoBehaviour {
	public static bool Duo1Checker;
	// Use this for initialization
	void Start () {
		Duo1Checker = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		
		if (other.gameObject.tag == "Player") {
						Duo1Checker = true;
				}

		
		
		
		
		
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player") {
			Duo1Checker = false;		
		}
	}
}
