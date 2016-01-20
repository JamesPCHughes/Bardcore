using UnityEngine;
using System.Collections;

public class catchDetectorKiller : MonoBehaviour {
	public float catchCounter =1;
	public bool P2in;
	public bool P1in;
	public float lifeSpan;
	public static float powerCharge;

	void Awake () {
		P2in = PlayerDetectManager.p2IsIn;
		P1in = PlayerDetectManager.p1IsIn;
	}
	// Use this for initialization
	void Start () {
		powerCharge = 1;
	
	}
	
	IEnumerator powerLevel() {
		yield return new WaitForSeconds(10f);
		powerCharge = 1;


	}
	// Update is called once per frame
	void Update () {
		//if (Vector3.Distance(transform
		P2in = PlayerDetectManager.p2IsIn;
		P1in = PlayerDetectManager.p1IsIn;

		lifeSpan += Time.deltaTime;

		if (lifeSpan > ((60f/168f)*16)) {
				PlayerDetectManager.SetP2IsIn(false);
				PlayerDetectManager.SetP1IsIn(false);
		Destroy(gameObject);
			//PlayerDetectManager.SetP2IsIn(false);
			//PlayerDetectManager.SetP1IsIn (false);
				}

		if((P1in) && (P2in) && (powerCharge == 1)) {
			powerCharge =2;
			StartCoroutine(powerLevel());
		}
		/*if ((catchCounter > 0) && (catchCounter < 76) && (expand == true)) {
			catchCounter += .5f;
		}
		if (catchCounter > 75) {
			expand = false;
		}
		if (expand == false) {
			radius -= .5f;
		}
		if (radius < 0) {
			Destroy(gameObject);
		} */
	}

	void OnTriggerStay(Collider other) {
		if (other.gameObject.tag == "Player2") { 
			// By using {}, the condition apply to that entire scope, instead of the next line. 
			PlayerDetectManager.SetP2IsIn(true);
			}

			//else {
				//	PlayerDetectManager.SetP2IsIn (false);
				//		}
			

						//Destroy(other.gameObject);
				
		if (other.gameObject.tag == "Player") {
						PlayerDetectManager.SetP1IsIn (true);
				}

				//else {
				//	PlayerDetectManager.SetP1IsIn (false);
					//	}
			
				
	}

	void OnTriggerExit(Collider other) {
				if (other.gameObject.tag == "Player2") { 
						// By using {}, the condition apply to that entire scope, instead of the next line. 
						PlayerDetectManager.SetP2IsIn (false);
			
						//Destroy(other.gameObject);
				}
				if (other.gameObject.tag == "Player") {
						PlayerDetectManager.SetP1IsIn (false);
				}

		}
}
