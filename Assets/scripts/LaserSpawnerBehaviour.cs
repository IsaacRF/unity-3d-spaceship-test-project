using UnityEngine;
using System.Collections;

public class LaserSpawnerBehaviour : MonoBehaviour {
	
	public Transform laser;
	public Transform missile;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Fires a laser at the Spawner position
	public void fireLaser()
	{		
		Instantiate (laser, this.transform.position, this.transform.rotation);
	}

	//Fires a missile that follows the target at the Spawner position
	public void fireMissile(GameObject target)
	{
		missile.GetComponent<MissileBehaviour> ().target = target;
		Instantiate (missile, this.transform.position, this.transform.rotation);
	}
}
