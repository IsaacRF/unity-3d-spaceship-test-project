using UnityEngine;
using System.Collections;

public class EnemyShipBehaviour : MonoBehaviour {

	private float energy = 100;
	private float laserFireTime = 0.5F;
	private float nextLaserTime = 1.0F;
	private float rotationSpeed;
	private Quaternion targetRotation;

	private Animation destroyAnimation;

	private GameObject target;
	public Transform laserSpawner;

	// Use this for initialization
	void Start () {
		//The shoot target will be the player by default
		target = GameObject.FindGameObjectWithTag ("Shippy");
		rotationSpeed = Mathf.Min (Time.deltaTime * 3, 1);
		destroyAnimation = this.GetComponent<Animation> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextLaserTime && energy > 0) 
		{
			laserSpawner.GetComponent<LaserSpawnerBehaviour> ().fireLaser();

			nextLaserTime = Time.time + laserFireTime;
		}

		//Face target to shoot it
		targetRotation = Quaternion.LookRotation (target.transform.position - this.transform.position);
		this.transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, rotationSpeed);
	}

	//Event called when the object enters in collision with another object
	void OnCollisionEnter(Collision c)
	{
		if (c.gameObject.tag == "Laser") {
			energy = energy - 10;
			Debug.Log("Enemy ship -" + this.gameObject.name + "- received 10% damage from laser, remaining energy " + energy);
		}
		else if (c.gameObject.tag == "Missile")
		{
			energy = energy - 20;
			Debug.Log("Enemy ship -" + this.gameObject.name + "- received 20% damage from missile, remaining energy " + energy);
		}

		if (energy <= 0 && !destroyAnimation.isPlaying)
		{
			Debug.Log("Enemy ship -" + this.gameObject.name + "- destroyed");
			destroyAnimation.Play();
			Destroy (this.gameObject, 2);
		}
	}
}
