using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlayerWonController : playerController {



	public BeatTimer beat;
	public bool beatbool = false; 
	public GameObject P2GameObject;
	public GameObject DodgePoint1;
	
	public Material material1;
	public Material material2;
	public Material material3;
	public Renderer rend;
	public AudioSource Drums;
	public AudioSource Bass;
	public AudioSource Guitar;
	public AudioSource Vox;
	public GameObject Sword;

	public bool P2inCopy;
	public bool P1inCopy;

	public static bool p1Attacking;
	public bool p2AttackingCopy;
	public static bool p1HoldingA;
	public static bool p1HoldingB;
	public static bool p1HoldingX;
	public static bool p1HoldingY;
	public static bool p1HoldingLB;
	public static bool p1HoldingRB;

	public GameObject EnergyYP1;
	public GameObject EnergyBP1;
	public GameObject EnergyRB1;

	
	public bool p2HoldA;
	public bool p2HoldB;
	public bool p2HoldX;
	public bool p2HoldY;
	public bool p2HoldLB;
	public bool p2HoldRB;
	//public GameObject Sword2;
	private Animator myAnimator;
	private float Saiyan1;
	public bool FirstHit;
	public bool ExtraHit;
	public bool ExtraHit1;
	public int Jab1;
	public int ComboCount;

	//private bool Saiyan2;
	//private bool Saiyan3;
	//private bool Saiyan4;
	private Rigidbody rb;	
	// Use this for initialization

	void Awake () {
		p2AttackingCopy = playerTwoController.p2Attacking;
		P2inCopy = PlayerDetectManager.p2IsIn;
		P1inCopy = PlayerDetectManager.p1IsIn;

		p2HoldA = playerTwoController.p2HoldingA;
		p2HoldB = playerTwoController.p2HoldingB;
		p2HoldX = playerTwoController.p2HoldingX;
		p2HoldY = playerTwoController.p2HoldingY;
		p2HoldLB = playerTwoController.p2HoldingLB;
		p2HoldRB = playerTwoController.p2HoldingRB;
	}

	void Start () {
		//rend = GetComponent<Renderer>();
 		AudioSource audio = GetComponent<AudioSource>();
		beat = FindObjectOfType<BeatTimer> ();
		FirstHit = true;
		ExtraHit1 = false; 
		ExtraHit = false; 
		rend.enabled = true;
		Jab1 = 1;
		myAnimator = GetComponentInChildren<Animator>();
		P2GameObject = GameObject.FindGameObjectWithTag("Player2");

		

		p1Attacking = false;

		base.Start();
		playerNumber=2;

		Drums.volume = (0.0f);
		Bass.volume = (0.0f);
		Guitar.volume = (0.0f);
		Vox.volume = (0.0f);
	
		Saiyan1 = 0;
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

		p2HoldA = playerTwoController.p2HoldingA;
		p2HoldB = playerTwoController.p2HoldingB;
		p2HoldX = playerTwoController.p2HoldingX;
		p2HoldY = playerTwoController.p2HoldingY;
		p2HoldLB = playerTwoController.p2HoldingLB;
		p2HoldRB = playerTwoController.p2HoldingRB;
		P2inCopy = PlayerDetectManager.p2IsIn;
		P1inCopy = PlayerDetectManager.p1IsIn;
		p2AttackingCopy = playerTwoController.p2Attacking;

		//REACTIVATE FOR BEAT DETECTION
		//beatbool = beat.onBeat; 
		beatbool = true;

		if (Saiyan1 < 1) {
						Drums.volume = (0.0f);
						Bass.volume = (0.0f);
						Guitar.volume = (0.0f);
						Vox.volume = (0.0f);
				}

		//if(animation_bool == true)
		//{
		//	animation.Play("SwordSwing");
			
		//}
		base.Update();
	}


	public override void comboCheck(){
		//if(comboable){
		base.comboCheck();
		Debug.Log("Player 2 combochecking");
		
		if(comboCooldown.cooldown>0){
			comboable=false;
		}

		if(otherPlayer.GetComponent<playerOneController>().comboable==true  && comboDistance.maxComboDistance>comboDistance.distanceBetweenPlayers){
				Debug.Log("Combo!");
				movementCooldown=2.5f;
				otherPlayer.GetComponent<playerOneController>().movementCooldown=2.5f;

				comboController.castMeteor();
				comboCounter = 0;
				comboable = false;
				comboCooldown.cooldown = 30;				
			}

		if(comboCounter<1 && comboCooldown.cooldown<1){
				heavyAttack();
				comboCounter=0;
				comboable = false;
			}

		else{
				comboCounter--;
			}

			//}
	}

	public override void faceCheck(){
		base.faceCheck();
		//Debug.Log("xFacing: "+xFacing+" zFacing: "+zFacing);
		if(Input.GetKey(KeyCode.I)||Input.GetAxis("Vertical")>0f){
			zFacing = 1;		
			xFacing = 0;
			movementSpeed=defaultMovementSpeed;
			//new
			transform.forward = new Vector3(0f, 0f, 1f);
		}
		
		if(Input.GetKey(KeyCode.L)||Input.GetAxis("Horizontal")>0f){
			xFacing = 1;
			zFacing = 0;
			movementSpeed=defaultMovementSpeed;
			//new
			transform.forward = new Vector3(1f, 0f, 0f);

		}
		
		if(Input.GetKey(KeyCode.K)||Input.GetAxis("Vertical")<00f){
			zFacing = -1;
			xFacing = 0;
			movementSpeed=defaultMovementSpeed;
			//new
			transform.forward = new Vector3(0f, 0f, -1f);
		}
		
		if(Input.GetKey(KeyCode.J)||Input.GetAxis("Horizontal")<0f){
			xFacing=-1;
			zFacing=0;
			movementSpeed=defaultMovementSpeed;
			//new
			transform.forward = new Vector3(-1f, 0f, 0f);
		}
		
		if(Input.GetKey(KeyCode.K)&&Input.GetKey(KeyCode.J)||Input.GetAxis("Vertical")<0&&Input.GetAxis("Horizontal")<0){
			zFacing = -1;
			xFacing = -1;
			movementSpeed=defaultMovementSpeed;
			transform.forward = new Vector3(-1f, 0f, -1f);
		}
		
		if(Input.GetKey(KeyCode.K)&&Input.GetKey(KeyCode.L)||Input.GetAxis("Vertical")<0&&Input.GetAxis("Horizontal")>0){
			zFacing = -1;
			xFacing = 1;
			movementSpeed=defaultMovementSpeed;
			transform.forward = new Vector3(1f, 0f, -1f);
		}
		
		if(Input.GetKey(KeyCode.I)&&Input.GetKey(KeyCode.L)||Input.GetAxis("Vertical")>0&&Input.GetAxis("Horizontal")>0){
			zFacing = 1;
			xFacing = 1;
			movementSpeed=defaultMovementSpeed;
			transform.forward = new Vector3(1f, 0f, 1f);
		}
		
		if(Input.GetKey(KeyCode.I)&&Input.GetKey(KeyCode.J)||Input.GetAxis("Vertical")>0&&Input.GetAxis("Horizontal")<0){
			zFacing = 1;
			xFacing = -1;
			movementSpeed=defaultMovementSpeed;
			transform.forward = new Vector3(-1f, 0f, 1f);
		}
	}
		IEnumerator heavyComboCooldown() {
		yield return new WaitForSeconds (.5f);
		p1Attacking = false;
		Jab1 = 1;
	}

	IEnumerator bassDrum() {
		
		yield return  new WaitForSeconds(.70f);
		p1HoldingA = true;
		yield return new WaitForSeconds(.35f);
		p1HoldingA = false;



		
	}

	IEnumerator comboXPart1() {

		yield return new WaitForSeconds(1.60f);
		p1HoldingX = true;
		yield return new WaitForSeconds(.35f);
		p1HoldingX = false;
	}

	IEnumerator lightCooldown() {
		yield return new WaitForSeconds(.24f);
		Jab1 = 2;
		p1Attacking = false;

		}

		IEnumerator lightCooldown1() {
		yield return new WaitForSeconds(.24f);
		Jab1 = 1;
		p1Attacking = false;

		}
	IEnumerator isP1Attacking() {
			yield return new WaitForSeconds(.24f);
			p1Attacking = false;
		}

	public override void attackCheck(){
				base.attackCheck ();

		if (Input.GetButton ("P1Y")) {
						p1HoldingY = true;
			EnergyYP1.SetActive (true);
						//aoe ();
				} else {
						p1HoldingY = false; 
			EnergyYP1.SetActive (false);
				}

		if (Input.GetButtonDown ("P1X")) {
			comboX ();
			StartCoroutine("comboXPart1");
			//p1HoldingX = true;
			//aoe ();
		} else {
			//p1HoldingX = false; 
		}

		if ((Input.GetButton ("P1B"))) {
			p1HoldingB = true;
			p1Attacking = true;
			EnergyBP1.SetActive (true);

			
		}
		else {
			p1HoldingB = false;
			p1Attacking = false;
			EnergyBP1.SetActive (false);
		}

		if ((Input.GetButtonDown ("P1A"))) {
			rb.transform.Translate (Vector3.forward * 1);
			comboA();
			GetComponent<AudioSource>().Play();
			StartCoroutine("bassDrum");
			

			
		}
		else {

			//p1HoldingA = false;
		}

		if ((Input.GetButton ("P1LB"))) {
						p1HoldingLB = true;
				} else {
						p1HoldingLB = false;
				}
		if ((Input.GetButton ("P1RB"))) {
			p1HoldingRB = true;
			EnergyRB1.SetActive (true);
		} else {
			p1HoldingRB = false;
			EnergyRB1.SetActive (false);
		}

		if (Input.GetButtonDown ("P1LB") && p2HoldRB == true && beatbool == true) {

			comboConnect ();	

		}

		if (Input.GetButtonDown ("P1X") && p2HoldY == true && beatbool == true) {
			lightDodge();
			gameObject.transform.position = (P2GameObject.transform.position);		
			lightDodge ();
		}

		else if (Input.GetButtonDown ("P1X")) {
			//lightDodge();
			//rb.transform.Translate (Vector3.forward * 10);

			//lightDodge();
		}

		/*if ((Input.GetButtonDown ("Punch2") || Input.GetKeyDown (KeyCode.N)) && (ExtraHit1 == true) && (beatbool == true)) {
			lightAttack3 ();
			lightAttack2 ();
			lightAttack ();
			ExtraHit1 = false;
			ExtraHit = false;
			p1Attacking = true;
			StartCoroutine(isP1Attacking());

		}
		//second hit in combo
		if ( (Input.GetButtonDown ("Punch2") || Input.GetKeyDown (KeyCode.N)) && (ExtraHit == true) && (beatbool == true)) {
			//if ( (Input.GetKeyDown (KeyCode.Space)) && (attackCooldown > 1) && (attackCooldown < 1.5) && (ExtraHit == true)) {
			Debug.Log ("Fire 2 pushed");
			//GetComponent<AudioSource>().PlayOneShot(Guitar2, 0.7F);
			//myAnimator.SetBool ("Attack", true);
			rb.transform.Translate (Vector3.forward * 3);
			lightAttack2 ();
			lightAttack ();
			ExtraHit1 = true; 
			ExtraHit = false;
			FirstHit = false;
			p1Attacking = true;
			StartCoroutine(isP1Attacking());
		} */

		if ((Input.GetButton ("P1B"))) {
			p1Attacking = true;

		}
		else {
			p1Attacking = false;
		}

		//Heavy Combined Attack
		if ((Input.GetButtonDown ("P1A") || Input.GetKeyDown(KeyCode.B) ) && (p2HoldB == true) && (beatbool == true)) {
			 

			//myAnimator.SetBool ("Attack", true);

			/*StartCoroutine("frontAttackPowered");
						
			ExtraHit = true;
			ExtraHit1 = false;
			FirstHit = false;
			Jab1 = 0;
			ComboCount += 1;
			p1Attacking = true;
			StartCoroutine(heavyComboCooldown());*/
		}
	
			
			

		//Basic light attack
		/*if ((Input.GetButtonDown ("P1A")) && (Jab1 == 1)) {
			//myAnimator.SetBool ("Attack", true);
			rb.transform.Translate (Vector3.forward * 1);
			lightJab ();
			//lightAttack5 ();			
			ExtraHit = true;
			ExtraHit1 = false;
			FirstHit = false;
			Jab1 = 0;
			ComboCount += 1;
			p1Attacking = true;
			StartCoroutine(lightCooldown());
				}
		//Second part of basic light attack (for alternating punches)
		else if ((Input.GetButtonDown ("P1A")) && (Jab1 == 2)) {
			//myAnimator.SetBool ("Attack", true);
			rb.transform.Translate (Vector3.forward * 1);
			lightJab ();
			//lightAttack6 ();			
			ExtraHit = true;
			ExtraHit1 = false;
			FirstHit = false;
			Jab1 = 0;
			ComboCount += 1;
			p1Attacking = true;
			StartCoroutine(lightCooldown1());
			

		}*/




				//if ((attackCooldown > 1.5) && (attackCooldown < 2)) {
				//		rend.sharedMaterial = material2;
				//} else { 
				//		rend.sharedMaterial = material1;
				//}
				//if ((attackCooldown > 2) && (attackCooldown < 2.5)) {
				//		rend.sharedMaterial = material3;
				//}
				//Debug.Log ("charge: "+charge);
		//if (Input.GetKeyDown (KeyCode.V) || Input.GetButtonDown ("Fire3")) {
			//if ((attackCooldown > 2.25) && (attackCooldown < 2.9)) {
			//Debug.Log ("Fire 2 pushed");
			//GetComponent<AudioSource>().PlayOneShot(Guitar2, 0.7F);
			//myAnimator.SetBool ("Attack", true);
			//lightAttack2 ();
		//}

				/*if (Input.GetKeyDown (KeyCode.Z) || Input.GetButtonDown ("Fire2")) {





						if ((Saiyan1 < 1)) {
						//if ((attackCooldown < 1) && (Saiyan1 < 1)) {
					
								Debug.Log ("FIRE pushed");
				ExtraHit = true;
				ExtraHit1 = false;
								Drums.volume = (1.0f);
								Saiyan1 ++;
								//myAnimator.SetBool ("Swing", true);
				//myAnimator.SetBool ("Attack", true);
								lightAttack ();
						}
						if ((attackCooldown < 1) && (Saiyan1 == 1)) {
				ExtraHit = true;
				ExtraHit1 = false;
								Bass.volume = (1.0f);
								Saiyan1 ++;
								//myAnimator.SetBool ("Swing", true);
				//myAnimator.SetBool ("Attack", true);
								lightAttack ();
						}
						if ((attackCooldown < 1) && (Saiyan1 == 2)) {
				ExtraHit = true;
				ExtraHit1 = false;
								Guitar.volume = (1.0f);
								Saiyan1 ++;
								//myAnimator.SetBool ("Swing", true);
				//myAnimator.SetBool ("Attack", true);
								lightAttack ();
						}
						if ((attackCooldown < 1) && (Saiyan1 == 3)) {
				ExtraHit = true;
				ExtraHit1 = false;
								Vox.volume = (1.0f);
								Saiyan1 = 0;
								//myAnimator.SetBool ("Swing", true);
				//myAnimator.SetBool ("Attack", true);
								lightAttack ();
						}
						//GetComponent<AudioSource>().PlayOneShot(Guitar1, 0.7F);

						//Sword.GetComponent<Animation>().Play("SwordSwing");
						//Sword.gameObject.animation.Play("SwordSwing");
						//if (charAnimator.GetBool("die"))
						//	charAnimator.SetBool("die", false);	
						//Sword.GetComponent<Animator>().bool	
						//REINSTATEmyAnimator.SetBool ("Swing", true);
						//Swing = true;
						

					
					
					


						//REINSTATElightAttack ();
				//}
			//if (Input.GetKeyDown (KeyCode.V) || Input.GetButtonDown ("Fire3")) {




	
						if ((attackCooldown > 1.5) && (attackCooldown < 2)) {
								Debug.Log ("Fire 2 pushed");
				//GetComponent<AudioSource>().PlayOneShot(Guitar2, 0.7F);
				//myAnimator.SetBool ("Swing2", true);
							//	lightAttack2 ();
				//ExtraHit1 = true; 

						}
		
				} else {
			myAnimator.SetBool ("Attack", false);			
			//myAnimator.SetBool ("Swing", false);
			myAnimator.SetBool ("Swing2", false);
			myAnimator.SetBool ("Swing3", false);
				}
		
		if(Input.GetKey(KeyCode.Space)||Input.GetButton("Fire2"))
		{
			Debug.Log("Player two charge at: "+charge);
			charge= charge+Time.deltaTime;
			
		}
		
		if(Input.GetKeyUp(KeyCode.Space)||Input.GetButtonUp("Fire2"))
		{
			Debug.Log("Player two FIRE released");
			if(charge>maxCharge){
				Debug.Log("Player two super fireball");
				comboCounter = comboWindow;
				comboable = true;
				//	heavyAttack();
			}
			charge=0;
		}*/
	}

}

