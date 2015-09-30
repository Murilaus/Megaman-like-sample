using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {

	public GameObject sparks;

	public float speed;
	public float damage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Enemy") {
			other.gameObject.SendMessage ("ApplyDamage", damage);
			damage=0;
			Debug.Log("Hit " + damage);
		}
		Instantiate (sparks, transform.position, Quaternion.Euler (
			new Vector3(transform.rotation.x, transform.rotation.y+270, transform.rotation.z)));

		Destroy (gameObject);
	}
}
