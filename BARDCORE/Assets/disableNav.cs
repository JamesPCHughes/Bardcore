using UnityEngine;
using System.Collections;

public class disableNav : MonoBehaviour {



	// Use this for initialization
	void Start () {
		gameObject.GetComponent<NavMeshAgent> ().enabled = true;
		
		(GetComponent ("EnemyMovement") as MonoBehaviour).enabled = true;
		//scriptE = GetComponent<EnemyMovement>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator enemyMoveCooldown() {
		yield return new WaitForSeconds(10f);
		gameObject.GetComponent<NavMeshAgent> ().enabled = true;
		
		(GetComponent ("EnemyMovement") as MonoBehaviour).enabled = true;
	}
	void OnCollisionEnter(Collision collision) {

		if (collision.gameObject.tag == "Projectile") {
						gameObject.GetComponent<NavMeshAgent> ().enabled = false;
						
						(GetComponent ("EnemyMovement") as MonoBehaviour).enabled = false;

			StartCoroutine(enemyMoveCooldown());
				}
	

			
			
		
	}

	void OnTriggerEnter(Collider other) {

		if (other.gameObject.tag == "Killbox") {
			print("Passing through Killbox"); 
			Destroy (gameObject);	
		}

		if ((other.gameObject.tag == "Edge")) {
			gameObject.GetComponent<NavMeshAgent> ().enabled = false;
			
			(GetComponent ("EnemyMovement") as MonoBehaviour).enabled = false;
			
			StartCoroutine(enemyMoveCooldown());
			
		}


	}
}
