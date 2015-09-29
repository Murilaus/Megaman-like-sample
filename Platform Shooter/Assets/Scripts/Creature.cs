using UnityEngine;
using System.Collections;

public class Creature: MonoBehaviour{

	public GameObject shot;
	public float shotCharge;
	public GameObject shotPoint;
	public float health;
	public float currentHealth;
	public float speed;
	public float direction;
	public float damage;
	public float jumpIntensity;
	public bool isGrounded;
	public float raycastDown;

	void Update(){
		if (currentHealth <= 0) {
			Die ();
		}
	}

	public void ApplyDamage(float damage){
		currentHealth -= damage;
	}

	public void Die(){
		Destroy (gameObject);
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Player") {
			other.gameObject.SendMessage ("ApplyDamage", damage);
		}
	}
}
