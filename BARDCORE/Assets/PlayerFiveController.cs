using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class playerFiveController : playerController {
	
	public BeatTimer timer;
	public Material material1;
	public Material material2;
	public Material material3;
	public Renderer rend;
	public AudioSource Drums;
	public AudioSource Bass;
	public AudioSource Guitar;
	public AudioSource Vox;
	public GameObject Sword;
	//public GameObject Sword2;
	private Animator myAnimator;
	private float Saiyan1;
	public bool FirstHit;
	public bool ExtraHit;
	public bool ExtraHit1;
	public int Jab1;
	
	//private bool Saiyan2;
	//private bool Saiyan3;
	//private bool Saiyan4;
	private Rigidbody rb;	
	// Use this for initialization
	void Start () {
		//rend = GetComponent<Renderer>();
		FirstHit = true;
		ExtraHit1 = false; 
		ExtraHit = false; 
		rend.enabled = true;
		Jab1 = 1;
		myAnimator = GetComponentInChildren<Animator>();
		
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
		if(Input.GetKey(KeyCode.W)||Input.GetAxis("Vertical1")>0f){
			zFacing = 1;		
			xFacing = 0;
			movementSpeed=defaultMovementSpeed;
			//new
			transform.forward = new Vector3(0f, 0f, 1f);
		}
		
		if(Input.GetKey(KeyCode.D)||Input.GetAxis("Horizontal1")>0f){
			xFacing = 1;
			zFacing = 0;
			movementSpeed=defaultMovementSpeed;
			//new
			transform.forward = new Vector3(1f, 0f, 0f);
			
		}
		
		if(Input.GetKey(KeyCode.S)||Input.GetAxis("Vertical1")<00f){
			zFacing = -1;
			xFacing = 0;
			movementSpeed=defaultMovementSpeed;
			//new
			transform.forward = new Vector3(0f, 0f, -1f);
		}
		
		if(Input.GetKey(KeyCode.A)||Input.GetAxis("Horizontal1")<0f){
			xFacing=-1;
			zFacing=0;
			movementSpeed=defaultMovementSpeed;
			//new
			transform.forward = new Vector3(-1f, 0f, 0f);
		}
		
		if(Input.GetKey(KeyCode.S)&&Input.GetKey(KeyCode.A)||Input.GetAxis("Vertical1")<0&&Input.GetAxis("Horizontal1")<0){
			zFacing = -1;
			xFacing = -1;
			movementSpeed=defaultMovementSpeed;
			transform.forward = new Vector3(-1f, 0f, -1f);
		}
		
		if(Input.GetKey(KeyCode.S)&&Input.GetKey(KeyCode.D)||Input.GetAxis("Vertical1")<0&&Input.GetAxis("Horizontal1")>0){
			zFacing = -1;
			xFacing = 1;
			movementSpeed=defaultMovementSpeed;
			transform.forward = new Vector3(1f, 0f, -1f);
		}
		
		if(Input.GetKey(KeyCode.W)&&Input.GetKey(KeyCode.D)||Input.GetAxis("Vertical1")>0&&Input.GetAxis("Horizontal1")>0){
			zFacing = 1;
			xFacing = 1;
			movementSpeed=defaultMovementSpeed;
			transform.forward = new Vector3(1f, 0f, 1f);
		}
		
		if(Input.GetKey(KeyCode.W)&&Input.GetKey(KeyCode.A)||Input.GetAxis("Vertical1")>0&&Input.GetAxis("Horizontal1")<0){
			zFacing = 1;
			xFacing = -1;
			movementSpeed=defaultMovementSpeed;
			transform.forward = new Vector3(-1f, 0f, 1f);
		}
	}
	
	public override void attackCheck(){
		base.attackCheck ();
		
		if ((Input.GetButtonDown ("Fire2")) && (ExtraHit1 == true)) {
			lightAttack3 ();
			lightAttack2 ();
			lightAttack ();
			ExtraHit1 = false;
			ExtraHit = false;
			
		}
		
		if ( (Input.GetButtonDown ("Fire2")) && (ExtraHit == true)) {
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
		}
		
		
		if (Input.GetButtonDown ("Fire") && (Jab1 == 1)) {
			//myAnimator.SetBool ("Attack", true);
			rb.transform.Translate (Vector3.forward * 1);
			lightAttack5 ();			
			ExtraHit = true;
			ExtraHit1 = false;
			FirstHit = false;
			Jab1 += 1;
		}
		
		else if (Input.GetButtonDown ("Fire") && (Jab1 == 2)) {
			//myAnimator.SetBool ("Attack", true);
			rb.transform.Translate (Vector3.forward * 1);
			lightAttack6 ();			
			ExtraHit = true;
			ExtraHit1 = false;
			FirstHit = false;
			Jab1 = 1;
		}
		
		
		
		
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
