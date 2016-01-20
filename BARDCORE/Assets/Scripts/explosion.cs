using UnityEngine;
using System.Collections;

public class explosion : MonoBehaviour {
	public GameObject triggerEffect;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision otherCollider){
		if(otherCollider.gameObject.tag =="Enemy"){
			Instantiate(triggerEffect, gameObject.transform.position, Quaternion.identity);
			//Destroy(otherCollider.gameObject);
			Destroy(gameObject);
		}
	}
}
