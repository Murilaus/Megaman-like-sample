using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public Slider healthSlider;

	public GameObject shot;
	public GameObject chargedShot;
	public GameObject chargeParticles;
	public float charge;
	public float shotCharge;
	public GameObject shotPoint;
	public float damage;

	public float health;
	public float currentHealth;
	public float currentSpeed;
	public float speed;
	public float acceleration;
	public float direction;

	public GameObject frontalRaycast;
	public GameObject backRaycast;
	public bool grounded;

	public float jumpIntensity;
	public bool canJump;
	public float raycastDown;

	public bool jumping;


	public bool airCollision;	
	public bool hasJumped;
	public float timeJumping;
	public float maxJump;
	Rigidbody rb;
	
	// Use this for initialization
	void Start () {
		healthSlider.maxValue = health;
		currentHealth = health;
		healthSlider.value = currentHealth;
		charge = 0;
		timeJumping = 0;
		direction = 1;
		rb = this.GetComponent<Rigidbody>();
		chargeParticles.SetActive(false);
		currentSpeed = speed;
	}
	
	// Update is called once per frame
	void Update () {
		SetJump ();
		UpdateHealth();
		Shot ();
		Jump ();
		CheckGround ();

		Debug.Log ("Speed: " + rb.velocity.x);
	}

	void FixedUpdate(){
		Walk ();
	}
	
	void Turn(){
		direction *= -1;
		transform.Rotate (new Vector3 (0, 1, 0), 180);
	}
	
	void Walk (){
		float movement = Input.GetAxis ("Horizontal")*currentSpeed;
		if (movement < 0 && direction == 1 || movement > 0 && direction == -1) {
			Turn ();
		}
		if (movement != 0/* && !airCollision*/) {
			rb.velocity = new Vector3(movement, rb.velocity.y, 0);
		}
	}

	void UpdateHealth(){
		if (healthSlider.value > currentHealth) {
			healthSlider.value -= 10 * Time.deltaTime;
		}
	}

	void Jump(){

		if (canJump) {
			if(Input.GetButtonDown ("Jump")){
				jumping = true;
			}
			if(Input.GetButtonUp ("Jump") || timeJumping >= maxJump){
				jumping = false;
				canJump = false;
			}

			if(jumping){
				timeJumping += Time.deltaTime;
				rb.velocity = new Vector3 (rb.velocity.x, jumpIntensity+Mathf.Pow (1+timeJumping, 4), 0);
			}
		}

		/*if (Input.GetButtonDown ("Jump") && grounded) {
			jumping = true;
		}
		if (Input.GetButtonUp ("Jump")) {
			jumping = false;
		}

		if (Input.GetButton ("Jump") && jumping && timeJumping < maxJump) {
			timeJumping += Time.deltaTime;
			rb.velocity = new Vector3 (rb.velocity.x, jumpIntensity+Mathf.Pow (1+timeJumping, 4), 0);
		}else{
			jumping = false;
		}*/
	}

	void Shot(){
		if (charge >= shotCharge) {
			chargeParticles.SetActive(true);
		} else {
			chargeParticles.SetActive(false);
		}
		
		if (Input.GetButton ("Fire1")) {
			charge += Time.deltaTime;
		}
		if (Input.GetButtonUp ("Fire1")) {
			DoShoot(charge);
			charge = 0;
		}
	}

	void OnCollisionStay(Collision info){
		if (!grounded) {
			airCollision = true;
			currentSpeed = 0.1f;
		} else {
			airCollision = false;
			currentSpeed = speed;
		}
	}

	void OnCollisionExit(Collision info){
		airCollision = false;
		currentSpeed = speed;
	}

	void DoShoot(float charge){
		if (charge >= shotCharge) {
			Instantiate (chargedShot, shotPoint.transform.position, shotPoint.transform.rotation);
		} else {
			Instantiate (shot, shotPoint.transform.position, shotPoint.transform.rotation);
		}
	}

	void ApplyDamage(float damage){
		currentHealth -= damage;
		//healthSlider.value = currentHealth;
		if (currentHealth <= 0) {
			Die ();
		}
	}

	void Die(){
		Application.LoadLevel (Application.loadedLevel);
	}

	void CheckGround(){
		if(Physics.Raycast (frontalRaycast.transform.position, new Vector3(0, -1, 0), raycastDown) || 
			Physics.Raycast (backRaycast.transform.position, new Vector3(0, -1, 0), raycastDown)){
				grounded = true;
		}
		else{
			grounded = false;
		}
	}

	void SetJump(){
		if (grounded && !jumping) {
			canJump = true;
			timeJumping = 0;
		}
	}
}
