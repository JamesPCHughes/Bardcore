using UnityEngine;
using System.Collections;

public class jointLightAttack : MonoBehaviour {
	public GameObject triggerEffect;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision otherCollider){
	//	Debug.Log("collided with something");
		if(otherCollider.gameObject.tag=="Projectile"){
			if(otherCollider.gameObject.GetComponent<fire>().playerID==2){
			Instantiate(triggerEffect, gameObject.transform.position, Quaternion.identity);
			Destroy(otherCollider.gameObject);
			Destroy(gameObject);
			}
		}

	}
}
