using UnityEngine;
using System.Collections;

namespace CompleteProject
{
	public class EnemyMovement : SpawnableMonobehaviour, IPoolable
	{
		Transform player;               // Reference to the player's position.
		Transform player2; 				// Reference to Player 2's position.
		//PlayerHealth playerHealth;      // Reference to the player's health.
		//EnemyHealth enemyHealth;        // Reference to this enemy's health.
		NavMeshAgent nav;               // Reference to the nav mesh agent.
		private float number;
		private float angle = 0;
		private int radius = 10;
		//public float stoppingDistance;
		public GameObject triggerEffect2;
		public GameObject gameOver;
		// NEW NEW NEW JUST ONE LINE BELOW
		//public int damagePerShot = 20;                  // The damage inflicted by each bullet.
		public int startingHealth = 100;            // The amount of health the enemy starts the game with.
		public int currentHealth;                   // The current health the enemy has.

		void Awake ()
		{
			// Set up the references.
			number = Random.value;
		
			currentHealth = startingHealth;

			if( number < .5f) 
			player = GameObject.FindGameObjectWithTag ("Player").transform;
			
			else 

			player = GameObject.FindGameObjectWithTag ("Player2").transform;


			//playerHealth = player.GetComponent <PlayerHealth> ();
			//enemyHealth = GetComponent <EnemyHealth> ();
			nav = GetComponent <NavMeshAgent> ();
		}
		
		
		void Update ()
		{
			nav.SetDestination (player.position);

			//put in the following if you want random range at which they stop, but it will override attack range
			//nav.stoppingDistance = Random.Range(1f,4f);

			// If the enemy and the player have health left...

				//if (gameObject.tag == "fire") { 
					//			NavMeshAgent.enabled = false;
						//}
			//(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
			//{
				//else {
								//NavMeshAgent.enabled = true;// ... set the destination of the nav mesh agent to the player.
								
						//}
			//}
			// Otherwise...
			//else
			//{
				// ... disable the nav mesh agent.
				//nav.enabled = false;
		
			//}

			if(currentHealth <= 0)
			{
				// ... the enemy is dead.
				Death ();
			}

				

		}
		void OnCollisionEnter(Collision otherCollider){
							
						if (otherCollider.gameObject.tag == "Projectile") {
				Debug.Log("collided with something");
				//currentHealth -= 20;
								
						}
			if (otherCollider.gameObject.tag == "Projectile2") {
				Debug.Log("collided with something");
				//currentHealth -= 10;
				
			}
				}
		void Death ()
		{
			// The enemy is dead.
			//isDead = true;
			
			// Turn the collider into a trigger so shots can pass through it.
			//capsuleCollider.isTrigger = true;
			
			// Tell the animator that the enemy is dead.
			//anim.SetTrigger ("Dead");
			Instantiate(triggerEffect2, gameObject.transform.position, Quaternion.identity);
			Destroy (gameObject);
			
			// Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
			//enemyAudio.clip = deathClip;
			//enemyAudio.Play ();
		}

		IEnumerator EnemyAgro () {
			yield return new WaitForSeconds(.2f);
			nav.stoppingDistance = 3f;
			nav.acceleration = -10f;
			nav.speed = -10f;
		}

		void OnTriggerEnter(Collider other) {
					if (other.gameObject.tag == "Player1Bubble" && number < .5f) {
				nav.stoppingDistance = 0f;
				nav.acceleration = 10f;
				nav.speed = 10f;
				StartCoroutine(EnemyAgro());
			}

			if (other.gameObject.tag == "Player2Bubble" && number >= .5f) {
				nav.stoppingDistance = 0f;
				nav.acceleration = 10f;
				nav.speed = 10f;
				StartCoroutine(EnemyAgro());
			}

			//if (other.gameObject.tag == "Player") {
							
				//Instantiate(triggerEffect2, other.gameObject.transform.position, Quaternion.identity);
				//NOT THISgameOver.gameObject.SetActive(true);
				//Application.LoadLevel("GameOver");
						//}
						//if (other.gameObject.tag == "Player2") {
								//Destroy (other.gameObject);
				//Instantiate(triggerEffect2, other.gameObject.transform.position, Quaternion.identity);
				//Application.LoadLevel("GameOver");
				//NOT THISgameOver.gameObject.SetActive(true);
			//}
			
			
				//NEW NEW NEW
			//if(other.gameObject.tag == "Projectile")
			//{
			//	EnemyHealth enemyHealth = gameObject.GetComponent <EnemyHealth> ();
				// ... the enemy should take damage.
			//	enemyHealth.TakeDamage (damagePerShot);
			//}
			//END OF NEW NEW NEW
						if (other.gameObject.tag == "shockwave") {
							//nav.enabled = false; 	
							//nav.enabled = true;

				Instantiate(triggerEffect2, gameObject.transform.position, Quaternion.identity);
				Despawn();// Destroy(gameObject);
				//nav.speed = 1;
						}
						//if (other.gameObject.tag == "Projectile") {
				//nav.speed = 0;	
				//gameObject.rigidbody.AddForce(110, 0, 0);
				//nav.enabled = false;
								//nav.enabled = true; 
						//}
						if (other.gameObject.tag == "Boundary") {
				Instantiate(triggerEffect2, gameObject.transform.position, Quaternion.identity);
				Despawn();//Destroy (gameObject);

				//gameObject.rigidbody.AddForce(110, 0, 0);
				//rigidbody.AddForce (0, 10, 0);
						}
				}

		
		public new IPoolable SpawnAt (Vector3 position, Quaternion rotation) {
			base.SpawnAt(position, rotation);
			return this;
		}
		
		//void OnDestroy() {
		//	Instantiate(triggerEffect2, other.gameObject.transform.position, Quaternion.identity);
		//}
		}


}