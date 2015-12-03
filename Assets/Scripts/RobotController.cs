using UnityEngine;
using System.Collections;

public class RobotController : MonoBehaviour {

	bool grounded = false; 

	public Transform Groundcheck; 

	float groundRadius = 0.2f; 

	public LayerMask whatIsGround; 

	public float jumpForce = 700f;

	public float maxSpeed = 2f; 

	bool facingLeft = true;

	Animator anim;

	void Start () {
	
		anim = GetComponent<Animator>();
	}
	void Update(){
		if (grounded && Input.GetKeyDown (KeyCode.Space)) { 
			anim.SetBool ("Ground", false); 
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpForce));
		} 
	}

	void FixedUpdate () {
		anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D>().velocity.y);
		grounded = Physics2D.OverlapCircle (Groundcheck.position, groundRadius, whatIsGround); 

		float move = Input.GetAxis("Horizontal");//Gives us of one if we are moving via the arrow keys

		GetComponent<Rigidbody2D>().velocity = new Vector3 (move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

		anim.SetFloat ("Speed",Mathf.Abs (move));
		//set ground in our Animator to match grounded anim.SetBool ("Ground", grounded);
		


		if (move < 0 && !facingLeft) 
		{
			Flip ();
		}
		else if (move > 0 && facingLeft) 
		{
			Flip ();

		}
	}


	void Flip () {
		facingLeft = !facingLeft;

		Vector3 theScale = transform.localScale;

		theScale.x *= -1;

		transform.localScale = theScale;

	}

}
          