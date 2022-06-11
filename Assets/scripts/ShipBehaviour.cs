using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShipBehaviour : MonoBehaviour {
	
	private float energyDrainTime = 0.5F;
	private float energyDrainTimeElapse = 0.5F;
	private float energyDrainLaser = 1;
	private float laserFireTime = 0.1F;
	private float missileFireTime = 5F;
	private float nextEnergyDrainTime = 0.0F;
	private float nextLaserTime = 0.0F;
	private float nextMissileTime = 0.0F;
	private float energy = 100;
	private float shipSpeed = 50;
	private int weaponSelected = 1;
	private bool humanSpawned = false;

	private bool weapon2Unlocked = true;
	private bool weapon3Unlocked = false;

	public Rigidbody shipRgBody;
	public Text lblShipInfo;
	public Text lblShipEnergy;
	public Slider sldEnergy;
	public Image imgEnergyBar;
	public Canvas uiGameOver; 
	public Light flashlight;
	public Transform laserSpawnerLeft;
	public Transform laserSpawnerFront;
	public Transform laserSpawnerRight;
	public Text lblWeapon1;
	public Text lblWeapon2;
	public Text lblWeapon3;

	public AudioClip energySound;
	public Transform human;


	// Use this for initialization
	void Start () {
		shipRgBody = this.GetComponent <Rigidbody> ();
		shipRgBody.drag = 10;

		if (lblShipInfo != null) 
		{
			lblShipInfo.text = string.Format("Energy = {0}%", energy.ToString());
		}

		if (weapon2Unlocked) {
			lblWeapon2.color = Color.black;
		}
		if (weapon3Unlocked) {
			lblWeapon3.color = Color.black;
		}

		switch (weaponSelected) 
		{
		case 1:
			lblWeapon1.color = Color.blue;
			lblWeapon2.color = (weapon2Unlocked ? Color.black : Color.gray);
			lblWeapon3.color = (weapon3Unlocked ? Color.black : Color.gray);
			break;
		case 2:
			lblWeapon1.color = Color.gray;
			lblWeapon2.color = Color.blue;
			lblWeapon3.color = (weapon3Unlocked ? Color.black : Color.gray);
			break;
		case 3:
			lblWeapon1.color = Color.black;
			lblWeapon2.color = (weapon2Unlocked ? Color.black : Color.gray);
			lblWeapon3.color = Color.blue;
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Control the ship only if there is remaining energy
		if (energy > 0) 
		{
			//Control User Input over Space ship
			// code for the movement of Cube1' forward
			if (Input.GetKey (KeyCode.UpArrow)) {
				//Translation method
				//this.transform.Translate (Vector3.forward * Time.deltaTime * shipSpeed);

				//Forces method
				shipRgBody.drag = 10;
				shipRgBody.AddForce(this.transform.forward * shipSpeed);
			}
			else
			{
				shipRgBody.drag = 4;
			}
			// code for the movement of Cube1' backward
			if (Input.GetKey (KeyCode.DownArrow)) {
				this.transform.Translate (Vector3.back * Time.deltaTime * 2);
			}
			// code for the movement of Cube1' left
			if (Input.GetKey (KeyCode.LeftArrow)) {
				this.transform.Rotate (new Vector3 (0, 1, 0), -1);
			}
			// code for the movement of Cube1' right
			if (Input.GetKey (KeyCode.RightArrow)) {
				this.transform.Rotate (new Vector3 (0, 1, 0), 1);
			}
			if (Input.GetKey (KeyCode.W)) {
				this.transform.Rotate (new Vector3 (1, 0, 0), 1);
			}
			if (Input.GetKey (KeyCode.S)) {
				this.transform.Rotate (new Vector3 (1, 0, 0), -1);
			}
			if (Input.GetKey (KeyCode.A)) {
				this.transform.Rotate (new Vector3 (0, 0, 1), 1);
			}
			if (Input.GetKey (KeyCode.D)) {
				this.transform.Rotate (new Vector3 (0, 0, 1), -1);
			}
			if (Input.GetKey (KeyCode.F))
			{
				if (flashlight.enabled)
				{
					flashlight.enabled = false;
				}
				else
				{
					flashlight.enabled = true;
				}
			}
			if (Input.GetKey (KeyCode.E))
			{
				if (humanSpawned == false)
				{
					Instantiate(human, this.transform.position + this.transform.forward, this.transform.rotation);
					humanSpawned = true;
				}
			}
			if (Input.GetKey(KeyCode.LeftShift)) {
				shipSpeed = 120;
				energyDrainTimeElapse = 1;
			}
			else
			{
				shipSpeed = 90;
				energyDrainTimeElapse = 0.5F;
			}
			if (Input.GetKey(KeyCode.Alpha1))
			{
				weaponSelected = 1;

				lblWeapon1.color = Color.blue;
				lblWeapon2.color = (weapon2Unlocked ? Color.black : Color.gray);
				lblWeapon3.color = (weapon3Unlocked ? Color.black : Color.gray);
			}
			if (Input.GetKey(KeyCode.Alpha2) && weapon2Unlocked)
			{
				weaponSelected = 2;

				lblWeapon1.color = Color.black;
				lblWeapon2.color = Color.blue;
				lblWeapon3.color = (weapon3Unlocked ? Color.black : Color.gray);
			}
			if (Input.GetKey(KeyCode.Alpha3) && weapon3Unlocked)
			{
				weaponSelected = 3;

				lblWeapon1.color = Color.black;
				lblWeapon2.color = (weapon2Unlocked ? Color.black : Color.gray);
				lblWeapon3.color = Color.blue;
			}
			//Fire
			if (Input.GetKey (KeyCode.Space) ) {

				/*	This is an alternative method, you can calculate the position where 
					you want to spawn the laser, but is more accurate
					to use spawners
				*/
				/*Vector3 laser1Position = 
					this.transform.position + this.transform.forward + this.transform.right;
				Vector3 laser2Position = 
					this.transform.position + this.transform.forward - this.transform.right;

				Instantiate (laser, laser1Position, this.transform.rotation);
				Instantiate (laser, laser2Position, this.transform.rotation);*/

				//Fire current weapon using Laser Spawners
				switch (weaponSelected)
				{
				case 1:	//Laser
					if (Time.time > nextLaserTime)
					{
						//laserSpawnerLeft.GetComponent<LaserSpawnerBehaviour>().fireLaser();
						laserSpawnerFront.GetComponent<LaserSpawnerBehaviour>().fireLaser();
						//laserSpawnerRight.GetComponent<LaserSpawnerBehaviour>().fireLaser();

						//Update firing time
						nextLaserTime = Time.time + laserFireTime;
						
						//Energy consumption by firing laser
						energy = energy - energyDrainLaser;
					}
					break;
				case 2:	//Missile
					if (Time.time > nextMissileTime)
					{
						laserSpawnerFront.GetComponent<LaserSpawnerBehaviour>().fireMissile(GameObject.FindGameObjectWithTag("EnemyShip"));

						//Update firing time
						nextMissileTime = Time.time + missileFireTime;
					}
					break;
				case 3:
					break;
				}
			}

			//Calculate energy drained by time
			if (Time.time > nextEnergyDrainTime) {
				energy = energy - energyDrainTimeElapse;
				nextEnergyDrainTime = Time.time + energyDrainTime;
			}


			//Update info panel labels and controls
			if (lblShipInfo != null) {
				lblShipInfo.text = string.Format ("P:<{0:F2},{1:F2},{2:F2}>; R:<{3:F2},{4:F2},{5:F2}>",
			                                  this.transform.localPosition.x,
			                                  this.transform.localPosition.y,
			                                  this.transform.localPosition.z,
			                                  this.transform.localEulerAngles.x,
			                                  this.transform.localEulerAngles.y,
			                                  this.transform.localEulerAngles.z);
			}
			//Update remaining energy
			if (lblShipEnergy != null) {
				lblShipEnergy.text = string.Format ("Energy = {0}%", energy.ToString ());
			}
			if (sldEnergy != null) {
				sldEnergy.value = energy;
				imgEnergyBar.color = Color.Lerp (Color.red, Color.green, (float)energy / 100);
			}
		}
		else
		{
			//If energy reaches 0, the space ship fall
			shipRgBody.drag = 0;
			this.GetComponent<Rigidbody>().useGravity = true;
			//And the game ends
			uiGameOver.gameObject.SetActive(true);
		}
	}

	// This event will be raised by objects that have their Is Trigger attributed enabled.
	// In our case, the fuel GameObject has Is Trigger set to true on its collider.
	void OnTriggerEnter(Collider c)
	{
		AudioSource.PlayClipAtPoint (energySound, this.transform.position);

		//Recover energy
		if(c.tag.Equals("Fuel")){
			if ((energy + 20) >= 100)
			{
				energy = 100;
			}
			else 
			{
				energy = energy + 20; 
			}
		}

		//Destroy the fuel GameObject so it dissapears from the scene
		Destroy (c.gameObject);
	}

	//Event called when the object enters in collision with another object
	void OnCollisionEnter(Collision c)
	{
		if (c.gameObject.tag == "Laser") {
			if (energy >= 10)
			{
				energy = energy - 10;
			}
			else
			{
				energy = 0;
			}

			Debug.Log("Shippy received 10% damage, remaining energy " + energy);
			
			if (energy == 0)
			{
				Debug.Log("Shippy destroyed!");
				//Update remaining energy
				if (lblShipEnergy != null) {
					lblShipEnergy.text = string.Format ("Energy = {0}%", energy.ToString ());
				}
				if (sldEnergy != null) {
					sldEnergy.value = energy;
					imgEnergyBar.color = Color.Lerp (Color.red, Color.green, (float)energy / 100);
				}
			}
		}
	}

	public void btnReset_Click()
	{
		this.transform.localPosition = new Vector3 (0, 2, 0);
		this.transform.rotation = Quaternion.identity;
	}

	public void btnGameOver_Click()
	{
		Application.LoadLevel (Application.loadedLevel);
	}

	public void btnLights_Click()
	{
		foreach(Light light in FindObjectsOfType<Light>())
		{
			if (light.tag == "StageLight")
			{
				if (light.enabled)
				{
					light.enabled = false;
				}
				else
				{
					light.enabled = true;
				}
			}
		}
	}
}
