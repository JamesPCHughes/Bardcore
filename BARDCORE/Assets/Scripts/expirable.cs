using UnityEngine;
using System.Collections;

public class expirable : MonoBehaviour {

	public float lifespan = 1.5f; //how long the shockwave will last
	public float counter;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public virtual void Update () {
		counter = counter+Time.deltaTime;
		if(counter>lifespan){
			Destroy(gameObject);
		}
	}
}
