using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	public GameObject target;
	public float minDistance;
	public float maxDistance;
	public float delay;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Abs(transform.position.x - target.transform.position.x)>maxDistance) {
			MoveX();
		}
		if (Mathf.Abs(transform.position.y - target.transform.position.y)>maxDistance) {
			MoveY();
		}
	}

	void MoveX(){
		float xPosition = Mathf.Lerp (transform.position.x, target.transform.position.x, delay * Time.deltaTime);
		transform.position = new Vector3(xPosition, transform.position.y, transform.position.z);
	}
	
	void MoveY(){
		float yPosition = Mathf.Lerp (transform.position.y, target.transform.position.y, delay * Time.deltaTime);
		transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);
	}
}
