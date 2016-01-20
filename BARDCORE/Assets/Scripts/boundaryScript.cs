using UnityEngine;
using System.Collections;

public class boundaryScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTriggerEnter(Collider other){
		Debug.Log ("colliding with: "+other.tag);
		if(other.tag == "Player"){
			Application.LoadLevel(2);
		}
		
	}

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "Player") {
			//	Destroy (other.gameObject);
			//Instantiate(triggerEffect2, other.gameObject.transform.position, Quaternion.identity);
			//gameOver.gameObject.SetActive(true);
			Application.LoadLevel("GameOver");
		}
		if (other.gameObject.tag == "Player2") {
			//Destroy (other.gameObject);
			//Instantiate(triggerEffect2, other.gameObject.transform.position, Quaternion.identity);
			Application.LoadLevel("GameOver");
			//gameOver.gameObject.SetActive(true);
		}
	}
}
