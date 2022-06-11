using UnityEngine;
using System.Collections;

public class LaserBehaviour : MonoBehaviour {
	
	private float laserDurationTime = 1F;
	private float nextLaserDestroyTime;

	// Use this for initialization
	void Start () {
		nextLaserDestroyTime = Time.time + laserDurationTime;
	}
	
	// Update is called once per frame
	void Update () {
		//The basic laser always move forward till it collides with another object
		this.transform.Translate(Vector3.forward * Time.deltaTime * 40);

		/*If the specified time elapses and the laser doesn't collide with anything it
		  dissapears 
	 	*/
		if (Time.time > nextLaserDestroyTime) 
		{
			Destroy (this.gameObject);
		}
	}

	//Event called when the object enters in collision with another object
	void OnCollisionEnter(Collision c)
	{
		Debug.Log ("Laser collision with: " + c.gameObject.name);
		Destroy (this.gameObject);
	}
}
