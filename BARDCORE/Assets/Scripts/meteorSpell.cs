using UnityEngine;
using System.Collections;

public class meteorSpell : expirable {

	public GameObject blast;
	public int numberOfWaves = 3;
	public Transform spawnPoint;
	fader specialFXVignette;


	// Use this for initialization
	void Start () {
		specialFXVignette = GameObject.FindObjectOfType<fader>() as fader;
		specialFXVignette.toggleFading();
		StartCoroutine(burst ());

	}
	
	// Update is called once per frame
	void Update () {
		base.Update();
	}

	IEnumerator burst(){

		for(int i = 0; i<numberOfWaves; i++){
			Debug.Log("Wave #"+i);
			fireARound(i);
			if(i==numberOfWaves-1){

			}
			yield return new WaitForSeconds(.75f);
		}
	}

	void fireARound(int angleModifier){

		for(int i= 0; i<8; i++){
			int tempint = i*45+angleModifier*2;
			transform.Rotate(0,tempint,0);
			GameObject projectile = Instantiate(blast, spawnPoint.position, transform.rotation) as GameObject;
			projectile.GetComponent<fire>().goForward();
			//projectile.rigidbody.AddForce(Vector3.forward*100);
		}
	}

	void OnDestroy(){
		specialFXVignette.toggleFading();
	}
}
