using UnityEngine;
using System.Collections;

public class MissileBehaviour : MonoBehaviour {

	private float missileDurationTime = 5F;
	private float nextMissileDestroyTime;
	private Quaternion targetRotation;
	private float rotationSpeed;

	public GameObject target;

	// Use this for initialization
	void Start () {
		nextMissileDestroyTime = Time.time + missileDurationTime;
		rotationSpeed = Mathf.Min (Time.deltaTime * 2, 1);
	}
	
	// Update is called once per frame
	void Update () {
		//The missile moves forward aiming to the target (if exists)
		this.transform.Translate(Vector3.forward * Time.deltaTime * 10);
		if (target != null) {
			targetRotation = Quaternion.LookRotation (target.transform.position - this.transform.position);
			this.transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, rotationSpeed);
		}
		
		/*If the specified time elapses and the missile doesn't collide with anything it
		  dissapears
	 	*/
		if (Time.time > nextMissileDestroyTime) 
		{
			Destroy (this.gameObject);
		}
	}

	//Event called when the object enters in collision with another object
	void OnCollisionEnter(Collision c)
	{
		Debug.Log ("Missile collision with: " + c.gameObject.name);
		Destroy (this.gameObject);
	}
}
