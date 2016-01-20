using UnityEngine;
using System.Collections;

public class fire : expirable {
	
	Vector3 directionVector;
	public float defaultSpeed = 1;
	public AudioClip[] mySounds;
	public bool chargedShot = false;
	public int playerID;

	// Use this for initialization
	void Start () {
		Debug.Log("charged shot status is: "+chargedShot);
		AudioSource myAS = gameObject.GetComponent<AudioSource>();

		if(myAS!=null){
		if(chargedShot){
			//audio.clip = mySounds[1];
		}
		else{
			//audio.clip = mySounds[0];
		}
		GetComponent<AudioSource>().Play();
	}
}

	
	// Update is called once per frame
	void Update () {
		base.Update();

	}

	public void setDirection(int xdirection, int zdirection, int projectileSpeed){

		directionVector = new Vector3(xdirection * projectileSpeed,0,zdirection*projectileSpeed);
		//directionVector = new Vector3(xdirection * projectileSpeed,0,zdirection*projectileSpeed);
		GetComponent<Rigidbody>().velocity = directionVector;
	}

	public void goForward(){
		GetComponent<Rigidbody>().velocity = transform.forward*defaultSpeed;
	}

	/*void OnCollisionEnter(Collision other){
		Debug.Log("collided with: "+other.gameObject.tag);
	if(other.gameObject.tag=="Enemy" && chargedShot==false){
			Debug.Log("should destroy self");
			Destroy(gameObject);
		}
	}*/
}
