using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {
	public Rigidbody projectile;
	public Transform shotPos;
	public float shotForce = 1000f;
	public float moveSpeed = 15f;
	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	public float turnSpeed = 60F;
	private Vector3 moveDirection = Vector3.zero;
	private Vector3 moveRotation = Vector3.zero; // NEW
	// Use this for initialization
	//void Start () {
	//
	//}
	
	// Update is called once per frame
	void Update () {

		CharacterController controller = GetComponent<CharacterController>();
		//float turn = Input.GetAxis ("Horizontal1");
		//transform.Rotate (0, turn * turnSpeed * Time.deltaTime, 0);


		if (controller.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis("Horizontal1"), 0, Input.GetAxis("Vertical1"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			//moveDirection = transform.forward * Input.GetAxis("Vertical1") * speed;
			//controller.Move(moveDirection * Time.deltaTime);
			//moveRotation = transform.Rotate(Input.GetAxis(Vector3("Horizontal1"), 0, Input.GetAxis("Vertical1"))); //NEW
			if (Input.GetButton("Jump1"))
				moveDirection.y = jumpSpeed;
			
		}
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);

		if (Input.GetButtonUp ("Fire1")) {
			Rigidbody shot = Instantiate(projectile, shotPos.position, shotPos.rotation) as Rigidbody;
			shot.AddForce(shotPos.forward * shotForce);		
		}
	}

	
	}